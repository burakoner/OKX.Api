namespace OKX.Api.Trade;

/// <summary>
/// OKX WebSocket Api Trade Client
/// </summary>
public class OkxTradeSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;
    internal OkxWebSocketApiOptions Options { get { return _.Options; } }

    /// <summary>
    /// Retrieve order information. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderUpdatesAsync(
        Action<OkxTradeOrder> onData,
        OkxInstrumentType instrumentType,
        string? instrumentFamily = null,
        string? instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToOrderUpdatesAsync(onData, [new(instrumentType, instrumentFamily, instrumentId)], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve order information. Data will not be pushed when first subscribed. Data will only be pushed when there are order updates.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderUpdatesAsync(
        Action<OkxTradeOrder> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "orders",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });

        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxTradeOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve transaction information. Data will not be pushed when first subscribed. Data will only be pushed when there are order book fill events, where tradeId > 0.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToFillsAsync(
        Action<OkxTradeFill> onData,
        string? instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToFillsAsync(onData, [instrumentId], ct).ConfigureAwait(false);
    
    /// <summary>
    /// Retrieve transaction information. Data will not be pushed when first subscribed. Data will only be pushed when there are order book fill events, where tradeId > 0.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">Instrument IDs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToFillsAsync(
        Action<OkxTradeFill> onData,
        IEnumerable<string?> instrumentIds,
        CancellationToken ct = default)
    {
        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "fills",
            InstrumentId = instrumentId,
        });

        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxTradeFill>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await _.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// You can place an order only if you have sufficient funds.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxTradeOrderPlaceResponse>> PlaceOrderAsync(OkxTradeOrderPlaceRequest request)
    {
        request.Tag = OkxConstants.BrokerId;
        var req = new OkxSocketRequest<OkxTradeOrderPlaceRequest>(_.RequestId().ToString(), OkxSocketOperation.Order, [request]);
        return await _.RootQueryAsync<OkxTradeOrderPlaceResponse>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Place orders in a batch. Maximum 20 orders can be placed per request
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxTradeOrderPlaceResponse>>> PlaceOrdersAsync(IEnumerable<OkxTradeOrderPlaceRequest> requests)
    {
        foreach (var order in requests) order.Tag = OkxConstants.BrokerId;
        var req = new OkxSocketRequest<OkxTradeOrderPlaceRequest>(_.RequestId().ToString(), OkxSocketOperation.BatchOrders, requests);
        return await _.RootQueryAsync<IEnumerable<OkxTradeOrderPlaceResponse>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel an incomplete order
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxTradeOrderCancel>> CancelOrderAsync(OkxTradeOrderCancelRequest request)
    {
        var req = new OkxSocketRequest<OkxTradeOrderCancelRequest>(_.RequestId().ToString(), OkxSocketOperation.CancelOrder, [request]);
        return await _.RootQueryAsync<OkxTradeOrderCancel>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled per request.
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxTradeOrderCancel>>> CancelOrdersAsync(IEnumerable<OkxTradeOrderCancelRequest> requests)
    {
        var req = new OkxSocketRequest<OkxTradeOrderCancelRequest>(_.RequestId().ToString(), OkxSocketOperation.BatchAmendOrders, requests);
        return await _.RootQueryAsync<IEnumerable<OkxTradeOrderCancel>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend an incomplete order.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxTradeOrderAmend>> AmendOrderAsync(OkxTradeOrderAmendRequest request)
    {
        var req = new OkxSocketRequest<OkxTradeOrderAmendRequest>(_.RequestId().ToString(), OkxSocketOperation.AmendOrder, [request]);
        return await _.RootQueryAsync<OkxTradeOrderAmend>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended per request.
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxTradeOrderAmend>>> AmendOrdersAsync(IEnumerable<OkxTradeOrderAmendRequest> requests)
    {
        var req = new OkxSocketRequest<OkxTradeOrderAmendRequest>(_.RequestId().ToString(), OkxSocketOperation.BatchAmendOrders, requests);
        return await _.RootQueryAsync<IEnumerable<OkxTradeOrderAmend>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel all the MMP pending orders of an instrument family.
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxBooleanResponse>> MassCancelAsync(OkxTradeMassCancelRequest request)
    {
        var req = new OkxSocketRequest<OkxTradeMassCancelRequest>(_.RequestId().ToString(), OkxSocketOperation.MassCancel, [request]);
        return await _.RootQueryAsync<OkxBooleanResponse>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

}