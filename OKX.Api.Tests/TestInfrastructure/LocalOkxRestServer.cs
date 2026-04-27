using System.Net;
using System.Net.Sockets;
using System.Text;

namespace OKX.Api.Tests.TestInfrastructure;

internal sealed class LocalOkxRestServer : IDisposable
{
    private readonly HttpListener _listener;
    private readonly Dictionary<string, string> _responses;
    private readonly Task _serverTask;

    public string BaseAddress { get; }
    public List<CapturedRestRequest> Requests { get; } = [];

    public LocalOkxRestServer(Dictionary<string, string> responses)
    {
        _responses = responses;

        var port = GetFreePort();
        BaseAddress = $"http://127.0.0.1:{port}";

        _listener = new HttpListener();
        _listener.Prefixes.Add($"{BaseAddress}/");
        _listener.Start();

        _serverTask = Task.Run(HandleLoopAsync);
    }

    public void Dispose()
    {
        if (_listener.IsListening)
            _listener.Stop();

        _listener.Close();

        try
        {
            _serverTask.Wait(TimeSpan.FromSeconds(2));
        }
        catch
        {
            // Ignore shutdown exceptions from the listener loop.
        }
    }

    private async Task HandleLoopAsync()
    {
        while (_listener.IsListening)
        {
            HttpListenerContext? context = null;
            try
            {
                context = await _listener.GetContextAsync().ConfigureAwait(false);
            }
            catch
            {
                break;
            }

            if (context is not null)
                await HandleAsync(context).ConfigureAwait(false);
        }
    }

    private async Task HandleAsync(HttpListenerContext context)
    {
        string body;
        using (var reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
            body = await reader.ReadToEndAsync().ConfigureAwait(false);

        Requests.Add(new CapturedRestRequest(
            context.Request.HttpMethod,
            context.Request.Url?.AbsolutePath ?? string.Empty,
            context.Request.Url?.Query ?? string.Empty,
            body));

        var responseKey = $"{context.Request.HttpMethod} {context.Request.Url?.AbsolutePath}";
        if (!_responses.TryGetValue(responseKey, out var payload))
            payload = "{\"code\":\"404\",\"msg\":\"Missing test route\",\"data\":[]}";

        context.Response.StatusCode = 200;
        context.Response.ContentType = "application/json";

        var buffer = Encoding.UTF8.GetBytes(payload);
        await context.Response.OutputStream.WriteAsync(buffer).ConfigureAwait(false);
        context.Response.Close();
    }

    private static int GetFreePort()
    {
        using var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        return ((IPEndPoint)listener.LocalEndpoint).Port;
    }
}

internal sealed record CapturedRestRequest(string Method, string Path, string Query, string Body);
