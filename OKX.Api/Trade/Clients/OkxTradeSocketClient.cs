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
    /// OKX delisted instId for this WebSocket channel on 2026-03-26; use InstrumentIdCode.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxTradeOrderPlaceResponse>> PlaceOrderAsync(OkxTradeOrderPlaceRequest request)
    {
        var socketRequest = CreateSocketPlaceOrderRequest(request);
        var req = new OkxSocketRequest<OkxTradeOrderPlaceRequest>(_.RequestId().ToString(), OkxSocketOperation.Order, [socketRequest]);
        return await _.RootQueryAsync<OkxTradeOrderPlaceResponse>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Place orders in a batch. Maximum 20 orders can be placed per request
    /// OKX delisted instId for this WebSocket channel on 2026-03-26; use InstrumentIdCode for each order.
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxTradeOrderPlaceResponse>>> PlaceOrdersAsync(IEnumerable<OkxTradeOrderPlaceRequest> requests)
    {
        var socketRequests = CreateSocketPlaceOrderRequests(requests);
        var req = new OkxSocketRequest<OkxTradeOrderPlaceRequest>(_.RequestId().ToString(), OkxSocketOperation.BatchOrders, socketRequests);
        return await _.RootQueryAsync<IEnumerable<OkxTradeOrderPlaceResponse>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel an incomplete order.
    /// OKX deprecated instId for this WebSocket channel on 2026-04-07; use InstrumentIdCode.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxTradeOrderCancel>> CancelOrderAsync(OkxTradeOrderCancelRequest request)
    {
        var socketRequest = CreateSocketCancelOrderRequest(request);
        var req = new OkxSocketRequest<OkxTradeOrderCancelRequest>(_.RequestId().ToString(), OkxSocketOperation.CancelOrder, [socketRequest]);
        return await _.RootQueryAsync<OkxTradeOrderCancel>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled per request.
    /// OKX deprecated instId for this WebSocket channel on 2026-04-07; use InstrumentIdCode for each order.
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxTradeOrderCancel>>> CancelOrdersAsync(IEnumerable<OkxTradeOrderCancelRequest> requests)
    {
        var socketRequests = CreateSocketCancelOrderRequests(requests);
        var req = new OkxSocketRequest<OkxTradeOrderCancelRequest>(_.RequestId().ToString(), OkxSocketOperation.BatchCancelOrders, socketRequests);
        return await _.RootQueryAsync<IEnumerable<OkxTradeOrderCancel>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend an incomplete order.
    /// OKX deprecated instId for this WebSocket channel on 2026-04-07; use InstrumentIdCode.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxTradeOrderAmend>> AmendOrderAsync(OkxTradeOrderAmendRequest request)
    {
        var socketRequest = CreateSocketAmendOrderRequest(request);
        var req = new OkxSocketRequest<OkxTradeOrderAmendRequest>(_.RequestId().ToString(), OkxSocketOperation.AmendOrder, [socketRequest]);
        return await _.RootQueryAsync<OkxTradeOrderAmend>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended per request.
    /// OKX deprecated instId for this WebSocket channel on 2026-04-07; use InstrumentIdCode for each order.
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxTradeOrderAmend>>> AmendOrdersAsync(IEnumerable<OkxTradeOrderAmendRequest> requests)
    {
        var socketRequests = CreateSocketAmendOrderRequests(requests);
        var req = new OkxSocketRequest<OkxTradeOrderAmendRequest>(_.RequestId().ToString(), OkxSocketOperation.BatchAmendOrders, socketRequests);
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

    private static OkxTradeOrderPlaceRequest CreateSocketPlaceOrderRequest(OkxTradeOrderPlaceRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (request.InstrumentIdCode is null)
            throw new ArgumentException("OKX delisted instId for WebSocket place order channels on 2026-03-26. Set InstrumentIdCode and map it via GetInstrumentsAsync before sending the request.", nameof(request));

        return request with
        {
            InstrumentId = null,
            Tag = OkxConstants.BrokerId
        };
    }

    private static List<OkxTradeOrderPlaceRequest> CreateSocketPlaceOrderRequests(IEnumerable<OkxTradeOrderPlaceRequest> requests)
    {
        if (requests is null)
            throw new ArgumentNullException(nameof(requests));
        return requests.Select(CreateSocketPlaceOrderRequest).ToList();
    }

    private static OkxTradeOrderAmendRequest CreateSocketAmendOrderRequest(OkxTradeOrderAmendRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (request.InstrumentIdCode is null)
            throw new ArgumentException("OKX deprecated instId for WebSocket amend order channels on 2026-04-07. Set InstrumentIdCode and map it via GetInstrumentsAsync before sending the request.", nameof(request));

#pragma warning disable CS0618
        return request with
        {
            InstrumentId = null
        };
#pragma warning restore CS0618
    }

    private static List<OkxTradeOrderAmendRequest> CreateSocketAmendOrderRequests(IEnumerable<OkxTradeOrderAmendRequest> requests)
    {
        if (requests is null)
            throw new ArgumentNullException(nameof(requests));
        return requests.Select(CreateSocketAmendOrderRequest).ToList();
    }

    private static OkxTradeOrderCancelRequest CreateSocketCancelOrderRequest(OkxTradeOrderCancelRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (request.InstrumentIdCode is null)
            throw new ArgumentException("OKX deprecated instId for WebSocket cancel order channels on 2026-04-07. Set InstrumentIdCode and map it via GetInstrumentsAsync before sending the request.", nameof(request));

#pragma warning disable CS0618
        return request with
        {
            InstrumentId = null
        };
#pragma warning restore CS0618
    }

    private static List<OkxTradeOrderCancelRequest> CreateSocketCancelOrderRequests(IEnumerable<OkxTradeOrderCancelRequest> requests)
    {
        if (requests is null)
            throw new ArgumentNullException(nameof(requests));
        return requests.Select(CreateSocketCancelOrderRequest).ToList();
    }
}
