/* Sync: 09 Aug 2024 */

using OKX.Api.Block.Converters;
using OKX.Api.Block.Enums;
using OKX.Api.Block.Models;
using OKX.Api.Trade.Converters;
using OKX.Api.Trade.Enums;

namespace OKX.Api.Block.Clients;

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

    public Task<RestCallResult<List<OkxBlockCounterparty>>> GetCounterpartiesAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxBlockCounterparty>(GetUri(v5RfqCounterparties), HttpMethod.Get, ct, signed: true);
    }

    public Task<RestCallResult<OkxBlockRfq>> CreateRfqAsync(
        IEnumerable<string> counterparties,
        IEnumerable<OkxBlockLegResponse> legs,
        string clientRfqId = null,
        bool anonymous = false,
        bool allowPartialExecution = false,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "counterparties", counterparties },
            { "legs", legs },
            { "anonymous", anonymous },
            { "allowPartialExecution", allowPartialExecution },
        };
        parameters.AddOptionalParameter("clRfqId", clientRfqId);
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        return ProcessOneRequestAsync<OkxBlockRfq>(GetUri(v5RfqCreateRfq), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxBlockCancelRfqResponse>> CancelRfqAsync(
        string rfqId = null,
        string clientRfqId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("rfqId", rfqId);
        parameters.AddOptionalParameter("clRfqId", clientRfqId);

        return ProcessOneRequestAsync<OkxBlockCancelRfqResponse>(GetUri(v5RfqCancelRfq), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxBlockCancelRfqResponse>>> CancelRfqsAsync(
        IEnumerable<string> rfqIds = null,
        IEnumerable<string> clientRfqIds = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("rfqIds", rfqIds);
        parameters.AddOptionalParameter("clRfqIds", clientRfqIds);

        return ProcessListRequestAsync<OkxBlockCancelRfqResponse>(GetUri(v5RfqCancelBatchRfqs), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxTimestamp>> CancelAllRfqsAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5RfqCancelAllRfqs), HttpMethod.Post, ct, signed: true);
    }

    public Task<RestCallResult<List<OkxBlockTrade>>> ExecuteQuoteAsync(
        string rfqId,
        string quoteId,
        IEnumerable<OkxBlockLegSize> legs = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "rfqId", rfqId },
            { "quoteId", quoteId },
        };
        parameters.AddOptionalParameter("legs", legs);

        return ProcessListRequestAsync<OkxBlockTrade>(GetUri(v5RfqExecuteQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxBlockQuoteProduct>>> GetQuoteProductsAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxBlockQuoteProduct>(GetUri(v5RfqMakerInstrumentSettings), HttpMethod.Get, ct, signed: true);
    }

    public Task<RestCallResult<OkxBooleanResponse>> SetQuoteProductsAsync(
        IEnumerable<OkxBlockQuoteProductRequest> products,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object> {
            { Options.RequestBodyParameterKey, products },
        };

        return ProcessOneRequestAsync<OkxBooleanResponse>(GetUri(v5RfqMakerInstrumentSettings), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxTimestamp>> ResetMmpAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5RfqMmpReset), HttpMethod.Post, ct, signed: true);
    }

    public Task<RestCallResult<OkxBlockMmp>> SetMmpAsync(int timeInterval, int frozenInterval, int countLimit, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "timeInterval", timeInterval.ToOkxString() },
            { "frozenInterval", frozenInterval.ToOkxString() },
            { "countLimit", countLimit.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxBlockMmp>(GetUri(v5RfqMmpConfig), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxBlockMmpConfiguration>> GetMmpAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxBlockMmpConfiguration>(GetUri(v5RfqMmpConfig), HttpMethod.Get, ct, signed: true);
    }

    public Task<RestCallResult<OkxBlockRfq>> CreateQuoteAsync(
        string rfqId,
        OkxOrderSide quoteSide,
        IEnumerable<OkxBlockLegQuote> legs,
        string clientQuoteId = null,
        bool anonymous = false,
        int? expiresIn = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "rfqId", rfqId },
            { "quoteSide", JsonConvert.SerializeObject(quoteSide, new OkxOrderSideConverter(false)) },
            { "legs", legs },
            { "anonymous", anonymous },
        };
        parameters.AddOptionalParameter("clRfqId", clientQuoteId);
        parameters.AddOptionalParameter("expiresIn", expiresIn?.ToOkxString());
        parameters.AddOptionalParameter("tag", Options.BrokerId);

        return ProcessOneRequestAsync<OkxBlockRfq>(GetUri(v5RfqCreateQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxBlockCancelQuoteResponse>> CancelQuoteAsync(
        string quoteId = null,
        string clientQuoteId = null,
        string rfqId = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("quoteId", quoteId);
        parameters.AddOptionalParameter("clQuoteId", clientQuoteId);
        parameters.AddOptionalParameter("rfqId", rfqId);

        return ProcessOneRequestAsync<OkxBlockCancelQuoteResponse>(GetUri(v5RfqCancelQuote), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<List<OkxBlockCancelQuoteResponse>>> CancelQuotesAsync(
        IEnumerable<string> quoteIds = null,
        IEnumerable<string> clientQuoteIds = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("quoteIds", quoteIds);
        parameters.AddOptionalParameter("clQuoteIds", clientQuoteIds);

        return ProcessListRequestAsync<OkxBlockCancelQuoteResponse>(GetUri(v5RfqCancelBatchQuotes), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    public Task<RestCallResult<OkxTimestamp>> CancelAllQuotesAsync(CancellationToken ct = default)
    {
        return ProcessOneRequestAsync<OkxTimestamp>(GetUri(v5RfqCancelAllQuotes), HttpMethod.Post, ct, signed: true);
    }

    public Task<RestCallResult<OkxBlockCancelAllAfterResponse>> CancelAllQuotesAfterAsync(int timeout, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "timeOut", timeout.ToOkxString() },
        };

        return ProcessOneRequestAsync<OkxBlockCancelAllAfterResponse>(GetUri(v5RfqCancelAllAfter), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxBlockRfq>>> GetRfqsAsync(
        string rfqId = null,
        string clientRfqId = null,
        OkxBlockState? state = null,
        string beginId = null,
        string endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("rfqId", rfqId);
        parameters.AddOptionalParameter("clRfqId", clientRfqId);
        if(state is not null)
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxBlockStateConverter(false)));
        parameters.AddOptionalParameter("beginId", beginId);
        parameters.AddOptionalParameter("endId", endId);
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBlockRfq>(GetUri(v5RfqRfqs), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxBlockQuote>>> GetQuotesAsync(
        string rfqId = null,
        string clientRfqId = null,
        string quoteId = null,
        string clientQuoteId = null,
        OkxBlockState? state = null,
        string beginId = null,
        string endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("rfqId", rfqId);
        parameters.AddOptionalParameter("clRfqId", clientRfqId);
        parameters.AddOptionalParameter("quoteId", quoteId);
        parameters.AddOptionalParameter("clQuoteId", clientQuoteId);
        if(state is not null)
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxBlockStateConverter(false)));
        parameters.AddOptionalParameter("beginId", beginId);
        parameters.AddOptionalParameter("endId", endId);
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBlockQuote>(GetUri(v5RfqQuotes), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
    
    public Task<RestCallResult<List<OkxBlockTrade>>> GetTradesAsync(
        string rfqId = null,
        string clientRfqId = null,
        string quoteId = null,
        string clientQuoteId = null,
        string blockTradeId = null,
        OkxBlockState? state = null,
        string beginId = null,
        string endId = null,
        long? beginTimestamp = null,
        long? endTimestamp = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("rfqId", rfqId);
        parameters.AddOptionalParameter("clRfqId", clientRfqId);
        parameters.AddOptionalParameter("quoteId", quoteId);
        parameters.AddOptionalParameter("clQuoteId", clientQuoteId);
        parameters.AddOptionalParameter("blockTdId", blockTradeId);
        if(state is not null)
        parameters.AddOptionalParameter("state", JsonConvert.SerializeObject(state, new OkxBlockStateConverter(false)));
        parameters.AddOptionalParameter("beginId", beginId);
        parameters.AddOptionalParameter("endId", endId);
        parameters.AddOptionalParameter("beginTs", beginTimestamp);
        parameters.AddOptionalParameter("endTs", endTimestamp);
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxBlockTrade>(GetUri(v5RfqTrades), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    public Task<RestCallResult<List<OkxBlockPublicExecutedTrade>>> GetPublicExecutedTradesAsync(
        string beginId = null,
        string endId = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);
        var parameters = new Dictionary<string, object>();
        parameters.AddOptionalParameter("beginId", beginId);
        parameters.AddOptionalParameter("endId", endId);
        parameters.AddOptionalParameter("limit", limit.ToOkxString());

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
    public Task<RestCallResult<List<OkxBlockTicker>>> GetTickersAsync(OkxInstrumentType instrumentType, string instrumentFamily = null, string underlying = null, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

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
        var parameters = new Dictionary<string, object>
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
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return ProcessListRequestAsync<OkxBlockPublicRecentTrade>(GetUri(v5MarketBlockTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters);
    }

}