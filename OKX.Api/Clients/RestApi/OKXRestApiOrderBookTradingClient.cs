namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Rest Api Order Book Trading Client
/// </summary>
public class OKXRestApiOrderBookTradingClient
{
    /// <summary>
    /// Trading Client
    /// </summary>
    public OKXRestApiTradeClient Trade { get; }

    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OKXRestApiAlgoTradingClient AlgoTrading { get; }

    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OKXRestApiGridTradingClient GridTrading { get; }

    /// <summary>
    /// Recurring Buy Client
    /// </summary>
    public OKXRestApiRecurringBuyClient RecurringBuy { get; }

    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OKXRestApiCopyTradingClient CopyTrading { get; }

    /// <summary>
    /// Market Data Client
    /// </summary>
    public OKXRestApiPublicClient MarketData { get; }

    /// <summary>
    /// OKXOrderBookTradingRestApiClient Constructor
    /// </summary>
    public OKXRestApiOrderBookTradingClient(OKXRestApiClient root)
    {
        Trade = new OKXRestApiTradeClient(root);
        AlgoTrading = new OKXRestApiAlgoTradingClient(root);
        GridTrading = new OKXRestApiGridTradingClient(root);
        RecurringBuy = new OKXRestApiRecurringBuyClient(root);
        CopyTrading = new OKXRestApiCopyTradingClient(root);
        MarketData = root.PublicData;
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        Trade.SetApiCredentials(credentials);
        AlgoTrading.SetApiCredentials(credentials);
        GridTrading.SetApiCredentials(credentials);
        RecurringBuy.SetApiCredentials(credentials);
        GridTrading.SetApiCredentials(credentials);
        CopyTrading.SetApiCredentials(credentials);
        MarketData.SetApiCredentials(credentials);
    }
}
