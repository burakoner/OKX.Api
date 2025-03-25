namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX WebSocket Api Copy Trading Client
/// </summary>
public class OkxCopyTradingSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;

    /// <summary>
    /// The notification when failing to lead trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLeadTradingNotificationsAsync(
        Action<OkxCopyTradingNotification> onData,
        OkxInstrumentType instrumentType,
        string? instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToLeadTradingNotificationsAsync(onData, [new(instrumentType, "",instrumentId)], ct).ConfigureAwait(false);

    /// <summary>
    /// The notification when failing to lead trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToLeadTradingNotificationsAsync(
        Action<OkxCopyTradingNotification> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxCopyTradingNotification>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "copytrading-lead-notification",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

}