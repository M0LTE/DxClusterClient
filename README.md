# DxClusterClient
A .NET 5 library which implements a DX Cluster client.

Designed for (and only tested with) VE7CC's DX cluster.

# Usage
```
nuget install DxClusterClient
```

then

```csharp
namespace DemoApp
{
    using ClusterSkimmer;
    using Microsoft.Extensions.Logging;

    class Program
    {
        static void Main(string[] args)
        {
            var logger = LoggerFactory
                .Create(logging => logging.AddConsole())
                .CreateLogger<ClusterClient>();

            var client = new ClusterClient(logger);

            client.OnClusterSpotReceived += (c, clusterSpot) =>
            {
                System.Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(clusterSpot));
            };

            client.Start(callsign: "N0CALL"); // put your own callsign here
            
            System.Threading.Thread.CurrentThread.Join();
        }
    }
}
```

Output:
```
{"receiverCallsign":"K9LC","frequency":7010000,"senderCallsign":"VE2ACP","mode":"CW","sNR":15,"wPM":21,"Comment":"CQ","TimestampZ":"2021-01-05T16:02:00Z","flowStartSeconds":1609862520}
{"receiverCallsign":"K9LC","frequency":14038000,"senderCallsign":"GW0ADC","mode":"CW","sNR":9,"wPM":17,"Comment":"CQ","TimestampZ":"2021-01-05T16:02:00Z","flowStartSeconds":1609862520}
{"receiverCallsign":"DL1YCL","frequency":7075252,"senderCallsign":"TA7OYG","mode":"FT8","sNR":-8,"wPM":null,"Comment":null,"TimestampZ":"2021-01-05T16:00:00Z","flowStartSeconds":1609862400}
...
```

# Notes
This isn't battle tested.

It should be pretty robust, there's an exception handler inside. Watch for warning log messages coming out of the logger. If you spot something's up, please raise an issue and supply the spot that broke it.

GitHub Actions is wired up to nuget.org to generate and release an updated package. Increment the version number in the project file to do a new release.

To stop, pass in a CancellationToken to `client.Start()` if you wish.

To turn on/off various kinds of spots, pass optional parameters to `client.Start()`, e.g. `ft8: false`.
