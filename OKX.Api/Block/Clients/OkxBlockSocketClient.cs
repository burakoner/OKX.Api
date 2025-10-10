namespace OKX.Api.Block;

/// <summary>
/// OKX WebSocket Api Block Trading Client
/// </summary>
public class OkxBlockSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;
    internal OkxWebSocketApiOptions Options { get { return _.Options; } }

    /// <summary>
    /// Retrieve the RFQs sent or received by the user. Data will be pushed whenever the user sends or receives an RFQ.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToRfqsUpdatesAsync(
        Action<OkxBlockRfq> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxBlockRfq>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, [new("rfqs")]);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Unique identifier of the message
    /// Provided by client.It will be returned in response message for identifying the corresponding request.
    /// A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToQuotesUpdatesAsync(
        Action<OkxBlockQuote> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxBlockQuote>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, [new("quotes")]);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve user's block trades data. All the legs in the same block trade are included in the same update. Data will be pushed whenever there is a block trade that the user is a counterparty for.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToUserStructureTradesAsync(
        Action<OkxBlockStructureTradeUpdate> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxBlockStructureTradeUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, [new("struc-block-trades")]);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the recent block trades data in OKX. All the legs in the same block trade are included in the same update. The data will be pushed 15 minutes after the block trade execution.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPublicStructureTradesAsync(
       Action<OkxBlockPublicStructureTradeUpdate> onData,
       CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxBlockPublicStructureTradeUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, [new("public-struc-block-trades")]);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the recent block trades data by individual legs. Each leg in a block trade is pushed in a separate update. The data will be pushed 15 minutes after the block trade execution.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPublicBlockTradesAsync(Action<OkxBlockPublicTradeUpdate> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToPublicBlockTradesAsync(onData, [instrumentId], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the recent block trades data by individual legs. Each leg in a block trade is pushed in a separate update. The data will be pushed 15 minutes after the block trade execution.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPublicBlockTradesAsync(Action<OkxBlockPublicTradeUpdate> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxBlockPublicTradeUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "public-block-trades",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// The data will be pushed when triggered by transaction execution event. In addition, it will also be pushed in 5 minutes interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<OkxBlockTicker> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToTickersAsync(onData, [instrumentId], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// The data will be pushed when triggered by transaction execution event. In addition, it will also be pushed in 5 minutes interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<OkxBlockTicker> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxBlockTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "block-tickers",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }
}