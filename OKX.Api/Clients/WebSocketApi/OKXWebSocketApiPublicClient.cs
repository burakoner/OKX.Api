using OKX.Api.Account.Enums;
using OKX.Api.Models.Public;

namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Public Client
/// </summary>
public class OKXWebSocketApiPublicClient
{
    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiPublicClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;
    }

    #region Public Data
    /// <summary>
    /// The full instrument list will be pushed for the first time after subscription. Subsequently, the instruments will be pushed if there's any change to the instrument’s state (such as delivery of FUTURES, exercise of OPTION, listing of new contracts / trading pairs, trading suspension, etc.).
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToInstrumentsAsync(Action<OkxInstrument> onData, OkxInstrumentType instrumentType, CancellationToken ct = default)
        => await SubscribeToInstrumentsAsync(onData, new OkxInstrumentType[] { instrumentType }, ct).ConfigureAwait(false);

    /// <summary>
    /// The full instrument list will be pushed for the first time after subscription. Subsequently, the instruments will be pushed if there's any change to the instrument’s state (such as delivery of FUTURES, exercise of OPTION, listing of new contracts / trading pairs, trading suspension, etc.).
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentTypes">List of Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToInstrumentsAsync(Action<OkxInstrument> onData, IEnumerable<OkxInstrumentType> instrumentTypes, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxInstrument>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentType in instrumentTypes) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "instruments",
            InstrumentType = instrumentType,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the open interest. Data will by pushed every 3 seconds.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOpenInterestsAsync(Action<OkxOpenInterest> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToOpenInterestsAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the open interest. Data will by pushed every 3 seconds.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">Lİst of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOpenInterestsAsync(Action<OkxOpenInterest> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxOpenInterest>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "open-interest",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve funding rate. Data will be pushed every minute.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToFundingRatesAsync(Action<OkxFundingRate> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToFundingRatesAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve funding rate. Data will be pushed every minute.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToFundingRatesAsync(Action<OkxFundingRate> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxFundingRate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "funding-rate",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the maximum buy price and minimum sell price of the instrument. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPriceLimitAsync(Action<OkxLimitPrice> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToPriceLimitAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the maximum buy price and minimum sell price of the instrument. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToPriceLimitAsync(Action<OkxLimitPrice> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxLimitPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "price-limit",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve detailed pricing information of all OPTION contracts. Data will be pushed at once.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOptionSummaryAsync(Action<OkxOptionSummary> onData, string instrumentFamily, CancellationToken ct = default)
        => await SubscribeToOptionSummaryAsync(onData, new string[] { instrumentFamily }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve detailed pricing information of all OPTION contracts. Data will be pushed at once.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentFamilies">List of Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOptionSummaryAsync(Action<OkxOptionSummary> onData, IEnumerable<string> instrumentFamilies, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxOptionSummary>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentFamily in instrumentFamilies) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "opt-summary",
            InstrumentFamily = instrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the estimated delivery/exercise price of FUTURES contracts and OPTION.
    /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToEstimatedPriceAsync(Action<OkxEstimatedPrice> onData, OkxInstrumentType instrumentType, string instrumentFamily = null, string instrumentId = null, CancellationToken ct = default)
        => await SubscribeToEstimatedPriceAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the estimated delivery/exercise price of FUTURES contracts and OPTION.
    /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbol List</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToEstimatedPriceAsync(Action<OkxEstimatedPrice> onData, IEnumerable<OkxSocketSymbolRequest> symbols, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxEstimatedPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "estimated-price",
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
            InstrumentId = symbol.InstrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the mark price. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPriceAsync(Action<OkxMarkPrice> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToMarkPriceAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the mark price. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">Lİst of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPriceAsync(Action<OkxMarkPrice> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxMarkPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "mark-price",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve index tickers data
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToIndexTickersAsync(Action<OkxIndexTicker> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToIndexTickersAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve index tickers data
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToIndexTickersAsync(Action<OkxIndexTicker> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxIndexTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "index-tickers",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of the mark price. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPriceCandlesticksAsync(Action<OkxMarkCandlestick> onData, string instrumentId, OkxPeriod period, CancellationToken ct = default)
        => await SubscribeToMarkPriceCandlesticksAsync(onData, [instrumentId], period, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the candlesticks data of the mark price. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToMarkPriceCandlesticksAsync(Action<OkxMarkCandlestick> onData, IEnumerable<string> instrumentIds, OkxPeriod period, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxMarkCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = data.Data.Args?.Instrument;
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "mark-price-candle" + JsonConvert.SerializeObject(period, new PeriodConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }


    /// <summary>
    /// Retrieve the candlesticks data of the index. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToIndexCandlesticksAsync(Action<OkxIndexCandlestick> onData, string instrumentId, OkxPeriod period, CancellationToken ct = default)
        => await SubscribeToIndexCandlesticksAsync(onData, new string[] { instrumentId }, period, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the candlesticks data of the index. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToIndexCandlesticksAsync(Action<OkxIndexCandlestick> onData, IEnumerable<string> instrumentIds, OkxPeriod period, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxIndexCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = data.Data.Args?.Instrument;
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "index-candle" + JsonConvert.SerializeObject(period, new PeriodConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    #endregion

    #region Market Data
    
    /// <summary>
    /// Retrieve the last traded price, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<OkxTicker> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToTickersAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the last traded price, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTickersAsync(Action<OkxTicker> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "tickers",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of an instrument. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(Action<OkxCandlestick> onData, string instrumentId, OkxPeriod period, CancellationToken ct = default)
        => await SubscribeToCandlesticksAsync(onData, new string[] { instrumentId }, period, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the candlesticks data of an instrument. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="period"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToCandlesticksAsync(Action<OkxCandlestick> onData, IEnumerable<string> instrumentIds, OkxPeriod period, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = data.Data.Args?.Instrument;
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "candle" + JsonConvert.SerializeObject(period, new PeriodConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Business, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the recent trades data. Data will be pushed whenever there is a trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(Action<OkxTrade> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToTradesAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the recent trades data. Data will be pushed whenever there is a trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToTradesAsync(Action<OkxTrade> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxSocketUpdateResponse<List<OkxTrade>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "trades",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    // TODO: WS / All trades channel

    /// <summary>
    /// Retrieve order book data.
    /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
    /// books: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed every 100 ms when there is change in order book.
    /// books5: 5 depth levels will be pushed every time.Data will be pushed every 200 ms when there is change in order book.
    /// books50-l2-tbt: 50 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// books-l2-tbt: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderBookType">Order Book Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(Action<OkxOrderBook> onData, string instrumentId, OkxOrderBookType orderBookType, CancellationToken ct = default)
        => await SubscribeToOrderBookAsync(onData, new string[] { instrumentId }, orderBookType, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve order book data.
    /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
    /// books: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed every 100 ms when there is change in order book.
    /// books5: 5 depth levels will be pushed every time.Data will be pushed every 200 ms when there is change in order book.
    /// books50-l2-tbt: 50 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// books-l2-tbt: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="orderBookType">Order Book Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<CallResult<WebSocketUpdateSubscription>> SubscribeToOrderBookAsync(Action<OkxOrderBook> onData, IEnumerable<string> instrumentIds, OkxOrderBookType orderBookType, CancellationToken ct = default)
    {
        var internalHandler = new Action<WebSocketDataEvent<OkxOrderBookUpdate>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = data.Data.Args?.Instrument;
                d.Action = data.Data.Action;
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = JsonConvert.SerializeObject(orderBookType, new OrderBookTypeConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await this.RootClient.RootSubscribeAsync(OkxSocketEndpoint.Public, request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    // TODO: WS / Option trades channel
    #endregion
}