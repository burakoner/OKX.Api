using OKX.Api.Clients.WebSocketApi;
using OKX.Api.Models;
using OKX.Api.Models.AlgoTrading;
using OKX.Api.Models.MarketData;
using OKX.Api.Models.PublicData;
using OKX.Api.Models.System;
using OKX.Api.Models.Trade;

namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OKXWebSocketApiClient : WebSocketApiClient
{
    /// <summary>
    /// Trading Account Client
    /// </summary>
    public OKXWebSocketTradingAccountClient TradingAccount { get; }

    /// <summary>
    /// OrderBook Trading Client
    /// </summary>
    public OKXWebSocketOrderBookTradingClient OrderBookTrading { get; }

    /// <summary>
    /// Block Trading Client
    /// </summary>
    public OKXWebSocketBlockTradingClient BlockTrading { get; }

    /// <summary>
    /// Spread Trading Client
    /// </summary>
    public OKXWebSocketSpreadTradingClient SpreadTrading { get; }

    /// <summary>
    /// PublicData Client
    /// </summary>
    public OKXWebSocketPublicDataClient PublicData { get; }

    /// <summary>
    /// Trading Statistics Client
    /// </summary>
    public OKXWebSocketTradingStatisticsClient TradingStatistics { get; }

    /// <summary>
    /// Funding Account Client
    /// </summary>
    public OKXWebSocketFundingAccountClient FundingAccount { get; }

    /// <summary>
    /// SubAccount Client
    /// </summary>
    public OKXWebSocketSubAccountClient SubAccount { get; }

    /// <summary>
    /// Financial Product Client
    /// </summary>
    public OKXWebSocketFinancialProductClient FinancialProduct { get; }

    /// <summary>
    /// Status Client
    /// </summary>
    public OKXWebSocketSystemClient Status { get; }

    /// <summary>
    /// If Websocket is authendicated
    /// </summary>
    internal bool IsAuthendicated { get; private set; }

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
    public OKXWebSocketApiClient(OKXWebSocketApiClientOptions options) : base("OKX Stream", options)
    {
        RateLimitPerConnectionPerSecond = 4;
        this.IgnoreHandlingList = new List<string> { "pong" };

        SetDataInterpreter(DecompressData, null);
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");

        this.TradingAccount = new OKXWebSocketTradingAccountClient(this);
        this.OrderBookTrading = new OKXWebSocketOrderBookTradingClient(this);
        this.BlockTrading = new OKXWebSocketBlockTradingClient(this);
        this.SpreadTrading = new OKXWebSocketSpreadTradingClient(this);
        this.PublicData = new OKXWebSocketPublicDataClient(this);
        this.TradingStatistics = new OKXWebSocketTradingStatisticsClient(this);
        this.FundingAccount = new OKXWebSocketFundingAccountClient(this);
        this.SubAccount = new OKXWebSocketSubAccountClient(this);
        this.FinancialProduct = new OKXWebSocketFinancialProductClient(this);
        this.Status = new OKXWebSocketSystemClient(this);
    }

    #region Overrided Methods
    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

    /// <inheritdoc />
    protected override async Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection connection)
    {
        // Check Point
        // if (connection.Authenticated)
        //    return new CallResult<bool>(true, null);

        // Check Point
        if (ClientOptions.ApiCredentials == null || ClientOptions.ApiCredentials.Key == null || ClientOptions.ApiCredentials.Secret == null || ((OkxApiCredentials)ClientOptions.ApiCredentials).PassPhrase == null)
            return new CallResult<bool>(new NoApiCredentialsError());

        // Get Credentials
        var key = ClientOptions.ApiCredentials.Key.GetString();
        var secret = ClientOptions.ApiCredentials.Secret.GetString();
        var passphrase = ((OkxApiCredentials)ClientOptions.ApiCredentials).PassPhrase.GetString();

        // Check Point
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(passphrase))
            return new CallResult<bool>(new NoApiCredentialsError());

        // Timestamp
        var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);

        // Signature
        var signtext = timestamp + "GET" + "/users/self/verify";
        var hmacEncryptor = new HMACSHA256(Encoding.ASCII.GetBytes(secret));
        var signature = OkxAuthenticationProvider.Base64Encode(hmacEncryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));
        var request = new OkxSocketAuthRequest(OkxSocketOperation.Login, new OkxSocketAuthRequestArgument
        {
            ApiKey = key,
            Passphrase = passphrase,
            Timestamp = timestamp,
            Signature = signature,
        });

        // Try to Login
        var result = new CallResult<bool>(new ServerError("No response from server"));
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
        {
            if ((string)data["event"] != "login")
                return false;

            var authResponse = Deserialize<OkxSocketResponse>(data);
            if (!authResponse)
            {
                log.Write(LogLevel.Warning, "Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error);
                return true;
            }
            if (!authResponse.Data.Success)
            {
                log.Write(LogLevel.Warning, "Authorization failed: " + authResponse.Error.Message);
                result = new CallResult<bool>(new ServerError(authResponse.Error.Code.Value, authResponse.Error.Message));
                return true;
            }

            log.Write(LogLevel.Debug, "Authorization completed");
            result = new CallResult<bool>(true);

            IsAuthendicated = true;
            return true;
        });

        return result;
    }

    /// <inheritdoc />
    protected override bool HandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T> callResult)
    {
        callResult = null;

        // Ping Request
        if (request.ToString() == "ping" && data.ToString() == "pong")
        {
            return true;
        }

        // Check for Error
        if (data is JObject && data["event"] != null && (string)data["event"]! == "error" && data["code"] != null && data["msg"] != null)
        {
            log.Write(LogLevel.Warning, "Query failed: " + (string)data["msg"]!);
            callResult = new CallResult<T>(new ServerError($"{(string)data["code"]!}, {(string)data["msg"]!}"));
            return true;
        }

        // Login Request
        if (data is JObject && data["event"] != null && (string)data["event"]! == "login")
        {
            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                log.Write(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            callResult = new CallResult<T>(desResult.Data);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool HandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken data, out CallResult<object> callResult)
    {
        callResult = null;

        // Ping-Pong
        var json = data.ToString();
        if (json == "pong")
            return false;

        // Check for Error
        // 30040: {0} Channel : {1} doesn't exist
        if (data.HasValues && data["event"] != null && (string)data["event"]! == "error" && data["errorCode"] != null && (string)data["errorCode"]! == "30040")
        {
            log.Write(LogLevel.Warning, "Subscription failed: " + (string)data["message"]!);
            callResult = new CallResult<object>(new ServerError($"{(string)data["errorCode"]!}, {(string)data["message"]!}"));
            return true;
        }

        // Check for Success
        if (data.HasValues && data["event"] != null && (string)data["event"]! == "subscribe" && data["arg"]["channel"] != null)
        {
            if (request is OkxSocketRequest socRequest)
            {
                if (socRequest.Arguments.FirstOrDefault().Channel == (string)data["arg"]["channel"]!)
                {
                    log.Write(LogLevel.Debug, "Subscription completed");
                    callResult = new CallResult<object>(true);
                    return true;
                }
            }
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, object request)
    {
        // Ping Request
        if (request.ToString() == "ping" && message.ToString() == "pong")
            return true;

        // Check Point
        if (message.Type != JTokenType.Object)
            return false;

        // Socket Request
        if (request is OkxSocketRequest hRequest)
        {
            // Check for Error
            if (message is JObject && message["event"] != null && (string)message["event"]! == "error" && message["code"] != null && message["msg"] != null)
                return false;

            // Check for Channel
            if (hRequest.Operation != OkxSocketOperation.Subscribe || message["arg"]["channel"] == null)
                return false;

            // Compare Request and Response Arguments
            var reqArg = hRequest.Arguments.FirstOrDefault();
            var resArg = JsonConvert.DeserializeObject<OkxSocketRequestArgument>(message["arg"].ToString());

            // Check Data
            var data = message["data"];
            if (data?.HasValues ?? false)
            {
                if (reqArg.Channel == resArg.Channel &&
                    reqArg.InstrumentId == resArg.InstrumentId &&
                    reqArg.InstrumentType == resArg.InstrumentType &&
                    reqArg.InstrumentFamily == resArg.InstrumentFamily)
                {
                    return true;
                }
            }
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool MessageMatchesHandler(WebSocketConnection connection, JToken message, string identifier)
    {
        return true;
    }

    /// <inheritdoc />
    protected override async Task<bool> UnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
    {
        if (subscription == null || subscription.Request == null)
            return false;

        var request = new OkxSocketRequest(OkxSocketOperation.Unsubscribe, ((OkxSocketRequest)subscription.Request).Arguments);
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
        {
            if (data.Type != JTokenType.Object)
                return false;

            if ((string)data["event"] == "unsubscribe")
            {
                return (string)data["arg"]["channel"] == request.Arguments.FirstOrDefault().Channel;
            }

            return false;
        });
        return false;
    }

    /// <inheritdoc />
    protected override async Task<CallResult<WebSocketConnection>> GetWebSocketConnection(string address, bool authenticated)
    {
        address = authenticated
            ? "wss://ws.okx.com:8443/ws/v5/private"
            : "wss://ws.okx.com:8443/ws/v5/public";

        if (((OKXWebSocketApiClientOptions)ClientOptions).DemoTradingService)
        {
            address = authenticated
                ? "wss://wspap.okx.com:8443/ws/v5/private?brokerId=9999"
                : "wss://wspap.okx.com:8443/ws/v5/public?brokerId=9999";
        }

        return await base.GetWebSocketConnection(address, authenticated);
    }
    #endregion

    #region Private Methods
    private string DecompressData(byte[] byteData)
    {
        using var decompressedStream = new MemoryStream();
        using var compressedStream = new MemoryStream(byteData);
        using var deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress);
        deflateStream.CopyTo(decompressedStream);
        decompressedStream.Position = 0;

        using var streamReader = new StreamReader(decompressedStream);
        return streamReader.ReadToEnd();
    }
    #endregion

    #region Root Methods
    internal Task<CallResult<WebSocketUpdateSubscription>> RootSubscribeAsync<T>(object request, string identifier, bool authenticated, Action<WebSocketDataEvent<T>> dataHandler, CancellationToken ct)
    {
        return SubscribeAsync<T>(request, identifier, authenticated, dataHandler, ct);
    }
    #endregion

    #region Ping
    /// <summary>
    /// Send Ping Request
    /// </summary>
    /// <returns></returns>
    public virtual async Task<CallResult<OkxSocketPingPong>> PingAsync()
    {
        var pit = DateTime.UtcNow;
        var sw = Stopwatch.StartNew();
        var response = await QueryAsync<string>("ping", false).ConfigureAwait(true);
        var pot = DateTime.UtcNow;
        sw.Stop();

        var result = new OkxSocketPingPong { PingTime = pit, PongTime = pot, Latency = sw.Elapsed, PongMessage = response.Data };
        return response.Error != null ? new CallResult<OkxSocketPingPong>(response.Error) : new CallResult<OkxSocketPingPong>(result);
    }
    #endregion

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="apiKey">The api key</param>
    /// <param name="apiSecret">The api secret</param>
    /// <param name="passPhrase">The passphrase you specified when creating the API key</param>
    public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        SetApiCredentials(new OkxApiCredentials(apiKey, apiSecret, passPhrase));
    }

}