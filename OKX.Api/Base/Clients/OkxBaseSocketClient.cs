namespace OKX.Api.Base;

/// <summary>
/// OKX WebSocket Api Base Client
/// </summary>
public abstract class OkxBaseSocketClient : WebSocketApiClient
{
    private const string ServiceUpgradeNoticeHandler = "service-upgrade-notice";
    private const string ChannelConnectionCountHandler = "channel-connection-count";

    /// <summary>
    /// Logger
    /// </summary>
    public ILogger Logger { get => _logger; }

    /// <summary>
    /// Client Options
    /// </summary>
    public OkxWebSocketApiOptions Options { get; }

    /// <summary>
    /// Raised when OKX reports that a public, private, or business WebSocket connection will close for a service upgrade.
    /// </summary>
    public event Action<OkxSocketServiceUpgradeNotice>? ServiceUpgradeNotice;

    /// <summary>
    /// Raised for server channel connection counts and connection-limit termination notices.
    /// A subscription acknowledgement does not guarantee that a subsequent limit error will not terminate it.
    /// No automatic reconnect or subscription restoration is performed by this event.
    /// </summary>
    public event Action<OkxSocketChannelConnectionCount>? ChannelConnectionCount;

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
        AddGenericHandler(ServiceUpgradeNoticeHandler, HandleServiceUpgradeNotice);
        AddGenericHandler(ChannelConnectionCountHandler, HandleChannelConnectionCount);
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

            var placeOrderRequest = op == "order" && request is OkxSocketRequest<OkxTradeOrderPlaceRequest> socRequest01 && socRequest01.RequestId == id && socRequest01.Operation == OkxSocketOperation.Order;
            var amendOrderRequest = op == "amend-order" && request is OkxSocketRequest<OkxTradeOrderAmendRequest> socRequest02 && socRequest02.RequestId == id && socRequest02.Operation == OkxSocketOperation.AmendOrder;
            var cancelOrderRequest = op == "cancel-order" && request is OkxSocketRequest<OkxTradeOrderCancelRequest> socRequest03 && socRequest03.RequestId == id && socRequest03.Operation == OkxSocketOperation.CancelOrder;
            var massCancelOrderRequest = op == "mass-cancel" && request is OkxSocketRequest<OkxTradeMassCancelRequest> socRequest04 && socRequest04.RequestId == id && socRequest04.Operation == OkxSocketOperation.MassCancel;
            var spreadPlaceOrderRequest = op == "sprd-order" && request is OkxSocketRequest<OkxSpreadOrderPlaceRequest> socRequest08 && socRequest08.RequestId == id && socRequest08.Operation == OkxSocketOperation.SpreadOrder;
            var spreadAmendOrderRequest = op == "sprd-amend-order" && request is OkxSocketRequest<OkxSpreadOrderAmendRequest> socRequest09 && socRequest09.RequestId == id && socRequest09.Operation == OkxSocketOperation.SpreadAmendOrder;
            var spreadCancelOrderRequest = op == "sprd-cancel-order" && request is OkxSocketRequest<OkxSpreadOrderCancelRequest> socRequest10 && socRequest10.RequestId == id && socRequest10.Operation == OkxSocketOperation.SpreadCancelOrder;
            var spreadMassCancelOrderRequest = op == "sprd-mass-cancel" && request is OkxSocketRequest<OkxSpreadMassCancelRequest> socRequest11 && socRequest11.RequestId == id && socRequest11.Operation == OkxSocketOperation.SpreadMassCancel;
            var placeBatchOrdersRequest = op == "batch-orders" && request is OkxSocketRequest<OkxTradeOrderPlaceRequest> socRequest05 && socRequest05.RequestId == id && socRequest05.Operation == OkxSocketOperation.BatchOrders;
            var amendBatchOrdersRequest = op == "batch-amend-orders" && request is OkxSocketRequest<OkxTradeOrderAmendRequest> socRequest06 && socRequest06.RequestId == id && socRequest06.Operation == OkxSocketOperation.BatchAmendOrders;
            var cancelBatchOrdersRequest = op == "batch-cancel-orders" && request is OkxSocketRequest<OkxTradeOrderCancelRequest> socRequest07 && socRequest07.RequestId == id && socRequest07.Operation == OkxSocketOperation.BatchCancelOrders;
            var singleRequest = placeOrderRequest || amendOrderRequest || cancelOrderRequest || massCancelOrderRequest
                || spreadPlaceOrderRequest || spreadAmendOrderRequest || spreadCancelOrderRequest || spreadMassCancelOrderRequest;
            var batchRequest = placeBatchOrdersRequest || amendBatchOrdersRequest || cancelBatchOrdersRequest;
            if (!singleRequest && !batchRequest) return false;

