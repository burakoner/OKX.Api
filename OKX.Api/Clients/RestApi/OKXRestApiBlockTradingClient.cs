﻿using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;
using OKX.Api.Models.BlockTrading;

namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Rest Api Block Trading Client
/// </summary>
public class OKXRestApiBlockTradingClient : OKXRestApiBaseClient
{
    // Endpoints
    // TODO: api/v5/rfq/counterparties
    // TODO: api/v5/rfq/create-rfq
    // TODO: api/v5/rfq/cancel-rfq
    // TODO: api/v5/rfq/cancel-batch-rfqs
    // TODO: api/v5/rfq/cancel-all-rfqs
    // TODO: api/v5/rfq/execute-quote
    // TODO: api/v5/rfq/maker-instrument-settings
    // TODO: api/v5/rfq/maker-instrument-settings
    // TODO: api/v5/rfq/mmp-reset
    // TODO: api/v5/rfq/create-quote
    // TODO: api/v5/rfq/cancel-quote
    // TODO: api/v5/rfq/cancel-batch-quotes
    // TODO: api/v5/rfq/cancel-all-quotes
    // TODO: api/v5/rfq/rfqs
    // TODO: api/v5/rfq/quotes
    // TODO: api/v5/rfq/trades
    // TODO: api/v5/rfq/public-trades
    private const string v5MarketBlockTickers = "api/v5/market/block-tickers";
    private const string v5MarketBlockTicker = "api/v5/market/block-ticker";
    private const string v5MarketBlockTrades = "api/v5/market/block-trades";

    internal OKXRestApiBlockTradingClient(OKXRestApiClient root) : base(root)
    {
    }

    #region Block Trading API Endpoints

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
    public async Task<RestCallResult<List<OkxBlockTicker>>> GetBlockTickersAsync(
        OkxInstrumentType instrumentType,
        string instrumentFamily = null,
        string underlying = null,
        CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instType", JsonConvert.SerializeObject(instrumentType, new OkxInstrumentTypeConverter(false)) },
        };
        parameters.AddOptionalParameter("uly", underlying);
        parameters.AddOptionalParameter("instFamily", instrumentFamily);

        return await ProcessListRequest<OkxBlockTicker>(GetUri(v5MarketBlockTickers), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block ticker
    /// Retrieve the latest block trading volume in the last 24 hours.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<OkxBlockTicker>> GetBlockTickerAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await ProcessFirstOrDefaultRequest<OkxBlockTicker>(GetUri(v5MarketBlockTicker), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }

    /// <summary>
    /// Get block trades
    /// Retrieve the recent block trading transactions of an instrument. Descending order by tradeId.
    /// Rate Limit: 20 requests per 2 seconds
    /// </summary>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxBlockTrade>>> GetBlockTradesAsync(string instrumentId, CancellationToken ct = default)
    {
        var parameters = new Dictionary<string, object>
        {
            { "instId", instrumentId },
        };

        return await ProcessListRequest<OkxBlockTrade>(GetUri(v5MarketBlockTrades), HttpMethod.Get, ct, signed: false, queryParameters: parameters).ConfigureAwait(false);
    }
    #endregion

}