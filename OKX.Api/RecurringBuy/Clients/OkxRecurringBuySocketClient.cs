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
        var subscription = CreateOrderUpdatesSubscription(instrumentType, algoOrderId);
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxRecurringBuyOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        return await _.RootSubscribeAsync(subscription.Endpoint, subscription.Request, null, subscription.Authenticated, internalHandler, ct).ConfigureAwait(false);
    }

    private static (OkxSocketEndpoint Endpoint, bool Authenticated, OkxSocketRequest Request) CreateOrderUpdatesSubscription(OkxInstrumentType instrumentType, long? algoOrderId)
    {
        if (instrumentType is not OkxInstrumentType.Spot and not OkxInstrumentType.Any)
            throw new ArgumentException("Recurring buy orders channel only supports SPOT or ANY instrument types.", nameof(instrumentType));

        var args = new OkxSocketRequestArgument
        {
            Channel = "algo-recurring-buy",
            InstrumentType = instrumentType,
        };
        if (algoOrderId.HasValue)
            args.AlgoOrderId = algoOrderId.ToOkxString();

        return (OkxSocketEndpoint.Business, true, new OkxSocketRequest(OkxSocketOperation.Subscribe, args));
    }
}
