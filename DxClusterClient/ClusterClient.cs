using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClusterSkimmer
{
    public class ClusterClient
    {
        private readonly ILogger<ClusterClient> _logger;

        public event EventHandler<ClusterSpot> OnClusterSpotReceived;

        public ClusterClient(ILogger<ClusterClient> logger)
        {
            _logger = logger;
        }

        public void Start(string callsign, string host = "dxc.ve7cc.net", int port = 23, bool ft8 = true, bool beacon = true, bool skimmer = true, CancellationToken stoppingToken = default)
        {
            _ = Task.Run(() => WorkerLoop(host, port, callsign, stoppingToken, ft8, beacon, skimmer));
        }

        private async Task WorkerLoop(string host, int port, string callsign, CancellationToken cancellationToken, bool ft8, bool beacon, bool skimmer)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Connecting...");
                    using var client = new TcpClient(host, port);
                    var stream = client.GetStream();
                    using var streamReader = new StreamReader(stream);
                    using var streamWriter = new StreamWriter(stream) { AutoFlush = true };
                    
                    HandleClusterLogin(streamReader, streamWriter, callsign, ft8, beacon, skimmer);

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        string line = streamReader.ReadLine();
                        if (line == null || cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }

                        ProcessLine(line);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Exception");

                    await Task.Delay(10000);
                }
            }

            _logger.LogInformation("Disconnected");
        }

        private void HandleClusterLogin(StreamReader reader, StreamWriter writer, string callsign, bool ft8, bool beacon, bool skimmer)
        {
            _logger.LogInformation("Logging in...");

            ReadUntil(reader, "login: ");
            writer.WriteLine(callsign);
            ReadUntil(reader, $"{callsign} de VE7CC");
            ReadUntil(reader, $"{callsign} de VE7CC");
            ReadUntil(reader, ">");
            _logger.LogInformation("Sent callsign, setting options...");
            
            if (skimmer)
            {
                writer.WriteLine("set/skimmer");
                ReadUntil(reader, "Skimmer spots enabled");
            }
            else
            {
                writer.WriteLine("set/noskimmer");
                ReadUntil(reader, "Skimmer spots disabled");
            }

            if (beacon)
            {
                writer.WriteLine("set/beacon");
                ReadUntil(reader, "Beacon spots enabled");
            }
            else
            {
                writer.WriteLine("set/nobeacon");
                ReadUntil(reader, "Beacon spots disabled");
            }

            if (ft8)
            {
                writer.WriteLine("set/ft8");
                ReadUntil(reader, "FT8 spots enabled");
            }
            else
            {
                writer.WriteLine("set/noft8");
                ReadUntil(reader, "FT8 spots disabled");
            }

            _logger.LogInformation("Options set, login done");
        }

        private bool ReadUntil(StreamReader reader, string recognise)
        {
            var buffer = new StringBuilder();
            while (true)
            {
                char c = (char)reader.Read();
                Debug.Write(c);
                buffer.Append(c);
                if (buffer.ToString().EndsWith(recognise))
                {
                    return true;
                }
                
                if (c == '\n')
                {
                    buffer.Clear();
                }
            }
        }

        private void ProcessLine(string line)
        {
            if (ClusterSpot.TryParse(line, out var spot))
            {
                OnClusterSpotReceived?.Invoke(this, spot);
            }
            else
            {
                _logger.LogWarning("Could not parse line into spot: \n    " + line);
            }
        }
    }
}