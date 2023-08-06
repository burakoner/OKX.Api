using OKX.Api.Models;
using OKX.Api.Models.TradingAccount;

namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Trading Account Client
/// </summary>
public class OKXWebSocketApiTradingAccountClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiTradingAccountClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    /// <summary>
    /// Retrieve account information. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToAccountUpdatesAsync(
        Action<OkxAccountBalance> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxAccountBalance>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "account"
        });
        return await this.RootClient.RootSubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
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
    public virtual async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionUpdatesAsync(
        Action<OkxPosition> onData,
        OkxInstrumentType instrumentType,
        string instrumentFamily = null,
        string instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToPositionUpdatesAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPositionUpdatesAsync(
        Action<OkxPosition> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxPosition>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
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
        return await this.RootClient.RootSubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve account balance and position information. Data will be pushed when triggered by events such as filled order, funding transfer.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(
        Action<OkxPositionRisk> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxPositionRisk>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "balance_and_position"
        });
        return await this.RootClient.RootSubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    // TODO: Position risk warning
    // TODO: Account greeks channel

}