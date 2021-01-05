using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClusterSkimmer
{
    /*
        public long sequenceNumber { get; set; }
        public long? frequency { get; set; }
        public string mode { get; set; }
        public int? sNR { get; set; }
        public long flowStartSeconds { get; set; }
        public string senderCallsign { get; set; }
        public string senderLocator { get; set; }
        public string receiverCallsign { get; set; }
        public string receiverLocator { get; set; }
        public string receiverDecoderSoftware { get; set; }
     */

    public class ClusterSpot
    {
        /// <summary>
        /// Callsign that spotted the DX
        /// </summary>
        [JsonPropertyName("receiverCallsign")]
        public string ReceiverCallsign { get; set; }

        /// <summary>
        /// e.g. -1 from M0LTE-1
        /// </summary>
        public int? ReceiverSsid { get; set; }

        /// <summary>
        /// Frequency of the spot in Hz. For e.g. FT8, any offset will be added to the original spot frequency.
        /// </summary>
        [JsonPropertyName("frequency")]
        public long Frequency { get; set; }

        /// <summary>
        /// The DX call
        /// </summary>
        [JsonPropertyName("senderCallsign")]
        public string SenderCallsign { get; set; }

        /// <summary>
        /// DX mode if specified, otherwise null
        /// </summary>
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        /// <summary>
        /// dB report of the DX if specified, otherwise null
        /// </summary>
        [JsonPropertyName("sNR")]
        public int? Snr { get; set; }

        /// <summary>
        /// CW words per minute if specified, otherwise null
        /// </summary>
        [JsonPropertyName("wPM")]
        public int? Wpm { get; set; }

        /// <summary>
        /// Comment field if any, otherwise null
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Report timestamp (UTC)
        /// </summary>
        public DateTime TimestampZ { get; set; }

        [JsonPropertyName("flowStartSeconds")]
        public long FlowStartSeconds => (long)(TimestampZ - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;

        public static bool TryParse(string line, out ClusterSpot spot)
        {
            if (string.IsNullOrWhiteSpace(line) || !line.StartsWith("DX de "))
            {
                spot = null;
                return false;
            }

            if (Program.OutputUnitTests)
            {
                File.AppendAllText("tests.cs", @$"        [Fact]
        public void Test_{Guid.NewGuid().ToString().Replace("-", "_")}()
        {{
            ClusterSpot.TryParse(""{line}"", out var spot).Should().BeTrue();
        }}

");
            }

            var tokens = line[0..^5].Split(new[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);

            try
            {
                var result = new ClusterSpot();

                if (tokens[2].Contains('-'))
                {
                    var split = tokens[2].Split('-');

                    result.ReceiverCallsign = split[0];

                    if (split[1] != "#")
                    {
                        result.ReceiverSsid = int.Parse(split[1]);
                    }
                }
                else
                {
                    result.ReceiverCallsign = tokens[2];
                }

                result.Frequency = (long)(double.Parse(tokens[3]) * 1000.0);

                result.SenderCallsign = tokens[4];

                var commentField = line[39..70].Trim();

                int? offset;
                (result.Mode, result.Snr, result.Wpm, offset, result.Comment) = ProcessCommentField(commentField);

                if (offset != null)
                {
                    result.Frequency += offset.Value;
                }

                var hours = int.Parse(line[^5..^3]);
                var mins = int.Parse(line[^3..^1]);
                result.TimestampZ = DateTime.UtcNow.Date.AddHours(hours).AddMinutes(mins);

                spot = result;
                return true;
            }
            catch (Exception ex)
            {
                spot = null;
                return false;
            }
        }

        private static (string mode, int? dbReport, int? wpm, int? hz, string comment) ProcessCommentField(string commentField)
        {
            var tokens = commentField.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string mode = null, comment = null;
            int? dbReport = null, wpm = null, hz = null;

            if (tokens.Any())
            {
                if (tokens[0] == "CW" || tokens[0] == "RTTY" || tokens[0] == "PSK31" || tokens[0] == "PSK63" || tokens[0] == "FT8")
                {
                    int dbIndex = Array.IndexOf(tokens, "dB");
                    if (dbIndex != -1)
                    {
                        dbReport = int.Parse(tokens[dbIndex - 1]);

                        if (tokens[dbIndex-2] == "-")
                        {
                            dbReport = 0 - dbReport;
                        }
                    }

                    int wpmIndex = Array.IndexOf(tokens, "WPM");
                    if (wpmIndex != -1)
                    {
                        wpm = int.Parse(tokens[wpmIndex - 1]);
                    }

                    if (tokens[0] == "FT8")
                    {
                        int hzOffset = Array.IndexOf(tokens, "Hz");
                        if (hzOffset != -1)
                        {
                            hz = int.Parse(tokens[hzOffset - 1]);
                        }
                    }

                    int commentStartIndex = Math.Max(Math.Max(commentField.IndexOf(" WPM"), commentField.IndexOf(" dB")), commentField.IndexOf(" Hz"));

                    if (commentStartIndex != -1)
                    {
                        comment = commentField[commentStartIndex..].RemoveFirstWord().Trim();
                    }

                    if (string.IsNullOrWhiteSpace(comment))
                    {
                        comment = null;
                    }

                    mode = tokens[0];
                }
                else
                {
                    comment = commentField;
                }
            }

            return (mode, dbReport, wpm, hz, comment);
        }
    }
}