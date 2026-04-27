namespace OKX.Api.Spread;

/// <summary>
/// OKX WebSocket Api Spread Trading Client
/// </summary>
public class OkxSpreadSocketClient(OkxWebSocketApiClient root)
{
    // Internal
    internal OkxWebSocketApiClient _ { get; } = root;
    internal OkxWebSocketApiOptions Options { get { return _.Options; } }

    /// <summary>
    /// Retrieve spread order information. Data will not be pushed when first subscribed.
    /// Data will only be pushed when triggered by events such as placing/canceling orders.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadId">spread ID filter</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderUpdatesAsync(
        Action<OkxSpreadOrder> onData,
        string? spreadId = null,
        CancellationToken ct = default)
    {
        var subscription = CreateOrderUpdatesSubscription(spreadId);
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxSpreadOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        return await _.RootSubscribeAsync(subscription.Endpoint, subscription.Request, null, subscription.Authenticated, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve spread trade information. Data will not be pushed when first subscribed.
    /// Data will only be pushed after final settlement.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadId">spread ID filter</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradeUpdatesAsync(
        Action<OkxSpreadTrade> onData,
        string? spreadId = null,
        CancellationToken ct = default)
    {
        var subscription = CreateTradeUpdatesSubscription(spreadId);
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxSpreadTrade>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        return await _.RootSubscribeAsync(subscription.Endpoint, subscription.Request, null, subscription.Authenticated, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Place a new spread order.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxSpreadOrderPlace>> PlaceOrderAsync(OkxSpreadOrderPlaceRequest request)
    {
        var socketRequest = CreateSocketPlaceOrderRequest(request);
        var req = new OkxSocketRequest<OkxSpreadOrderPlaceRequest>(_.RequestId().ToString(), OkxSocketOperation.SpreadOrder, [socketRequest]);
        return await _.RootQueryAsync<OkxSpreadOrderPlace>(OkxSocketEndpoint.Business, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend an incomplete spread order.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxSpreadOrderAmend>> AmendOrderAsync(OkxSpreadOrderAmendRequest request)
    {
        var req = new OkxSocketRequest<OkxSpreadOrderAmendRequest>(_.RequestId().ToString(), OkxSocketOperation.SpreadAmendOrder, [request]);
        return await _.RootQueryAsync<OkxSpreadOrderAmend>(OkxSocketEndpoint.Business, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel an incomplete spread order.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxSpreadOrderCancel>> CancelOrderAsync(OkxSpreadOrderCancelRequest request)
    {
        var req = new OkxSocketRequest<OkxSpreadOrderCancelRequest>(_.RequestId().ToString(), OkxSocketOperation.SpreadCancelOrder, [request]);
        return await _.RootQueryAsync<OkxSpreadOrderCancel>(OkxSocketEndpoint.Business, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel all pending spread orders. If no spreadId is provided, all pending spread orders under the account are targeted.
    /// </summary>
    /// <param name="spreadId">spread ID filter</param>
    /// <returns></returns>
    public async Task<CallResult<OkxBooleanResponse>> CancelOrdersAsync(string? spreadId = null)
        => await CancelOrdersAsync(new OkxSpreadMassCancelRequest { SpreadId = spreadId }).ConfigureAwait(false);

    /// <summary>
    /// Cancel all pending spread orders.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxBooleanResponse>> CancelOrdersAsync(OkxSpreadMassCancelRequest request)
    {
        var req = new OkxSocketRequest<OkxSpreadMassCancelRequest>(_.RequestId().ToString(), OkxSocketOperation.SpreadMassCancel, [request]);
        return await _.RootQueryAsync<OkxBooleanResponse>(OkxSocketEndpoint.Business, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve spread order book data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadId">spread ID</param>
    /// <param name="orderBookType">Order Book Type. Supports BBO_TBT, OrderBook_5, and OrderBook_l2_TBT.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(
        Action<OkxSpreadOrderBook> onData,
        string spreadId,
        OkxOrderBookType orderBookType,
        CancellationToken ct = default)
        => await SubscribeToOrderBookAsync(onData, [spreadId], orderBookType, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve spread order book data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadIds">Spread IDs</param>
    /// <param name="orderBookType">Order Book Type. Supports BBO_TBT, OrderBook_5, and OrderBook_l2_TBT.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(
        Action<OkxSpreadOrderBook> onData,
        IEnumerable<string> spreadIds,
        OkxOrderBookType orderBookType,
        CancellationToken ct = default)
    {
        var subscription = CreateOrderBookSubscription(spreadIds, orderBookType);
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketSpreadOrderBookUpdate>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                if (d is null) continue;
                d.SpreadId = data.Data.Arguments?.SpreadId ?? string.Empty;
                d.Action = data.Data.Action;
                onData(d);
            }
        });

        return await _.RootSubscribeAsync(subscription.Endpoint, subscription.Request, null, subscription.Authenticated, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the recent public spread trades. Data will be pushed whenever there is a trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadId">spread ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPublicTradesAsync(Action<OkxSpreadPublicTrade> onData, string spreadId, CancellationToken ct = default)
        => await SubscribeToPublicTradesAsync(onData, [spreadId], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the recent public spread trades. Data will be pushed whenever there is a trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadIds">Spread IDs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPublicTradesAsync(Action<OkxSpreadPublicTrade> onData, IEnumerable<string> spreadIds, CancellationToken ct = default)
    {
        var subscription = CreatePublicSubscription("sprd-public-trades", spreadIds);
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxSpreadPublicTrade>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        return await _.RootSubscribeAsync(subscription.Endpoint, subscription.Request, null, subscription.Authenticated, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the latest spread ticker data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadId">spread ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<OkxSpreadTicker> onData, string spreadId, CancellationToken ct = default)
        => await SubscribeToTickersAsync(onData, [spreadId], ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the latest spread ticker data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadIds">Spread IDs</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<OkxSpreadTicker> onData, IEnumerable<string> spreadIds, CancellationToken ct = default)
    {
        var subscription = CreatePublicSubscription("sprd-tickers", spreadIds);
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxSpreadTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                if (d is not null) onData(d);
        });

        return await _.RootSubscribeAsync(subscription.Endpoint, subscription.Request, null, subscription.Authenticated, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve spread candlestick data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadId">spread ID</param>
    /// <param name="period">Period</param>
    /// <param name="utc">Use UTC-open candles</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(
        Action<OkxSpreadCandlestick> onData,
        string spreadId,
        OkxPeriod period,
        bool utc = false,
        CancellationToken ct = default)
        => await SubscribeToCandlesticksAsync(onData, [spreadId], period, utc, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve spread candlestick data.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="spreadIds">Spread IDs</param>
    /// <param name="period">Period</param>
    /// <param name="utc">Use UTC-open candles</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(
        Action<OkxSpreadCandlestick> onData,
        IEnumerable<string> spreadIds,
        OkxPeriod period,
        bool utc = false,
        CancellationToken ct = default)
    {
        var channel = $"sprd-candle{MapConverter.GetString(period)}{(utc ? "utc" : "")}";
        var subscription = CreatePublicSubscription(channel, spreadIds);
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxSpreadCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                if (d is null) continue;
                d.SpreadId = data.Data.Arguments?.SpreadId ?? string.Empty;
                onData(d);
            }
        });

        return await _.RootSubscribeAsync(subscription.Endpoint, subscription.Request, null, subscription.Authenticated, internalHandler, ct).ConfigureAwait(false);
    }

    private static (OkxSocketEndpoint Endpoint, bool Authenticated, OkxSocketRequest Request) CreateOrderUpdatesSubscription(string? spreadId)
        => CreatePrivateSubscription("sprd-orders", spreadId);

    private static (OkxSocketEndpoint Endpoint, bool Authenticated, OkxSocketRequest Request) CreateTradeUpdatesSubscription(string? spreadId)
        => CreatePrivateSubscription("sprd-trades", spreadId);

    private static (OkxSocketEndpoint Endpoint, bool Authenticated, OkxSocketRequest Request) CreateOrderBookSubscription(IEnumerable<string> spreadIds, OkxOrderBookType orderBookType)
        => CreatePublicSubscription(MapOrderBookChannel(orderBookType), spreadIds);

    private static (OkxSocketEndpoint Endpoint, bool Authenticated, OkxSocketRequest Request) CreatePrivateSubscription(string channel, string? spreadId)
    {
        var arg = new OkxSocketRequestArgument { Channel = channel };
        if (!string.IsNullOrWhiteSpace(spreadId))
            arg.SpreadId = spreadId;

        return (OkxSocketEndpoint.Business, true, new OkxSocketRequest(OkxSocketOperation.Subscribe, arg));
    }

    private static (OkxSocketEndpoint Endpoint, bool Authenticated, OkxSocketRequest Request) CreatePublicSubscription(string channel, IEnumerable<string> spreadIds)
    {
        if (spreadIds is null)
            throw new ArgumentNullException(nameof(spreadIds));

        var arguments = spreadIds
            .Select(x => string.IsNullOrWhiteSpace(x)
                ? throw new ArgumentException("Spread IDs cannot be null or whitespace.", nameof(spreadIds))
                : new OkxSocketRequestArgument
                {
                    Channel = channel,
                    SpreadId = x,
                })
            .ToList();

        if (arguments.Count == 0)
            throw new ArgumentException("At least one spread ID is required.", nameof(spreadIds));

        return (OkxSocketEndpoint.Business, false, new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments));
    }

    private static OkxSpreadOrderPlaceRequest CreateSocketPlaceOrderRequest(OkxSpreadOrderPlaceRequest request)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        if (string.IsNullOrWhiteSpace(request.SpreadId))
            throw new ArgumentException("SpreadId is required for spread websocket place-order requests.", nameof(request));

        return request with
        {
            Tag = OkxConstants.BrokerId
        };
    }

    private static string MapOrderBookChannel(OkxOrderBookType orderBookType)
    {
        if (orderBookType is not OkxOrderBookType.BBO_TBT
            and not OkxOrderBookType.OrderBook_5
            and not OkxOrderBookType.OrderBook_l2_TBT)
            throw new ArgumentException("Spread order book subscriptions only support BBO_TBT, OrderBook_5, and OrderBook_l2_TBT.", nameof(orderBookType));

        return "sprd-" + MapConverter.GetString(orderBookType);
    }
}
