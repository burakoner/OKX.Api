﻿namespace OKX.Api.Base;

/// <summary>
/// OKX WebSocket Api Base Client
/// </summary>
public abstract class OkxBaseSocketClient : WebSocketApiClient
{
    /// <summary>
    /// Logger
    /// </summary>
    public ILogger Logger { get => _logger; }

    /// <summary>
    /// Client Options
    /// </summary>
    public OkxWebSocketApiOptions Options { get; }

    /// <summary>
    /// If Websocket is authendicated
    /// </summary>
    public bool IsAuthendicated { get; private set; }

    /// <summary>
    /// OKXWebSocketBaseClient Constructor
    /// </summary>
    public OkxBaseSocketClient() : this(null, new OkxWebSocketApiOptions())
    {
    }

    /// <summary>
    /// OKXWebSocketBaseClient Constructor
    /// </summary>
    /// <param name="options">Options</param>
    public OkxBaseSocketClient(OkxWebSocketApiOptions options) : this(null, options)
    {
    }

    /// <summary>
    /// OKXWebSocketBaseClient Constructor
    /// </summary>
    /// <param name="logger">ILogger</param>
    /// <param name="options">Options</param>
    public OkxBaseSocketClient(ILogger? logger, OkxWebSocketApiOptions options) : base(logger ?? LoggerFactory.CreateLogger("OKX.Api"), options)
    {
        RateLimitPerConnectionPerSecond = 4;
        IgnoreHandlingList = ["pong"];
        Options = options;

        SetDataInterpreter(DecompressData, null);
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");
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

        // Credentials
        var creds = AuthenticationProvider.Credentials;

        // Check Point
        if (creds is null || creds.Key is null || creds.Secret is null || ((OkxApiCredentials)creds).PassPhrase is null)
            return new CallResult<bool>(new NoApiCredentialsError());

        // Get Credentials
        var key = creds.Key.GetString();
        var secret = creds.Secret.GetString();
        var passphrase = ((OkxApiCredentials)creds).PassPhrase.GetString();

        // Check Point
        if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(secret) || string.IsNullOrEmpty(passphrase))
            return new CallResult<bool>(new NoApiCredentialsError());

        // Timestamp
        var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(OkxConstants.OkxCultureInfo);

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
            if ((string)data!["event"]! != "login")
                return false;

            var authResponse = Deserialize<OkxSocketResponse>(data);
            if (!authResponse)
            {
                Logger.Log(LogLevel.Warning, "Authorization failed: " + authResponse.Error);
                result = new CallResult<bool>(authResponse.Error!);
                return true;
            }
            if (!authResponse.Data.Success)
            {
                Logger.Log(LogLevel.Warning, "Authorization failed: " + authResponse.Error!.Message);
                result = new CallResult<bool>(new ServerError(authResponse.Error.Code!.Value, authResponse.Error.Message));
                return true;
            }

            Logger.Log(LogLevel.Debug, "Authorization completed");
            result = new CallResult<bool>(true);

