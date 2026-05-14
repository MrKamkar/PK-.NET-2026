namespace McpServer1Test;

internal class Program
{
    private const string ServerPath = @"c:\Users\kamil\Documents\PK-.NET-2026\Ex10\MCPWeatherServer\bin\Debug\net10.0\MCPWeatherServer.exe";

    static async Task Main()
    {
        await using var client = new McpStdioClient(ServerPath);
        await client.StartAsync();

        await client.SendAsync(new
        {
            jsonrpc = "2.0",
            id = 1,
            method = "tools/list"
        });

        await client.ReadAsync();

        await client.SendAsync(new
        {
            jsonrpc = "2.0",
            id = 2,
            method = "tools/call",
            @params = new
            {
                name = "GetTemperature",
                arguments = new
                {
                    location = "Warsaw",
                    date = "2026-05-15"
                }
            }
        });

        await client.ReadAsync();
    }
}
