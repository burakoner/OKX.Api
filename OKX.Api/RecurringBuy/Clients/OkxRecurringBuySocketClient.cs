using OKX.Api.Common.Enums;
using OKX.Api.Common.Models;
using OKX.Api.RecurringBuy.Models;

namespace OKX.Api.RecurringBuy.Clients;

/// <summary>
/// OKX WebSocket Api RecurringBuy Client
/// </summary>
public class OkxRecurringBuySocketClient(OKXWebSocketApiClient root)
{
    // Internal
    internal OKXWebSocketApiClient Root { get; } = root;
    internal OkxWebSocketApiOptions Options { get { return Root.Options; } }

    /// <summary>
    /// Retrieve recurring buy orders. 
    /// Data will be pushed when triggered by events. 
    /// It will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData"></param>
    /// <param name="instrumentType"></param>
    /// <param name="algoOrderId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderUpdatesAsync(
        Action<OkxRecurringOrder> onData,
        OkxInstrumentType instrumentType,
        long? algoOrderId = null,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxRecurringOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var args = new OkxSocketRequestArgument
        {
            Channel = "algo-recurring-buy",
            InstrumentType = instrumentType,
        };
        if(algoOrderId.HasValue)args.AlgoOrderId = algoOrderId.ToOkxString();
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, args);

        return await Root.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }
}