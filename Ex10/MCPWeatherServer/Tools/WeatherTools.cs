using System.ComponentModel;
using ModelContextProtocol.Server;

namespace MCPWeatherServer.Tools;

public class WeatherTools
{
    [McpServerTool]
    [Description("Returns temperature in Celsius for a given place and date.")]
    public string GetTemperature(
        [Description("City or place, e.g. Warsaw")] string location,
        [Description("Date in YYYY-MM-DD format")] string date)
    {
        var seed = Math.Abs(HashCode.Combine(location.ToLowerInvariant(), date));
        var temp = (seed % 30) - 5; // -5..24
        
        return $"{temp}°C";
    }

    [McpServerTool]
    [Description("Returns information whether it will rain for a given place and date.")]
    public string WillItRain(
        [Description("City or place, e.g. Warsaw")] string location,
        [Description("Date in YYYY-MM-DD format")] string date)
    {
        var seed = Math.Abs(HashCode.Combine("rain", location.ToLowerInvariant(), date));
        var rain = seed % 2 == 0;
        
        return rain
            ? $"Yes, rain is expected in {location} on {date}."
            : $"No, rain is not expected in {location} on {date}.";
    }
}
