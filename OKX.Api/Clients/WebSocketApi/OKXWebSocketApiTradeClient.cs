using ApiSharp.Helpers;
using ApiSharp.Rest;
using OKX.Api.Enums;
using OKX.Api.Models;
using OKX.Api.Models.Trade;
using System;
using System.Drawing;

namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Trade Client
/// </summary>
public class OKXWebSocketApiTradeClient
{
    private const string _ref = "078ee129065aBCDE";
    private static Random _random = new Random();
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiTradeClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
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
        => await SubscribeToOrderUpdatesAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

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
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxOrder>>>>(data =>
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
        return await this.RootClient.RootSubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// PlaceOrderAsync order information. Data will not be pushed when first subscribed. Data will only be pushed when there are order updates.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<OkxBaseSocketResponse<OkxBaseSocketResponse>>> PlaceOrderAsync(string symbol,
        OkxOrderSide side,
        OkxOrderType type,
        OkxTradeMode tradeMode,
        decimal quantity,
        decimal? price = null,
        OkxPositionSide? positionSide = null,

        OkxQuickMarginType? quickMarginType = null,
        int? selfTradePreventionId = null,
        OkxSelfTradePreventionMode? selfTradePreventionMode = null,

        string? asset = null,
        OkxQuantityAsset? quantityAsset = null,
        string? clientOrderId = null,
        bool? reduceOnly = null)
    {
        string operation = "order";

        var args = new OkxBaseSocketRequestArg
        { 
            tag = "forkTest",
            instId = symbol,
            tdMode = JsonConvert.SerializeObject(tradeMode, new TradeModeConverter(false)),
            side = JsonConvert.SerializeObject(side, new OrderSideConverter(false)),
            posSide = JsonConvert.SerializeObject(positionSide, new PositionSideConverter(false)),
            ordType = JsonConvert.SerializeObject(type, new OrderTypeConverter(false)),
            sz = quantity.ToString(CultureInfo.InvariantCulture),
        };

        var request = new OkxBaseSocketRequest(operation, new List<OkxBaseSocketRequestArg> { args });
        request.Id =RandomString(10);
        var url = "/ws/v5/private";
        var json = JsonConvert.SerializeObject(request);
        var result = await RootClient.InternalQueryAsync<OkxBaseSocketResponse<OkxBaseSocketResponse>>(url, json, true);
        if (result.Success) {
            return result;
        }
        var errCode = result.Error?.Code ?? 0;
        var errMsg = result.Error?.Message ?? "empty";
        return result.AsError<OkxBaseSocketResponse<OkxBaseSocketResponse>>(new ServerError(errCode, errMsg, null));
    }

    // TODO: WS / Place order
    // TODO: WS / Place multiple orders
    // TODO: WS / Cancel order
    // TODO: WS / Cancel multiple orders
    // TODO: WS / Amend order
    // TODO: WS / Amend multiple orders
    // TODO: WS / Mass cancel order
    private string RandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[_random.Next(s.Length)]).ToArray());
    }
}