namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest Api Dual Investment Client
/// </summary>
public class OkxFinancialDualInvestmentRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    /// <summary>
    /// Get all supported dual investment currency pairs
    /// </summary>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialDualInvestmentCurrencyPair>>> GetCurrencyPairsAsync(CancellationToken ct = default)
    {
        return ProcessListRequestAsync<OkxFinancialDualInvestmentCurrencyPair>(GetUri("api/v5/finance/sfp/dcd/currency-pair"), HttpMethod.Get, ct, signed: true);
    }

    /// <summary>
    /// Get dual investment products for the selected currency pair and option type.
    /// The OKX documentation currently shows a list payload, while the live API returns a data.products wrapper.
    /// This wrapper supports both payload shapes.
    /// </summary>
    /// <param name="baseCurrency">Base currency, e.g. BTC</param>
    /// <param name="quoteCurrency">Quote currency, e.g. USDT</param>
    /// <param name="optionType">Option type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public async Task<RestCallResult<List<OkxFinancialDualInvestmentProduct>>> GetProductsAsync(
        string baseCurrency,
        string quoteCurrency,
        OkxOptionType optionType,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "baseCcy", baseCurrency },
            { "quoteCcy", quoteCurrency },
        };
        parameters.AddEnum("optType", optionType);

        var result = await ProcessModelRequestAsync<OkxFinancialDualInvestmentProductsData>(GetUri("api/v5/finance/sfp/dcd/products"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
        if (!result) return new RestCallResult<List<OkxFinancialDualInvestmentProduct>>(result.Request, result.Response, result.Raw ?? "", result.Error);
        return new RestCallResult<List<OkxFinancialDualInvestmentProduct>>(result.Request, result.Response, result.Data.Products, result.Raw ?? "", result.Error);
    }

    /// <summary>
    /// Request a quote for a dual investment product
    /// </summary>
    /// <param name="productId">Product ID</param>
    /// <param name="notionalSize">Investment size</param>
    /// <param name="notionalCurrency">Investment currency</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialDualInvestmentQuote>> RequestQuoteAsync(
        string productId,
        decimal notionalSize,
        string notionalCurrency,
        CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "productId", productId },
            { "notionalSz", notionalSize.ToOkxString() },
            { "notionalCcy", notionalCurrency },
        };

        return ProcessOneRequestAsync<OkxFinancialDualInvestmentQuote>(GetUri("api/v5/finance/sfp/dcd/quote"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Confirm a dual investment trade using a previously received quote
    /// </summary>
    /// <param name="quoteId">Quote ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialDualInvestmentTrade>> TradeAsync(string quoteId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "quoteId", quoteId },
        };

        return ProcessOneRequestAsync<OkxFinancialDualInvestmentTrade>(GetUri("api/v5/finance/sfp/dcd/trade"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Request an early redemption quote for a live dual investment order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialDualInvestmentRedeemQuote>> RequestRedeemQuoteAsync(string orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ordId", orderId },
        };

        return ProcessOneRequestAsync<OkxFinancialDualInvestmentRedeemQuote>(GetUri("api/v5/finance/sfp/dcd/redeem-quote"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Confirm an early redemption using the order ID and redeem quote ID
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="quoteId">Quote ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialDualInvestmentRedeem>> RedeemAsync(string orderId, string quoteId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ordId", orderId },
            { "quoteId", quoteId },
        };

        return ProcessOneRequestAsync<OkxFinancialDualInvestmentRedeem>(GetUri("api/v5/finance/sfp/dcd/redeem"), HttpMethod.Post, ct, signed: true, bodyParameters: parameters);
    }

    /// <summary>
    /// Get the current state of a dual investment order
    /// </summary>
    /// <param name="orderId">Order ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<OkxFinancialDualInvestmentOrderStatus>> GetOrderStatusAsync(string orderId, CancellationToken ct = default)
    {
        var parameters = new ParameterCollection
        {
            { "ordId", orderId },
        };

        return ProcessOneRequestAsync<OkxFinancialDualInvestmentOrderStatus>(GetUri("api/v5/finance/sfp/dcd/order-status"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }

    /// <summary>
    /// Get dual investment order history
    /// </summary>
    /// <param name="orderId">Order ID. When provided, returns that specific order directly and ignores other filters.</param>
    /// <param name="productId">Product ID, e.g. BTC-USDT-260327-77000-C</param>
    /// <param name="underlying">Underlying index, e.g. BTC-USD</param>
    /// <param name="state">Order state filter</param>
    /// <param name="beginId">Return records newer than this order ID</param>
    /// <param name="endId">Return records earlier than this order ID</param>
    /// <param name="beginTime">Begin timestamp filter, Unix timestamp format in milliseconds</param>
    /// <param name="endTime">End timestamp filter, Unix timestamp format in milliseconds</param>
    /// <param name="limit">Number of results per request. The maximum is 100.</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public Task<RestCallResult<List<OkxFinancialDualInvestmentOrder>>> GetOrderHistoryAsync(
        string? orderId = null,
        string? productId = null,
        string? underlying = null,
        OkxFinancialDualInvestmentOrderState? state = null,
        string? beginId = null,
        string? endId = null,
        long? beginTime = null,
        long? endTime = null,
        int limit = 100,
        CancellationToken ct = default)
    {
        limit.ValidateIntBetween(nameof(limit), 1, 100);

        var parameters = new ParameterCollection();
        parameters.AddOptional("ordId", orderId);
        parameters.AddOptional("productId", productId);
        parameters.AddOptional("uly", underlying);
        parameters.AddOptionalEnum("state", state);
        parameters.AddOptional("beginId", beginId);
        parameters.AddOptional("endId", endId);
        parameters.AddOptional("begin", beginTime?.ToOkxString());
        parameters.AddOptional("end", endTime?.ToOkxString());
        parameters.AddOptional("limit", limit.ToOkxString());

        return ProcessListRequestAsync<OkxFinancialDualInvestmentOrder>(GetUri("api/v5/finance/sfp/dcd/order-history"), HttpMethod.Get, ct, signed: true, queryParameters: parameters);
    }
}
