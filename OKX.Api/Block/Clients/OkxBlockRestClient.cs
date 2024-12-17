namespace OKX.Api.Block;

/// <summary>
/// OKX Rest Api Block Trading Client
/// </summary>
public class OkxBlockRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5RfqCounterparties = "api/v5/rfq/counterparties";
    private const string v5RfqCreateRfq = "api/v5/rfq/create-rfq";
    private const string v5RfqCancelRfq = "api/v5/rfq/cancel-rfq";
    private const string v5RfqCancelBatchRfqs = "api/v5/rfq/cancel-batch-rfqs";
    private const string v5RfqCancelAllRfqs = "api/v5/rfq/cancel-all-rfqs";
    private const string v5RfqExecuteQuote = "api/v5/rfq/execute-quote";
    private const string v5RfqMakerInstrumentSettings = "api/v5/rfq/maker-instrument-settings";
    private const string v5RfqMmpReset = "api/v5/rfq/mmp-reset";
    private const string v5RfqMmpConfig = "api/v5/rfq/mmp-config";
    private const string v5RfqCreateQuote = "api/v5/rfq/create-quote";
    private const string v5RfqCancelQuote = "api/v5/rfq/cancel-quote";
    private const string v5RfqCancelBatchQuotes = "api/v5/rfq/cancel-batch-quotes";
    private const string v5RfqCancelAllQuotes = "api/v5/rfq/cancel-all-quotes";
    private const string v5RfqCancelAllAfter = "api/v5/rfq/cancel-all-after";
    private const string v5RfqRfqs = "api/v5/rfq/rfqs";
    private const string v5RfqQuotes = "api/v5/rfq/quotes";
    private const string v5RfqTrades = "api/v5/rfq/trades";
    private const string v5RfqPublicTrades = "api/v5/rfq/public-trades";
    private const string v5MarketBlockTickers = "api/v5/market/block-tickers";
    private const string v5MarketBlockTicker = "api/v5/market/block-ticker";
    private const string v5MarketBlockTrades = "api/v5/market/block-trades";

    /// <summary>
    /// Retrieves the list of counterparties that the user is permitted to trade with.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockCounterparty>>> GetCounterpartiesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxBlockCounterparty>(GetUri(v5RfqCounterparties), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Creates a new RFQ
    /// </summary>
    /// <param name="counterparties">The trader code(s) of the counterparties who receive the RFQ. Can be found via /api/v5/rfq/counterparties/</param>
    /// <param name="legs">An Array of objects containing each leg of the RFQ. Maximum 15 legs can be placed per request</param>
    /// <param name="clientRfqId">Client-supplied RFQ ID. A combination of case-sensitive alpha-numeric, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="anonymous">Submit RFQ on a disclosed or anonymous basis. Valid values are true or false.
    /// If not specified, the default value is false.
    /// When anonymous = true, the taker’s identify is not disclosed to maker even after trade execution.</param>
    /// <param name="allowPartialExecution">Whether the RFQ can be partially filled provided that the shape of legs stays the same. Valid values are true or false. false by default.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBlockRfq>> CreateRfqAsync(
        IEnumerable<string> counterparties,
        IEnumerable<OkxBlockLegRequest> legs,
        string? clientRfqId = null,
        bool anonymous = false,
        bool allowPartialExecution = false,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "counterparties", counterparties },
            { "legs", legs },
            { "anonymous", anonymous },
            { "allowPartialExecution", allowPartialExecution },
        };
        parameters.AddOptional("clRfqId", clientRfqId);
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxBlockRfq>(GetUri(v5RfqCreateRfq), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel an existing active RFQ that you have created previously.
    /// </summary>
    /// <param name="rfqId">RFQ ID created .</param>
    /// <param name="clientRfqId">Client-supplied RFQ ID. A combination of case-sensitive alpha-numeric, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBlockCancelRfqResponse>> CancelRfqAsync(
        string? rfqId = null,
        string? clientRfqId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("rfqId", rfqId);
        parameters.AddOptional("clRfqId", clientRfqId);

        return ProcessOneRequestAsync<OkxBlockCancelRfqResponse>(GetUri(v5RfqCancelRfq), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel one or multiple active RFQ(s) in a single batch. Maximum 100 RFQ orders can be canceled per request.
    /// </summary>
    /// <param name="rfqIds">RFQ IDs .</param>
    /// <param name="clientRfqIds">Client-supplied RFQ IDs.
    /// Either rfqIds or clRfqIds is required.
    /// If both attributes are sent, rfqIds will be used as primary identifier.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockCancelRfqResponse>>> CancelRfqsAsync(
        IEnumerable<string>? rfqIds = null,
        IEnumerable<string>? clientRfqIds = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("rfqIds", rfqIds);
        parameters.AddOptional("clRfqIds", clientRfqIds);

        return ProcessListRequestAsync<OkxBlockCancelRfqResponse>(GetUri(v5RfqCancelBatchRfqs), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancels all active RFQs.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTimestamp>> CancelAllRfqsAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5RfqCancelAllRfqs), HttpMethod.Post, ct, signed: true);
    }

    /// <summary>
    /// Executes a Quote. It is only used by the creator of the RFQ
    /// </summary>
    /// <param name="rfqId">RFQ ID .</param>
    /// <param name="quoteId">Quote ID.</param>
    /// <param name="legs">An Array of objects containing the execution size of each leg of the RFQ.
    /// The ratio of the leg sizes needs to be the same as the RFQ.
    /// *Note: tgtCcy and side of each leg will be same as ones in the RFQ. px will be the same as the ones in the Quote.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockTrade>>> ExecuteQuoteAsync(
        string rfqId,
        string quoteId,
        IEnumerable<OkxBlockLegSize>? legs = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "rfqId", rfqId },
            { "quoteId", quoteId },
        };
        parameters.AddOptional("legs", legs);

        return ProcessListRequestAsync<OkxBlockTrade>(GetUri(v5RfqExecuteQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieve the products which makers want to quote and receive RFQs for, and the corresponding price and size limit.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockQuoteProduct>>> GetQuoteProductsAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxBlockQuoteProduct>(GetUri(v5RfqMakerInstrumentSettings), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Customize the products which makers want to quote and receive RFQs for, and the corresponding price and size limit.
    /// </summary>
    /// <param name="products">Products request</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<bool?>> SetQuoteProductsAsync(
        IEnumerable<OkxBlockQuoteProductRequest> products,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.SetBody(products);

        var result = await ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5RfqMakerInstrumentSettings), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
        if (!result) return new RestCallResult<bool?>(result.Request, result.Response, result.Raw, result.Error);
        return new RestCallResult<bool?>(result.Request, result.Response, result.Data.Result, result.Raw, result.Error);
    }

    /// <summary>
    /// Reset the MMP status to be inactive.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTimestamp>> ResetMmpAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5RfqMmpReset), HttpMethod.Post, ct, signed: true);
    }

    /// <summary>
    /// This endpoint is used to set MMP configure and only applicable to block trading makers
    /// </summary>
    /// <param name="timeInterval">Time window (ms). MMP interval where monitoring is done.
    /// "0" means disable MMP. Maximum time interval is 600,000.</param>
    /// <param name="frozenInterval">Frozen period (ms).
    /// "0" means the trade will remain frozen until you request "Reset MMP Status" to unfrozen.</param>
    /// <param name="countLimit">Limit in number of execution attempts.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBlockMmp>> SetMmpAsync(int timeInterval, int frozenInterval, int countLimit, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "timeInterval", timeInterval.ToOkxString() },
            { "frozenInterval", frozenInterval.ToOkxString() },
            { "countLimit", countLimit.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxBlockMmp>(GetUri(v5RfqMmpConfig), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// This endpoint is used to get MMP configure information and only applicable to block trading market makers
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBlockMmpConfiguration>> GetMmpAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxBlockMmpConfiguration>(GetUri(v5RfqMmpConfig), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Allows the user to Quote an RFQ that they are a counterparty to. The user MUST quote the entire RFQ and not part of the legs or part of the quantity. Partial quoting is not allowed.
    /// </summary>
    /// <param name="rfqId">RFQ ID .</param>
    /// <param name="quoteSide">The trading direction of the Quote. Its value can be buy or sell. For example, if quoteSide is buy, all the legs are executed in their leg sides; otherwise, all the legs are executed in the opposite of their leg sides.</param>
    /// <param name="legs">The legs of the Quote.</param>
    /// <param name="clientQuoteId">Client-supplied Quote ID. A combination of case-sensitive alphanumerics, all numbers, or all letters of up to 32 characters.</param>
    /// <param name="anonymous">Submit Quote on a disclosed or anonymous basis. Valid value is true or false. false by default.</param>
    /// <param name="expiresIn">Seconds that a quote expires in. Must be an integer between 10-120. Default is 60.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBlockRfq>> CreateQuoteAsync(
        string rfqId,
        OkxTradeOrderSide quoteSide,
        IEnumerable<OkxBlockLegQuote> legs,
        string? clientQuoteId = null,
        bool anonymous = false,
        int? expiresIn = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "rfqId", rfqId },
            { "legs", legs },
            { "anonymous", anonymous },
        };
        parameters.AddEnum("quoteSide", quoteSide);
        parameters.AddOptional("clRfqId", clientQuoteId);
        parameters.AddOptional("expiresIn", expiresIn?.ToOkxString());
        parameters.AddOptional("tag", OkxConstants.BrokerId);

        return ProcessOneRequestAsync<OkxBlockRfq>(GetUri(v5RfqCreateQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancels an existing active Quote you have created in response to an RFQ.
    /// </summary>
    /// <param name="quoteId">Quote ID.</param>
    /// <param name="clientQuoteId">Client-supplied Quote ID. Either quoteId or clQuoteId is required. If both clQuoteId and quoteId are passed, quoteId will be treated as primary identifier.</param>
    /// <param name="rfqId">RFQ ID.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBlockCancelQuoteResponse>> CancelQuoteAsync(
        string? quoteId = null,
        string? clientQuoteId = null,
        string? rfqId = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("quoteId", quoteId);
        parameters.AddOptional("clQuoteId", clientQuoteId);
        parameters.AddOptional("rfqId", rfqId);

        return ProcessOneRequestAsync<OkxBlockCancelQuoteResponse>(GetUri(v5RfqCancelQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancel one or multiple active Quote(s) in a single batch. Maximum 100 quote orders can be canceled per request.
    /// </summary>
    /// <param name="quoteIds">Quote IDs .</param>
    /// <param name="clientQuoteIds">Client-supplied Quote IDs. Either quoteIds or clQuoteIds is required.If both attributes are sent, quoteIds will be used as primary identifier.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockCancelQuoteResponse>>> CancelQuotesAsync(
        IEnumerable<string>? quoteIds = null,
        IEnumerable<string>? clientQuoteIds = null,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddOptional("quoteIds", quoteIds);
        parameters.AddOptional("clQuoteIds", clientQuoteIds);

        return ProcessListRequestAsync<OkxBlockCancelQuoteResponse>(GetUri(v5RfqCancelBatchQuotes), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Cancels all active Quotes.
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxTimestamp>> CancelAllQuotesAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5RfqCancelAllQuotes), HttpMethod.Post, ct, signed: true);
    }

    /// <summary>
    /// Cancel all quotes after the countdown timeout.
    /// </summary>
    /// <param name="timeout">The countdown for quotes cancellation, with second as the unit.
    /// Range of value can be 0, [10, 120].
    /// Setting timeOut to 0 disables Cancel All After.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxCancelAllAfter>> CancelAllQuotesAfterAsync(int timeout, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "timeOut", timeout.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxCancelAllAfter>(GetUri(v5RfqCancelAllAfter), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Retrieves details of RFQs that the user is a counterparty to (either as the creator or the receiver of the RFQ).
    /// </summary>
    /// <param name="rfqId">RFQ ID .</param>
    /// <param name="clientRfqId">Client-supplied RFQ ID. If both clRfqId and rfqId are passed, rfqId will be treated as primary identifier</param>
    /// <param name="state">The status of the RFQ. Valid values can be active canceled pending_fill filled expired failed traded_away. traded_away only applies to Maker</param>
    /// <param name="beginId">Start rfq id the request to begin with. Pagination of data to return records newer than the requested rfqId, not including beginId</param>
    /// <param name="endId">End rfq id the request to end with. Pagination of data to return records earlier than the requested rfqId, not including endId</param>
    /// <param name="limit">Number of results per request. The maximum is 100 which is also the default value.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockRfq>>> GetRfqsAsync(
        string? rfqId = null,
        string? clientRfqId = null,
        OkxBlockState? state = null,
        string? beginId = null,
        string? endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("rfqId", rfqId);
        parameters.AddOptional("clRfqId", clientRfqId);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("beginId", beginId);
        parameters.AddOptional("endId", endId);
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBlockRfq>(GetUri(v5RfqRfqs), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieve all Quotes that the user is a counterparty to (either as the creator or the receiver).
    /// </summary>
    /// <param name="rfqId">RFQ ID .</param>
    /// <param name="clientRfqId">Client-supplied RFQ ID. If both clRfqId and rfqId are passed, rfqId will be be treated as primary identifier.</param>
    /// <param name="quoteId">Quote ID</param>
    /// <param name="clientQuoteId">Client-supplied Quote ID. If both clQuoteId and quoteId are passed, quoteId will be treated as primary identifier</param>
    /// <param name="state">The status of the quote. Valid values can be active canceled pending_fill filled expired or failed.</param>
    /// <param name="beginId">Start quote id the request to begin with. Pagination of data to return records newer than the requested quoteId, not including beginId</param>
    /// <param name="endId">End quote id the request to end with. Pagination of data to return records earlier than the requested quoteId, not including endId</param>
    /// <param name="limit">Number of results per request. The maximum is 100 which is also the default value.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockQuote>>> GetQuotesAsync(
        string? rfqId = null,
        string? clientRfqId = null,
        string? quoteId = null,
        string? clientQuoteId = null,
        OkxBlockState? state = null,
        string? beginId = null,
        string? endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("rfqId", rfqId);
        parameters.AddOptional("clRfqId", clientRfqId);
        parameters.AddOptional("quoteId", quoteId);
        parameters.AddOptional("clQuoteId", clientQuoteId);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("beginId", beginId);
        parameters.AddOptional("endId", endId);
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBlockQuote>(GetUri(v5RfqQuotes), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieves the executed trades that the user is a counterparty to (either as the creator or the receiver).
    /// </summary>
    /// <param name="rfqId">RFQ ID .</param>
    /// <param name="clientRfqId">Client-supplied RFQ ID. If both clRfqId and rfqId are passed, rfqId will be treated as primary identifier</param>
    /// <param name="quoteId">Quote ID</param>
    /// <param name="clientQuoteId">Client-supplied Quote ID. If both clQuoteId and quoteId are passed, quoteId will be treated as primary identifier</param>
    /// <param name="blockTradeId">Block trade ID</param>
    /// <param name="isSuccessful">Whether the trade is filled successfully. true: the default value. false.</param>
    /// <param name="beginId">The starting rfq id the request to begin with. Pagination of data to return records newer than the requested blockTdId, not including beginId.</param>
    /// <param name="endId">The last rfq id the request to end withPagination of data to return records earlier than the requested blockTdId, not including endId.</param>
    /// <param name="beginTimestamp">Filter trade execution time with a begin timestamp (UTC timezone). Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="endTimestamp">	Filter trade execution time with an end timestamp (UTC timezone). Unix timestamp format in milliseconds, e.g. 1597026383085</param>
    /// <param name="limit">Number of results per request. The maximum is 100 which is also the default value.
    /// If the number of trades in the requested range is bigger than 100, the latest 100 trades in the range will be returned.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockTrade>>> GetTradesAsync(
        string? rfqId = null,
        string? clientRfqId = null,
        string? quoteId = null,
        string? clientQuoteId = null,
        string? blockTradeId = null,
        bool? isSuccessful = null,
        string? beginId = null,
        string? endId = null,
        long? beginTimestamp = null,
        long? endTimestamp = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("rfqId", rfqId);
        parameters.AddOptional("clRfqId", clientRfqId);
        parameters.AddOptional("quoteId", quoteId);
        parameters.AddOptional("clQuoteId", clientQuoteId);
        parameters.AddOptional("blockTdId", blockTradeId);
        parameters.AddOptional("isSuccessful", isSuccessful);
        parameters.AddOptional("beginId", beginId);
        parameters.AddOptional("endId", endId);
        parameters.AddOptional("beginTs", beginTimestamp);
        parameters.AddOptional("endTs", endTimestamp);
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBlockTrade>(GetUri(v5RfqTrades), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Retrieves the executed block trades.
    /// </summary>
    /// <param name="beginId">The starting blockTdId the request to begin with. Pagination of data to return records newer than the requested blockTdId, not including beginId.</param>
    /// <param name="endId">The last blockTdId the request to end with. Pagination of data to return records earlier than the requested blockTdId, not including endId.</param>
    /// <param name="limit">Number of results per request. The maximum is 100 which is also the default value.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockPublicExecutedTrade>>> GetPublicExecutedTradesAsync(
        string? beginId = null,
        string? endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new ParameterCollection();
        parameters.AddOptional("beginId", beginId);
        parameters.AddOptional("endId", endId);
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBlockPublicExecutedTrade>(GetUri(v5RfqPublicTrades), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get block tickers
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockTicker>>> GetTickersAsync(OkxInstrumentType instrumentType, string? instrumentFamily = null, string? underlying = null, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection();
        parameters.AddEnum("instType", instrumentType);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptional("instFamily", instrumentFamily);

        return ProcessListRequestAsync<OkxBlockTicker>(GetUri(v5MarketBlockTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Get block ticker
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxBlockTicker>> GetTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };

        return ProcessOneRequestAsync<OkxBlockTicker>(GetUri(v5MarketBlockTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

    /// <summary>
    /// Get block trades
    /// Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxBlockPublicRecentTrade>>> GetPublicRecentTradesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "instId", instrumentId },
        };

        return ProcessListRequestAsync<OkxBlockPublicRecentTrade>(GetUri(v5MarketBlockTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

}