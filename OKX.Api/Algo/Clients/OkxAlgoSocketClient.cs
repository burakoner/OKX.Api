namespace OKX.Api.Algo;

/// <summary>
/// OKX WebSocket Api Algo Trading Client
/// </summary>
public class OkxAlgoSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    /// <summary>
    /// Retrieve algo orders (includes trigger order, oco order, conditional order). Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        OkxInstrumentType instrumentType,
        string? instrumentFamily = null,
        string? instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToAlgoOrderUpdatesAsync(onData, [new(instrumentType, instrumentFamily, instrumentId)], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve algo orders (includes trigger order, oco order, conditional order). Data will not be pushed when first subscribed. Data will only be pushed when there are order updates.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        if (onData is null)
            throw new ArgumentNullException(nameof(onData));
        if (symbols is null)
            throw new ArgumentNullException(nameof(symbols));

        var symbolList = symbols.ToList();
        if (symbolList.Count == 0)
            throw new ArgumentException("At least one algo order subscription is required.", nameof(symbols));

        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbolList)
        {
            if (symbol is null)
                throw new ArgumentException("Algo order subscriptions cannot contain null items.", nameof(symbols));
            if (symbol.InstrumentType.IsNotIn(
                OkxInstrumentType.Any,
                OkxInstrumentType.Spot,
                OkxInstrumentType.Margin,
                OkxInstrumentType.Swap,
                OkxInstrumentType.Futures))
                throw new ArgumentOutOfRangeException(nameof(symbols), "Algo orders channel supports ANY, SPOT, MARGIN, SWAP, and FUTURES only.");
            if (!string.IsNullOrWhiteSpace(symbol.InstrumentFamily) &&
                symbol.InstrumentType.IsNotIn(OkxInstrumentType.Swap, OkxInstrumentType.Futures))
                throw new ArgumentException("Instrument family is only applicable to SWAP and FUTURES algo subscriptions.", nameof(symbols));
            if (symbol.InstrumentId is not null && string.IsNullOrWhiteSpace(symbol.InstrumentId))
                throw new ArgumentException("Instrument ID cannot be empty.", nameof(symbols));

            arguments.Add(new OkxSocketRequestArgument
            {
                Channel = "orders-algo",
                InstrumentId = symbol.InstrumentId,
                InstrumentType = symbol.InstrumentType,
                InstrumentFamily = symbol.InstrumentFamily,
            });
        }
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve advance algo orders (includes iceberg order and twap order). Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        OkxInstrumentType instrumentType,
        string? instrumentFamily = null,
        string? instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToAdvanceAlgoOrderUpdatesAsync(onData, [new(instrumentType, instrumentFamily, instrumentId)], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve advance algo orders (including Iceberg order, TWAP order, Trailing order). Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "algo-advance",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

}
