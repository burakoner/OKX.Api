using OKX.Api.Models;
using OKX.Api.Models.System;

namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api System Client
/// </summary>
public class OKXWebSocketApiSystemClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiSystemClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    /// <summary>
    /// Get the status of system maintenance and push when the system maintenance status changes. First subscription: "Push the latest change data"; every time there is a state change, push the changed content
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSystemStatusAsync(Action<OkxStatus> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxStatus>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "status",
        });
        return await this.RootClient.RootSubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }
}