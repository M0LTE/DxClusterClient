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

            client.Start(callsign: "N0CALL");
        }
    }
}
```

# Notes
This isn't battle tested.

It should be pretty robust, there's an exception handler inside. Watch for warning log messages coming out of the logger. If you spot something's up, please raise an issue and supply the spot that broke it.

GitHub Actions is wired up to nuget.org to generate and release an updated package. Increment the version number in the project file to do a new release.
