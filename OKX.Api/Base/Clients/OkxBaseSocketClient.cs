namespace OKX.Api.Base;

/// <summary>
/// OKX WebSocket Api Base Client
/// </summary>
public abstract class OkxBaseSocketClient : WebSocketApiClient
{
    private const string ServiceUpgradeNoticeHandler = "service-upgrade-notice";
    private const string ChannelConnectionCountHandler = "channel-connection-count";
    private const string SubscriptionAcknowledgementHandler = "subscription-acknowledgement";

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
        // Intermediate ACKs keep their pending request open and must not become unhandled-message warnings.
        AddGenericHandler(SubscriptionAcknowledgementHandler,
            message => Logger.LogDebug("WebSocket subscription acknowledgement: {Acknowledgement}", message.JsonData));
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");
    }

    #region Overrided Methods
    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

    /// <inheritdoc />
    protected override async Task<CallResult<bool>> AuthenticateAsync(WebSocketConnection connection)
    {
        IsAuthendicated = false;
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
            if (data.Type != JTokenType.Object || data["event"]?.Type != JTokenType.String
                || ((string?)data["event"] != "login" && !IsAuthenticationError(data)))
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
                var message = authResponse.Data.ErrorMessage;
                Logger.Log(LogLevel.Warning, "Authorization failed: " + message);
                var codeText = authResponse.Data.ErrorCode;
                var error = int.TryParse(codeText, out var code)
                    ? new ServerError(code, message) : new ServerError($"{codeText}, {message}");
                result = new CallResult<bool>(error, data.ToString());
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

        if (data.Type != JTokenType.Object)
            return false;

        // Web Socket Orders
        if (data["id"]?.Type == JTokenType.String && data["op"]?.Type == JTokenType.String)
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

        // Unidentified event:error messages are not evidence of a trading-query outcome.
        // Authentication has its own response wait; login replies cannot complete trading queries either.
        if (request is OkxSocketAuthRequest { Operation: OkxSocketOperation.Login }
            && data["event"]?.Type == JTokenType.String && (string?)data["event"] == "login")
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
        if (request is not OkxSocketRequest { Operation: OkxSocketOperation.Subscribe } socketRequest)
            return false;
        if (socketRequest.Arguments is null)
            return false;
        // A standalone response cannot confirm an entire multi-argument request.
        // SubscribeAndWaitAsync keeps the remaining arguments local to each send attempt.
        return HandleSubscriptionAcknowledgement(socketRequest, data, socketRequest.Arguments.ToList(), out callResult);
    }

    /// <inheritdoc />
    public override async Task<CallResult<bool>> SubscribeAndWaitAsync(WebSocketConnection connection, object request, WebSocketSubscription subscription)
    {
        if (request is not OkxSocketRequest { Operation: OkxSocketOperation.Subscribe } socketRequest)
            return await base.SubscribeAndWaitAsync(connection, request, subscription).ConfigureAwait(false);

        subscription.Confirmed = false;
        if (socketRequest.Arguments is null || socketRequest.Arguments.Count == 0)
            return new CallResult<bool>(new InvalidOperationError("A subscription requires at least one argument."));

        // Keep the stored request unchanged. SDK reconnects call this method again, with fresh ACK state/ID.
        var wireRequest = socketRequest with { RequestId = socketRequest.RequestId ?? Guid.NewGuid().ToString("N") };
        var response = await WaitForSubscriptionAcknowledgementAsync(connection, wireRequest, ClientOptions.ResponseTimeout).ConfigureAwait(false);

        if (response.Success)
        {
            subscription.Confirmed = true;
            return new CallResult<bool>(true, response.Raw);
        }

        return new CallResult<bool>(response.Error!, response.Raw);
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
        if (identifier == SubscriptionAcknowledgementHandler)
            return message.Type == JTokenType.Object && message["event"]?.Type == JTokenType.String
                && (string?)message["event"] is "subscribe" or "unsubscribe"
                && (string.IsNullOrEmpty(message["code"]?.ToString()) || message["code"]!.ToString() == "0")
                && TryDeserializeSocketRequestArgument(message["arg"], out var argument) && !string.IsNullOrEmpty(argument!.Channel);

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

        if (subscription.Request is not OkxSocketRequest subscriptionRequest
            || subscriptionRequest.Arguments is null || subscriptionRequest.Arguments.Count == 0)
            return false;

        var request = new OkxSocketRequest(OkxSocketOperation.Unsubscribe, subscriptionRequest.Arguments);
        // Orders documents id for both operations; the general unsubscribe table does not.
        if (request.Arguments.All(argument => argument.Channel == "orders"))
            request.RequestId = Guid.NewGuid().ToString("N");
        var response = await WaitForSubscriptionAcknowledgementAsync(connection, request, TimeSpan.FromSeconds(10)).ConfigureAwait(false);
        return response.Success;
    }
    #endregion

    #region Private Methods
    private static async Task<CallResult<object>> WaitForSubscriptionAcknowledgementAsync(
        WebSocketConnection connection, OkxSocketRequest request, TimeSpan timeout)
    {
        var remaining = request.Arguments.ToList();
        var acknowledgementLock = new object();
        var waiting = true;
        CallResult<object>? response = null;
        try
        {
            await connection.SendAndWaitAsync(request, timeout, data =>
            {
                lock (acknowledgementLock)
                    return waiting && response is null && HandleSubscriptionAcknowledgement(request, data, remaining, out response);
            }).ConfigureAwait(false);
        }
        finally
        {
            // ApiSharp can retain completed/timed-out pending handlers temporarily. Late ACKs must not consume
            // a subsequent attempt, overwrite a completed outcome, or resurrect its confirmation state.
            lock (acknowledgementLock) waiting = false;
        }

        return response ?? new CallResult<object>(new ServerError(
            $"{request.Operation} not confirmed: {remaining.Count} argument(s) lack acknowledgement; outcome is uncertain."));
    }

    private static bool IsAuthenticationError(JToken data)
        => (string?)data["event"] == "error" && data["id"] is null && data["op"] is null && data["arg"] is null
            && int.TryParse(data["code"]?.ToString(), out var code)
            // Only documented authentication-specific codes, never an ambiguous subscription/request error.
            && code is 60004 or 60005 or 60006 or 60007 or 60009 or 60023 or 60024 or 60026 or 60031 or 60032 or 63999;

    private static bool HandleSubscriptionAcknowledgement(OkxSocketRequest request, JToken data,
        List<OkxSocketRequestArgument> remaining, out CallResult<object>? result)
    {
        result = null;
        if (data.Type != JTokenType.Object || data["event"]?.Type != JTokenType.String || remaining.Count == 0)
            return false;

        var responseId = data["id"];
        if (responseId is not null && responseId.Type is not (JTokenType.String or JTokenType.Null))
            return false;
        if (!string.Equals(request.RequestId, (string?)responseId, StringComparison.Ordinal))
            return false;

        var operation = request.Operation == OkxSocketOperation.Subscribe ? "subscribe" : "unsubscribe";
        if (data["op"] is not null && (data["op"]!.Type != JTokenType.String || (string?)data["op"] != operation))
            return false;

        var eventName = (string?)data["event"];
        if (eventName != operation && eventName != "error")
            return false;
        var codeText = data["code"]?.ToString();
        if (eventName == "error" || (!string.IsNullOrEmpty(codeText) && codeText != "0"))
        {
            // Unidentified errors must not complete an unrelated pending request.
            if (string.IsNullOrEmpty(request.RequestId))
                return false;
            var message = data["msg"]?.ToString();
            if (string.IsNullOrWhiteSpace(message)) message = data.ToString();
            var error = int.TryParse(codeText, out var code)
                ? new ServerError(code, message!) : new ServerError(string.IsNullOrEmpty(codeText) ? message! : $"{codeText}, {message}");
            result = new CallResult<object>(error, data.ToString());
            return true;
        }

        if (!TryDeserializeSocketRequestArgument(data["arg"], out var argument) || string.IsNullOrEmpty(argument!.Channel))
            return false;
        if (remaining.RemoveAll(expected => SocketArgumentsMatch(expected, argument!)) == 0 || remaining.Count != 0)
            return false;

        result = new CallResult<object>(true, data.ToString());
        return true;
    }

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
