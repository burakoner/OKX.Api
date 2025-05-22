namespace OKX.Api.Account;

/// <summary>
/// OKX WebSocket Api Trading Account Client
/// </summary>
public class OkxAccountSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    /// <summary>
    /// Retrieve account information. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="currency">Currency</param>
    /// <param name="updateInterval">0: only push due to account events
    /// The data will be pushed both by events and regularly if this field is omitted or set to other values than 0.
    /// The following format should be strictly obeyed when using this field.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAccountUpdatesAsync(
        Action<OkxAccountBalance> onData,
        string? currency = null,
        int? updateInterval = null,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxAccountBalance>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "account",
            Currency = currency,
            ExtraParameters = updateInterval.HasValue 
                ? new Dictionary<string, string> { { "interval", updateInterval.Value.ToOkxString() } }
                : null
        });

        return await _.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<OkxAccountPosition> onData, OkxInstrumentType instrumentType, string? instrumentFamily = null, string? instrumentId = null, CancellationToken ct = default)
        => await SubscribeToPositionUpdatesAsync(onData, [new(instrumentType, instrumentFamily, instrumentId)], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionUpdatesAsync(Action<OkxAccountPosition> onData, IEnumerable<OkxSocketSymbolRequest> symbols, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxAccountPosition>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "positions",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve account balance and position information. Data will be pushed when triggered by events such as filled order, funding transfer.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(Action<OkxAccountPositionBalanceUpdate> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxAccountPositionBalanceUpdate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "balance_and_position"
        });
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// This push channel is only used as a risk warning, and is not recommended as a risk judgment for strategic trading
    /// In the case that the market is volatile, there may be the possibility that the position has been liquidated at the same time that this message is pushed.
    /// The warning is sent when a position is at risk of liquidation for isolated margin positions. The warning is sent when all the positions are at risk of liquidation for cross-margin positions.
    /// The exchange will implement concurrent connection limit in February. Please refer to Restrict number of connections per private WebSocket channel
    /// Restrict number of connections per private WebSocket channel https://www.okx.com/docs-v5/log_en/#upcoming-changes-restrict-number-of-connections-per-private-websocket-channel
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionRiskUpdatesAsync(OkxInstrumentType instrumentType, Action<OkxAccountPosition> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxAccountPosition>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "liquidation-warning",
            InstrumentType = instrumentType
        });
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve account greeks information. Data will be pushed when triggered by events such as increase/decrease positions or cash balance in account, and will also be pushed in regular interval according to subscription granularity.
    /// The exchange will implement concurrent connection limit in February. Please refer to Restrict number of connections per private WebSocket channel
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAccountGreeksUpdatesAsync(Action<OkxAccountGreeks> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxAccountGreeks>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "account-greeks",
        });
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }
}