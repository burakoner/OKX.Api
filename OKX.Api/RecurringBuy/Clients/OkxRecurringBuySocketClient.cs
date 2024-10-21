namespace OKX.Api.RecurringBuy;

/// <summary>
/// OKX WebSocket Api RecurringBuy Client
/// </summary>
public class OkxRecurringBuySocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;
    internal OkxWebSocketApiOptions Options { get { return _.Options; } }

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
        Action<OkxRecurringBuyOrder> onData,
        OkxInstrumentType instrumentType,
        long? algoOrderId = null,
        CancellationToken ct = default)
    {
        var args = new OkxSocketRequestArgument
        {
            Channel = "algo-recurring-buy",
            InstrumentType = instrumentType,
        };
        if(algoOrderId.HasValue)args.AlgoOrderId = algoOrderId.ToOkxString();
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, args);

        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxRecurringBuyOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        return await _.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }
}