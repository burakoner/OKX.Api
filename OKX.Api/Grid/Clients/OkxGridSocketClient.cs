namespace OKX.Api.Grid;

/// <summary>
/// OKX WebSocket Api Grid Trading Client
/// </summary>
public class OkxGridSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    /// <summary>
    /// Retrieve spot grid algo orders. Data will be pushed when triggered by events such as placing/canceling order. It will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="algoOrderId">Algo Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSpotOrdersAsync(
        Action<OkxGridSpotOrderUpdate> onData,
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? algoOrderId = null,
        CancellationToken ct = default)
    => await SubscribeToSpotOrdersAsync(onData, [new() {
        InstrumentType = instrumentType,
        InstrumentId = instrumentId,
        AlgoOrderId = algoOrderId
    }], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve spot grid algo orders. Data will be pushed when triggered by events such as placing/canceling order. It will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSpotOrdersAsync(
        Action<OkxGridSpotOrderUpdate> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxGridSpotOrderUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "grid-orders-spot",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
            AlgoOrderId = symbol.AlgoOrderId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve contract grid algo orders. Data will be pushed when triggered by events such as placing/canceling order. It will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument type</param>
    /// <param name="instrumentId"></param>
    /// <param name="algoOrderId">Algo Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractOrdersAsync(
        Action<OkxGridContractOrderUpdate> onData,
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        string? algoOrderId = null,
        CancellationToken ct = default)
    => await SubscribeToContractOrdersAsync(onData, [new() {
        InstrumentType = instrumentType,
        InstrumentId = instrumentId,
        AlgoOrderId = algoOrderId
    }], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve contract grid algo orders. Data will be pushed when triggered by events such as placing/canceling order. It will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToContractOrdersAsync(
        Action<OkxGridContractOrderUpdate> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxGridContractOrderUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "grid-orders-contract",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve contract grid positions. Data will be pushed when triggered by events such as placing/canceling order.
    /// Please ignore the empty data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="algoOrderId">Algo Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionsAsync(
        Action<OkxGridAlgoPositionUpdate> onData,
        string algoOrderId,
        CancellationToken ct = default)
        => await SubscribeToPositionsAsync(onData, [algoOrderId], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve contract grid positions. Data will be pushed when triggered by events such as placing/canceling order.
    /// Please ignore the empty data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="algoOrderIds">Algo Order ID List</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionsAsync(
        Action<OkxGridAlgoPositionUpdate> onData,
        IEnumerable<string> algoOrderIds,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxGridAlgoPositionUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var algoOrderId in algoOrderIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "grid-positions",
            AlgoOrderId = algoOrderId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve grid sub orders. Data will be pushed when triggered by events such as placing order.
    /// Please ignore the empty data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="algoOrderId">Algo Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSubOrdersAsync(
        Action<OkxGridAlgoSubOrderUpdate> onData,
        string algoOrderId,
        CancellationToken ct = default)
        => await SubscribeToSubOrdersAsync(onData, [algoOrderId], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve grid sub orders. Data will be pushed when triggered by events such as placing order.
    /// Please ignore the empty data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="algoOrderIds">Algo Order ID List</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToSubOrdersAsync(
        Action<OkxGridAlgoSubOrderUpdate> onData,
        IEnumerable<string> algoOrderIds,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxGridAlgoSubOrderUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var algoOrderId in algoOrderIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "grid-sub-orders",
            AlgoOrderId = algoOrderId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }
}