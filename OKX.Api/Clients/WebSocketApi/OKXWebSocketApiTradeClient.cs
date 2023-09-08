using ApiSharp.Helpers;
using OKX.Api.Enums;
using OKX.Api.Models;
using OKX.Api.Models.Trade;
using System;

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
    public async Task<CallResult<OkxOrderPlaceResponse>> PlaceOrderAsync(string symbol,
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
        var parameters = new Dictionary<string, object>()
        {
            { "instId", symbol },
            { "tdMode", EnumConverter.GetString(tradeMode) },
            { "side", EnumConverter.GetString(side) },
            { "ordType", EnumConverter.GetString(type) },
            { "sz", quantity.ToString(CultureInfo.InvariantCulture) },
        };

        parameters.AddOptionalParameter("ccy", asset);
        parameters.AddOptionalParameter("clOrdId", _ref + (clientOrderId ?? RandomString(15)));
        parameters.AddOptionalParameter("tag", _ref);
        parameters.AddOptionalParameter("posSide", EnumConverter.GetString(positionSide));
        parameters.AddOptionalParameter("px", price?.ToString(CultureInfo.InvariantCulture));
        parameters.AddOptionalParameter("reduceOnly", reduceOnly);
        parameters.AddOptionalParameter("tgtCcy", EnumConverter.GetString(quantityAsset));
        parameters.AddOptionalParameter("quickMgnType", EnumConverter.GetString(quickMarginType));
        parameters.AddOptionalParameter("stpId", selfTradePreventionId);
        parameters.AddOptionalParameter("stpMode", EnumConverter.GetString(selfTradePreventionMode));
        /*  "id": "1512",
"op": "order",
"args": [
{
"side": "buy",
"instId": "BTC-USDT",
"tdMode": "isolated",
"ordType": "market",
"sz": "100"
}
]*/        string operation = "order";
        var requestWrapper = new OkxSocketMessage
        {
            Id = _ref + RandomString(10),
            Operation = operation,
            Args = new[] { parameters }
        };
        var url = "/ws/v5/private";
        var result = await RootClient.QueryAsync<OkxOrderPlaceResponse>(url, requestWrapper, true);
        if (result.Data.ErrorCode!= "0")
            return result.AsError<OkxOrderPlaceResponse>(new ServerError(int.Parse(result.Data.ErrorCode), result.Data.ErrorMessage, null));

        return result;
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