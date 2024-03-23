namespace OKX.Api.Clients.WebSocketApi;

/// <summary>
/// OKX WebSocket Api Order Book Trading Client
/// </summary>
public class OKXWebSocketApiOrderBookTradingClient
{
    /// <summary>
    /// Trading Client
    /// </summary>
    public OKXWebSocketApiTradeClient Trade { get; }
    
    /// <summary>
    /// Algo Trading Client
    /// </summary>
    public OKXWebSocketApiAlgoTradingClient AlgoTrading { get; }

    /// <summary>
    /// Grid Trading Client
    /// </summary>
    public OKXWebSocketApiGridTradingClient GridTrading { get; }

    /// <summary>
    /// Recurring Buy Client
    /// </summary>
    public OKXWebSocketApiRecurringBuyClient RecurringBuy { get; }

    /// <summary>
    /// Copy Trading Client
    /// </summary>
    public OKXWebSocketApiCopyTradingClient CopyTrading { get; }

    /// <summary>
    /// Market Data Client
    /// </summary>
    public OKXWebSocketApiPublicClient MarketData { get; }

    // Root Client
    internal OKXWebSocketApiClient RootClient { get; }

    internal OKXWebSocketApiOrderBookTradingClient(OKXWebSocketApiClient root)
    {
        this.RootClient = root;

        Trade = new  OKXWebSocketApiTradeClient(root);
        AlgoTrading = new  OKXWebSocketApiAlgoTradingClient(root);
        GridTrading = new  OKXWebSocketApiGridTradingClient(root);
        RecurringBuy = new  OKXWebSocketApiRecurringBuyClient(root);
        CopyTrading = new  OKXWebSocketApiCopyTradingClient(root);
        MarketData = root.Public;
    }

}