            IsAuthendicated = true;
            return true;
        });

        return result;
    }

    /// <inheritdoc />
    protected override bool HandleQueryResponse<T>(WebSocketConnection connection, object request, JToken data, out CallResult<T>? callResult)
    {
        callResult = null;

        // Ping Request
        if (request.ToString() == "ping" && data.ToString() == "pong")
        {
            return true;
        }

        // Web Socket Orders
        if (data["id"] is not null && data["op"] is not null)
        {
            var id = (string)data["id"]!;
            var op = (string)data["op"]!;
            var placeOrderRequest = op == "order" && request is OkxSocketRequest<OkxTradeOrderPlaceRequest> socRequest01 && socRequest01.RequestId == id;
            var amendOrderRequest = op == "amend-order" && request is OkxSocketRequest<OkxTradeOrderAmendRequest> socRequest02 && socRequest02.RequestId == id;
            var cancelOrderRequest = op == "cancel-order" && request is OkxSocketRequest<OkxTradeOrderCancelRequest> socRequest03 && socRequest03.RequestId == id;
            var massCancelOrderRequest = op == "mass-cancel" && request is OkxSocketRequest<OkxTradeMassCancelRequest> socRequest04 && socRequest04.RequestId == id;
            if (placeOrderRequest || amendOrderRequest || cancelOrderRequest || massCancelOrderRequest)
            {
                var desResult = Deserialize<List<T>>(data["data"]!);
                if (!desResult)
                {
                    Logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data.FirstOrDefault()!);
                return true;
            }

            var placeBatchOrdersRequest = op == "batch-orders" && request is OkxSocketRequest<OkxTradeOrderPlaceRequest> socRequest05 && socRequest05.RequestId == id;
            var amendBatchOrdersRequest = op == "batch-amend-orders" && request is OkxSocketRequest<OkxTradeOrderPlaceRequest> socRequest06 && socRequest06.RequestId == id;
            var cancelBatchOrdersRequest = op == "batch-cancel-orders" && request is OkxSocketRequest<OkxTradeOrderPlaceRequest> socRequest07 && socRequest07.RequestId == id;
            if (placeBatchOrdersRequest || amendBatchOrdersRequest || cancelBatchOrdersRequest)
            {
                var desResult = Deserialize<T>(data["data"]!);
                if (!desResult)
                {
                    Logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = new CallResult<T>(desResult.Data);
                return true;
            }
        }

        // Check for Error
        if (data is JObject && data["event"] is not null && (string)data["event"]! == "error" && data["code"] is not null && data["msg"] is not null)
        {
            Logger.Log(LogLevel.Warning, "Query failed: " + (string)data["msg"]!);
            callResult = new CallResult<T>(new ServerError($"{(string)data["code"]!}, {(string)data["msg"]!}"));
            return true;
        }

        // Login Request
        if (data is JObject && data["event"] is not null && (string)data["event"]! == "login")
        {
            var desResult = Deserialize<T>(data);
            if (!desResult)
            {
                Logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                return false;
            }

            callResult = new CallResult<T>(desResult.Data);
            return true;
        }

        return false;
    }

    /// <inheritdoc />
    protected override bool HandleSubscriptionResponse(WebSocketConnection connection, WebSocketSubscription subscription, object request, JToken data, out CallResult<object>? callResult)
    {
        callResult = null;

        // Ping-Pong
        var json = data.ToString();
        if (json == "pong")
            return false;

        // Check for Error
        // 30040: {0} Channel : {1} doesn't exist
        if (data.HasValues && data["event"] is not null && (string)data["event"]! == "error" &&
            data["msg"] is not null && data["code"] is not null)
        {
            Logger.Log(LogLevel.Warning, "Subscription failed: " + (string)data["msg"]!);
            callResult = new CallResult<object>(new ServerError(data["code"]!.ToIntegerSafe(), (string)data["msg"]!));
            return true;
        }

        // Check for Success
        if (data.HasValues && data["event"] is not null && (string)data["event"]! == "subscribe" && data["arg"]!["channel"] is not null)
        {
            if (request is OkxSocketRequest socRequest && socRequest.Arguments is not null)
            {
                foreach (var arg in socRequest.Arguments)
                {
                    if (arg.Channel == (string)data["arg"]!["channel"]!)
                    {
                        Logger.Log(LogLevel.Debug, "Subscription completed");
                        callResult = new CallResult<object>(true);
                        return true;
                    }
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
            if (message is JObject && message["event"] is not null && (string)message["event"]! == "error" && message["code"] is not null && message["msg"] is not null)
                return false;

            // Check for Channel
            if (hRequest.Operation != OkxSocketOperation.Subscribe || message["arg"] is null || message["arg"]!["channel"] is null)
                return false;

            // Compare Request and Response Arguments
            var resArg = JsonConvert.DeserializeObject<OkxSocketRequestArgument>(message["arg"]!.ToString());
            if (resArg is null) return false;

            // Check Data
            var data = message["data"];
            if (data?.HasValues ?? false)
            {
                if (hRequest.Arguments is not null)
                {
                    foreach (var arg in hRequest.Arguments)
                    {
                        if (arg.Channel == resArg.Channel &&
                            arg.InstrumentId == resArg.InstrumentId &&
                            arg.InstrumentType == resArg.InstrumentType &&
                            arg.InstrumentFamily == resArg.InstrumentFamily)
                        {
                            return true;
                        }
                    }
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
        if (subscription is null || subscription.Request is null)
            return false;

        var request = new OkxSocketRequest(OkxSocketOperation.Unsubscribe, ((OkxSocketRequest)subscription.Request).Arguments);
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
        {
            if (data.Type != JTokenType.Object)
                return false;

            if ((string)data["event"]! == "unsubscribe")
            {
                foreach (var arg in request.Arguments)
                {
                    if ((string)data["arg"]!["channel"]! == arg.Channel)
                        return true;
                }
            }

            return false;
        });

        return false;
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

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="apiKey">The api key</param>
    /// <param name="apiSecret">The api secret</param>
    /// <param name="passPhrase">The passphrase you specified when creating the API key</param>
    public void SetApiCredentials(string apiKey, string apiSecret, string passPhrase)
    {
        var credentials = new OkxApiCredentials(apiKey, apiSecret, passPhrase);
        ClientOptions.ApiCredentials = credentials;
        SetApiCredentials(credentials);
    }

    #region Ping
    /// <summary>
    /// Send Ping Request
    /// </summary>
    /// <returns></returns>
    public async Task<CallResult<OkxSocketPingPong>> PingAsync()
    {
        var pit = DateTime.UtcNow;
        var sw = Stopwatch.StartNew();
        var response = await QueryAsync<string>("ping", false).ConfigureAwait(true);
        var pot = DateTime.UtcNow;
        sw.Stop();

        var result = new OkxSocketPingPong { PingTime = pit, PongTime = pot, Latency = sw.Elapsed, PongMessage = response.Data };
        return response.Error is not null ? new CallResult<OkxSocketPingPong>(response.Error) : new CallResult<OkxSocketPingPong>(result);
    }
    #endregion
}