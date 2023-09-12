using OKX.Api.Extensions;
using OKX.Api.Models;
using System.Net;
using System.Net.WebSockets;

namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OKXWebSocketApiClient : OKXWebSocketApiBaseClient
{
    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OKXWebSocketApiTradingAccountClient TradingAccount { get; }

    /// <summary>
    /// OrderBook Trading Client
    /// </summary>
    public OKXWebSocketApiOrderBookTradingClient OrderBookTrading { get; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OKXWebSocketApiBlockTradingClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OKXWebSocketApiSpreadTradingClient SpreadTrading { get; }

    /// <summary>
    /// PublicData Client
    /// </summary>
    public OKXWebSocketApiPublicDataClient PublicData { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OKXWebSocketApiTradingStatisticsClient TradingStatistics { get; }

    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OKXWebSocketApiFundingAccountClient FundingAccount { get; }

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OKXWebSocketApiSubAccountClient SubAccount { get; }

    /// <summary>
    /// Financial Product Client
    /// </summary>
    public OKXWebSocketApiFinancialProductClient FinancialProduct { get; }

    /// <summary>
    /// Status Client
    /// </summary>
    public OKXWebSocketApiSystemClient Status { get; }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    public OKXWebSocketApiClient() : this(new OKXWebSocketApiClientOptions())
    {
    }

    /// <summary>
    /// OKXWebSocketApiClient Constructor
    /// </summary>
    /// <param name="options">OKXStreamClientOptions</param>
    public OKXWebSocketApiClient(OKXWebSocketApiClientOptions options) : base(options)
    {
        this.TradingAccount = new OKXWebSocketApiTradingAccountClient(this);
        this.OrderBookTrading = new OKXWebSocketApiOrderBookTradingClient(this);
        this.BlockTrading = new OKXWebSocketApiBlockTradingClient(this);
        this.SpreadTrading = new OKXWebSocketApiSpreadTradingClient(this);
        this.PublicData = new OKXWebSocketApiPublicDataClient(this);
        this.TradingStatistics = new OKXWebSocketApiTradingStatisticsClient(this);
        this.FundingAccount = new OKXWebSocketApiFundingAccountClient(this);
        this.SubAccount = new OKXWebSocketApiSubAccountClient(this);
        this.FinancialProduct = new OKXWebSocketApiFinancialProductClient(this);
        this.Status = new OKXWebSocketApiSystemClient(this);
    }

    internal Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        return SubscribeAsync<T>(request, identifier, authenticated, dataHandler, ct);
    } 
    
    internal Task<CallResult<T>> InternalQueryAsync<T>(string url, OkxBaseSocketRequest request, bool authenticated = true, bool sign = true)
    {
        var parameters = new Dictionary<string, string>();

        if (authenticated)
        {
            if (AuthenticationProvider == null)
                throw new InvalidOperationException("No credentials provided for authenticated endpoint");

            var authProvider = AuthenticationProvider  as OkxAuthenticationProvider;
            parameters = authProvider.AuthenticateWithSocketParameters(request.Arguments);

            if (sign)
            {
                //request.Arguments = parameters;
            }
            else
            {
                request.Arguments.Add("apiKey", authProvider.GetApiKey());
                request.Arguments.Add("passphrase", authProvider.GetPassPhrase());
            }
            base.ClientOptions.ApiCredentials = new OkxApiCredentials(authProvider.GetApiKey(), parameters["apiSecret"], authProvider.GetPassPhrase());
        }
        return QueryAsync<T>(url ,request, authenticated);
    }

}