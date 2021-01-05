using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace ClusterSkimmer
{
    public static class Program
    {
        /// <summary>
        /// Set this to true and run the console app to generate unit test skeletons suitable for pasting into the test project
        /// </summary>
        public static bool OutputUnitTests { get; set; }

        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Expected one argument, a callsign, to be used to log in to the DX cluster");
                return -1;
            }

            ILogger<ClusterClient> logger = LoggerFactory
                .Create(logging => logging.AddConsole())
                .CreateLogger<ClusterClient>();

            var client = new ClusterClient(logger);
            
            client.OnClusterSpotReceived += (s, e) =>
            {
                var json = JsonSerializer.Serialize(e, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault });
                Console.WriteLine(json);
            };

            client.Start(args[0]);

            Thread.CurrentThread.Join();

            return 0;
        }
    }
}