using System.Diagnostics;
using System.Text.Json;

namespace McpServer1Test;

internal sealed class McpStdioClient : IAsyncDisposable
{
    private readonly string _serverPath;
    private Process? _process;
    private StreamWriter? _input;
    private StreamReader? _output;

    public McpStdioClient(string serverPath)
    {
        _serverPath = serverPath;
    }

    public Task StartAsync()
    {
        _process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = _serverPath,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        _process.Start();
        _input = _process.StandardInput;
        _output = _process.StandardOutput;

        return Task.CompletedTask;
    }

    public async Task SendAsync(object message)
    {
        if (_input is null)
            throw new InvalidOperationException("Client is not started.");

        var json = JsonSerializer.Serialize(message);
        Console.WriteLine($"\n➡️ SEND:\n{json}");
        
        await _input.WriteLineAsync(json);
        await _input.FlushAsync();
    }

    public async Task<string?> ReadAsync()
    {
        if (_output is null)
            throw new InvalidOperationException("Client is not started.");

        var line = await _output.ReadLineAsync();
        Console.WriteLine($"\n⬅️ RECV:\n{line}");
        
        return line;
    }

    public async ValueTask DisposeAsync()
    {
        if (_process is null)
            return;

        try
        {
            if (!_process.HasExited)
            {
                _process.Kill();
                await _process.WaitForExitAsync();
            }
        }
        catch (InvalidOperationException)
        {
            // Process was never successfully started
        }
        finally
        {
            _process.Dispose();
        }
    }
}