            ServerError? queryError = null;
            var codeText = data["code"]?.ToString();
            if (codeText is not null && codeText != "0")
            {
                var message = (string?)data["msg"];
                if (string.IsNullOrWhiteSpace(message)) message = $"OKX websocket error {codeText}";
                queryError = int.TryParse(codeText, out var code)
                    ? new ServerError(code, message!) : new ServerError($"{codeText}, {message}");

                // Only the documented Place/Amend aggregate outcomes may contain usable order data.
                var orderAcknowledgements = (placeOrderRequest || amendOrderRequest || placeBatchOrdersRequest || amendBatchOrdersRequest)
                    && codeText is "1" or "2" && data["data"] is JArray acknowledgements
                    && acknowledgements.Count > 0
                    && acknowledgements.All(item => item is JObject && int.TryParse(item["sCode"]?.ToString(), out var _));
                if (!orderAcknowledgements)
                {
                    callResult = new CallResult<T>(queryError, data.ToString());
                    return true;
                }
            }

            if (singleRequest)
            {
                var desResult = Deserialize<List<T>>(data["data"]!);
                if (!desResult)
                {
                    Logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                var item = desResult.Data.FirstOrDefault()!;
                if (queryError is not null && item is OkxRestApiErrorBase orderResult
                    && int.TryParse(orderResult.ErrorCode, out var itemCode) && itemCode != 0)
                {
                    var message = string.IsNullOrWhiteSpace(orderResult.ErrorMessage) ? $"OKX order error {itemCode}" : orderResult.ErrorMessage!;
                    var subCode = string.IsNullOrWhiteSpace(orderResult.SubCode) ? null : orderResult.SubCode;
                    if (subCode is not null) message = $"{message} (subCode: {subCode})";
                    queryError = new ServerError(itemCode, message, subCode);
                }
                callResult = queryError is null ? new CallResult<T>(item, data.ToString()) : new CallResult<T>(queryError, data.ToString()).As(item);
                return true;
            }

            if (batchRequest)
            {
                var desResult = Deserialize<T>(data["data"]!);
                if (!desResult)
                {
                    Logger.Log(LogLevel.Warning, $"Failed to deserialize data: {desResult.Error}. Data: {data}");
                    return false;
                }

                callResult = queryError is null ? new CallResult<T>(desResult.Data, data.ToString()) : new CallResult<T>(queryError, data.ToString()).As(desResult.Data);
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
            if (request is OkxSocketRequest socRequest
                && socRequest.Arguments is not null
                && TryDeserializeSocketRequestArgument(data["arg"], out var responseArgument))
            {
                foreach (var arg in socRequest.Arguments)
                {
                    if (SocketArgumentsMatch(arg, responseArgument!))
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
            if (!TryDeserializeSocketRequestArgument(message["arg"], out var resArg))
                return false;

            // Check Data
            var data = message["data"];
            if (data?.HasValues ?? false)
            {
                if (hRequest.Arguments is not null)
                {
                    foreach (var arg in hRequest.Arguments)
                    {
                        // Orders pushes may identify a concrete instrument within an ANY/type/family subscription.
                        // Subscribe/unsubscribe acknowledgements still require exact argument equality.
                        if (arg.Channel == "orders"
                            ? OrderPushArgumentsMatch(arg, resArg!)
                            : SocketArgumentsMatch(arg, resArg!))
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
        if (identifier == ChannelConnectionCountHandler)
            return message.Type == JTokenType.Object
                && ((string?)message["event"] == "channel-conn-count"
                    || (string?)message["event"] == "channel-conn-count-error");

        return identifier == ServiceUpgradeNoticeHandler
            && message.Type == JTokenType.Object
            && (string?)message["event"] == "notice"
            && (string?)message["code"] == "64008";
    }

    /// <inheritdoc />
    protected override async Task<bool> UnsubscribeAsync(WebSocketConnection connection, WebSocketSubscription subscription)
    {
        if (subscription is null || subscription.Request is null)
            return false;

        var request = new OkxSocketRequest(OkxSocketOperation.Unsubscribe, ((OkxSocketRequest)subscription.Request).Arguments);
        var unsubscribed = false;
        await connection.SendAndWaitAsync(request, TimeSpan.FromSeconds(10), data =>
        {
            if (data.Type != JTokenType.Object)
                return false;

            if ((string)data["event"]! == "unsubscribe" && TryDeserializeSocketRequestArgument(data["arg"], out var responseArgument))
            {
                foreach (var arg in request.Arguments)
                {
                    if (SocketArgumentsMatch(arg, responseArgument!))
                    {
                        unsubscribed = true;
                        return true;
                    }
                }
            }

            return false;
        });

        return unsubscribed;
    }
    #endregion

    #region Private Methods
    private static bool OrderPushArgumentsMatch(OkxSocketRequestArgument requestArgument, OkxSocketRequestArgument responseArgument)
    {
        return responseArgument.Channel == "orders"
            && responseArgument.InstrumentType.HasValue
            && (requestArgument.InstrumentType == OkxInstrumentType.Any
                || requestArgument.InstrumentType == responseArgument.InstrumentType)
            && (requestArgument.InstrumentFamily is null || requestArgument.InstrumentFamily == responseArgument.InstrumentFamily)
            && (requestArgument.InstrumentId is null || requestArgument.InstrumentId == responseArgument.InstrumentId)
            && requestArgument.Currency == responseArgument.Currency
            && requestArgument.AlgoOrderId == responseArgument.AlgoOrderId
            && requestArgument.SpreadId == responseArgument.SpreadId
            && DictionaryMatches(requestArgument.ExtraParameters, responseArgument.ExtraParameters);
    }

    private static bool SocketArgumentsMatch(OkxSocketRequestArgument requestArgument, OkxSocketRequestArgument responseArgument)
    {
        return requestArgument.Channel == responseArgument.Channel
            && requestArgument.Currency == responseArgument.Currency
            && requestArgument.AlgoOrderId == responseArgument.AlgoOrderId
            && requestArgument.InstrumentFamily == responseArgument.InstrumentFamily
            && requestArgument.InstrumentId == responseArgument.InstrumentId
            && requestArgument.SpreadId == responseArgument.SpreadId
            && requestArgument.InstrumentType == responseArgument.InstrumentType
            && DictionaryMatches(requestArgument.ExtraParameters, responseArgument.ExtraParameters);
    }

    private void HandleServiceUpgradeNotice(WebSocketMessageEvent message)
    {
        var result = Deserialize<OkxSocketServiceUpgradeNotice>(message.JsonData);
        if (!result)
        {
            Logger.Log(LogLevel.Warning, $"Failed to deserialize service upgrade notice: {result.Error}. Data: {message.JsonData}");
            return;
        }

        Logger.Log(LogLevel.Warning, $"WebSocket connection {result.Data.ConnectionId} will close for an OKX service upgrade: {result.Data.Message}");

        var handlers = ServiceUpgradeNotice;
        if (handlers is null)
            return;

        foreach (Action<OkxSocketServiceUpgradeNotice> handler in handlers.GetInvocationList())
        {
            try
            {
                handler(result.Data);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "A service upgrade notice handler failed");
            }
        }
    }

    private void HandleChannelConnectionCount(WebSocketMessageEvent message)
    {
        var result = Deserialize<OkxSocketChannelConnectionCount>(message.JsonData);
        if (!result)
        {
            Logger.LogWarning($"Failed to deserialize channel connection count: {result.Error}. Data: {message.JsonData}");
            return;
        }

        var notice = result.Data;
        Logger.Log(notice.IsLimitError ? LogLevel.Warning : LogLevel.Debug,
            $"WebSocket {notice.Event}: channel {notice.Channel}, connection {notice.ConnectionId}, count {notice.ConnectionCount}");

        var handlers = ChannelConnectionCount;
        if (handlers is null) return;
        foreach (Action<OkxSocketChannelConnectionCount> handler in handlers.GetInvocationList())
        {
            try
            {
                handler(notice);
            }
            catch (Exception exception)
            {
                Logger.LogError(exception, "A channel connection count handler failed");
            }
        }
    }

    private static bool DictionaryMatches(Dictionary<string, string>? left, Dictionary<string, string>? right)
    {
        if (ReferenceEquals(left, right))
            return true;

        if (left is null || right is null)
            return left is null && right is null;

        if (left.Count != right.Count)
            return false;

        foreach (var pair in left)
        {
            if (!right.TryGetValue(pair.Key, out var value) || value != pair.Value)
                return false;
        }

        return true;
    }

    private static bool TryDeserializeSocketRequestArgument(JToken? token, out OkxSocketRequestArgument? argument)
    {
        argument = null;
        if (token is null || token.Type != JTokenType.Object)
            return false;

        try
        {
            argument = token.ToObject<OkxSocketRequestArgument>(JsonSerializer.Create(SerializerOptions.WithConverters));
            return argument is not null;
        }
        catch
        {
            return false;
        }
    }

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
