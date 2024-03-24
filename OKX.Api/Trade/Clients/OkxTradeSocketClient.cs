using OKX.Api.Trade.Models;

namespace OKX.Api.Trade.Clients;

/// <summary>
/// OKX WebSocket Api Trade Client
/// </summary>
public class OkxTradeSocketClient
{
    // Root Client
    internal OkxSocketApiClient RootClient { get; }
    internal OkxSocketApiOptions Options { get { return RootClient.Options; } }

    internal OkxTradeSocketClient(OkxSocketApiClient root)
    {
        RootClient = root;
    }

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
        Action<OkxOrder> onData,
        OkxInstrumentType instrumentType,
        string instrumentFamily = null,
        string instrumentId = null,
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
        Action<OkxOrder> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "orders",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await RootClient.RootSubscribeAsync(OkxSocketEndpoint.Private, request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// You can place an order only if you have sufficient funds.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxOrderPlaceResponse>> PlaceOrderAsync(OkxOrderPlaceRequest request)
    {
        request.Tag = Options.BrokerId;
        var req = new OkxSocketRequest<OkxOrderPlaceRequest>(RootClient.RequestId().ToString(), OkxSocketOperation.Order, [request]);
        return await RootClient.RootQueryAsync<OkxOrderPlaceResponse>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Place orders in a batch. Maximum 20 orders can be placed per request
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxOrderPlaceResponse>>> PlaceMultipleOrdersAsync(IEnumerable<OkxOrderPlaceRequest> requests)
    {
        foreach (var order in requests) order.Tag = Options.BrokerId;
        var req = new OkxSocketRequest<OkxOrderPlaceRequest>(RootClient.RequestId().ToString(), OkxSocketOperation.BatchOrders, requests);
        return await RootClient.RootQueryAsync<IEnumerable<OkxOrderPlaceResponse>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel an incomplete order
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxOrderCancelResponse>> CancelOrderAsync(OkxOrderCancelRequest request)
    {
        var req = new OkxSocketRequest<OkxOrderCancelRequest>(RootClient.RequestId().ToString(), OkxSocketOperation.CancelOrder, [request]);
        return await RootClient.RootQueryAsync<OkxOrderCancelResponse>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel incomplete orders in batches. Maximum 20 orders can be canceled per request.
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxOrderCancelResponse>>> CancelMultipleOrdersAsync(IEnumerable<OkxOrderCancelRequest> requests)
    {
        var req = new OkxSocketRequest<OkxOrderCancelRequest>(RootClient.RequestId().ToString(), OkxSocketOperation.BatchAmendOrders, requests);
        return await RootClient.RootQueryAsync<IEnumerable<OkxOrderCancelResponse>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend an incomplete order.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxOrderAmendResponse>> AmendOrderAsync(OkxOrderAmendRequest request)
    {
        var req = new OkxSocketRequest<OkxOrderAmendRequest>(RootClient.RequestId().ToString(), OkxSocketOperation.AmendOrder, [request]);
        return await RootClient.RootQueryAsync<OkxOrderAmendResponse>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Amend incomplete orders in batches. Maximum 20 orders can be amended per request.
    /// </summary>
    /// <param name="requests">Requests</param>
    /// <returns></returns>
    public async Task<CallResult<IEnumerable<OkxOrderAmendResponse>>> AmendMultipleOrdersAsync(IEnumerable<OkxOrderAmendRequest> requests)
    {
        var req = new OkxSocketRequest<OkxOrderAmendRequest>(RootClient.RequestId().ToString(), OkxSocketOperation.BatchAmendOrders, requests);
        return await RootClient.RootQueryAsync<IEnumerable<OkxOrderAmendResponse>>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

    /// <summary>
    /// Cancel all the MMP pending orders of an instrument family.
    /// Only applicable to Option in Portfolio Margin mode, and MMP privilege is required.
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns></returns>
    public async Task<CallResult<OkxMassCancelResponse>> MassCancelOrdersAsync(OkxMassCancelRequest request)
    {
        var req = new OkxSocketRequest<OkxMassCancelRequest>(RootClient.RequestId().ToString(), OkxSocketOperation.MassCancel, [request]);
        return await RootClient.RootQueryAsync<OkxMassCancelResponse>(OkxSocketEndpoint.Private, req, true).ConfigureAwait(false);
    }

}