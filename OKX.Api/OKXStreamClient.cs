namespace OKX.Api;

/// <summary>
/// OKX WebSocket Client
/// </summary>
public class OKXStreamClient : StreamApiClient
{
    internal bool IsAuthendicated { get; private set; }

    /// <summary>
    /// OKXStreamClient Constructor
    /// </summary>
    public OKXStreamClient() : this(new OKXStreamClientOptions())
    {
    }

    /// <summary>
    /// OKXStreamClient Constructor
    /// </summary>
    /// <param name="options">OKXStreamClientOptions</param>
    public OKXStreamClient(OKXStreamClientOptions options) : base("OKX Stream", options)
    {
        RateLimitPerConnectionPerSecond = 4;
        this.IgnoreHandlingList = new List<string> { "pong" };

        SetDataInterpreter(DecompressData, null);
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");
    }

    #region Overrided Methods
    /// <inheritdoc />
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

    /// <inheritdoc />
    protected override async Task<CallResult<bool>> AuthenticateAsync(StreamConnection connection)
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
    protected override bool HandleQueryResponse<T>(StreamConnection connection, object request, JToken data, out CallResult<T> callResult)
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
    protected override bool HandleSubscriptionResponse(StreamConnection connection, StreamSubscription subscription, object request, JToken data, out CallResult<object> callResult)
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
    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, object request)
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
    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, string identifier)
    {
        return true;
    }

    /// <inheritdoc />
    protected override async Task<bool> UnsubscribeAsync(StreamConnection connection, StreamSubscription subscription)
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
    protected override async Task<CallResult<StreamConnection>> GetStreamConnection(string address, bool authenticated)
    {
        address = authenticated
            ? "wss://ws.okx.com:8443/ws/v5/private"
            : "wss://ws.okx.com:8443/ws/v5/public";

        if (((OKXStreamClientOptions)ClientOptions).DemoTradingService)
        {
            address = authenticated
                ? "wss://wspap.okx.com:8443/ws/v5/private?brokerId=9999"
                : "wss://wspap.okx.com:8443/ws/v5/public?brokerId=9999";
        }

        return await base.GetStreamConnection(address, authenticated);
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

    #region Public Channels
    /// <summary>
    /// The full instrument list will be pushed for the first time after subscription. Subsequently, the instruments will be pushed if there's any change to the instrument’s state (such as delivery of FUTURES, exercise of OPTION, listing of new contracts / trading pairs, trading suspension, etc.).
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToInstrumentsAsync(Action<OkxInstrument> onData, OkxInstrumentType instrumentType, CancellationToken ct = default)
        => await SubscribeToInstrumentsAsync(onData, new OkxInstrumentType[] { instrumentType }, ct).ConfigureAwait(false);

    /// <summary>
    /// The full instrument list will be pushed for the first time after subscription. Subsequently, the instruments will be pushed if there's any change to the instrument’s state (such as delivery of FUTURES, exercise of OPTION, listing of new contracts / trading pairs, trading suspension, etc.).
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentTypes">List of Instrument Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToInstrumentsAsync(Action<OkxInstrument> onData, IEnumerable<OkxInstrumentType> instrumentTypes, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxInstrument>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentType in instrumentTypes) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "instruments",
            InstrumentType = instrumentType,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the last traded price, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(Action<OkxTicker> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToTickersAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the last traded price, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(Action<OkxTicker> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "tickers",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the open interest. Data will by pushed every 3 seconds.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestsAsync(Action<OkxOpenInterest> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToOpenInterestsAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the open interest. Data will by pushed every 3 seconds.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">Lİst of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOpenInterestsAsync(Action<OkxOpenInterest> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxOpenInterest>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "open-interest",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of an instrument. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToCandlesticksAsync(Action<OkxCandlestick> onData, string instrumentId, OkxPeriod period, CancellationToken ct = default)
        => await SubscribeToCandlesticksAsync(onData, new string[] { instrumentId }, period, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the candlesticks data of an instrument. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="period"></param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToCandlesticksAsync(Action<OkxCandlestick> onData, IEnumerable<string> instrumentIds, OkxPeriod period, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                if (instrumentIds.Count() == 1) d.Instrument = instrumentIds.FirstOrDefault();
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "candle" + JsonConvert.SerializeObject(period, new PeriodConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the recent trades data. Data will be pushed whenever there is a trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(Action<OkxTrade> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToTradesAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the recent trades data. Data will be pushed whenever there is a trade.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(Action<OkxTrade> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxTrade>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "trades",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the estimated delivery/exercise price of FUTURES contracts and OPTION.
    /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceAsync(Action<OkxEstimatedPrice> onData, OkxInstrumentType instrumentType, string instrumentFamily = null, string instrumentId = null, CancellationToken ct = default)
        => await SubscribeToEstimatedPriceAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the estimated delivery/exercise price of FUTURES contracts and OPTION.
    /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbol List</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceAsync(Action<OkxEstimatedPrice> onData, IEnumerable<OkxSocketSymbolRequest> symbols, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxEstimatedPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "estimated-price",
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
            InstrumentId = symbol.InstrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the mark price. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceAsync(Action<OkxMarkPrice> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToMarkPriceAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the mark price. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">Lİst of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceAsync(Action<OkxMarkPrice> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxMarkPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "mark-price",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of the mark price. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceCandlesticksAsync(Action<OkxCandlestick> onData, string instrumentId, OkxPeriod period, CancellationToken ct = default)
        => await SubscribeToMarkPriceCandlesticksAsync(onData, new string[] { instrumentId }, period, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the candlesticks data of the mark price. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceCandlesticksAsync(Action<OkxCandlestick> onData, IEnumerable<string> instrumentIds, OkxPeriod period, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                if (instrumentIds.Count() == 1) d.Instrument = instrumentIds.FirstOrDefault();
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "mark-price-candle" + JsonConvert.SerializeObject(period, new PeriodConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the maximum buy price and minimum sell price of the instrument. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitAsync(Action<OkxLimitPrice> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToPriceLimitAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the maximum buy price and minimum sell price of the instrument. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitAsync(Action<OkxLimitPrice> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxLimitPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "price-limit",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve order book data.
    /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
    /// books: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed every 100 ms when there is change in order book.
    /// books5: 5 depth levels will be pushed every time.Data will be pushed every 200 ms when there is change in order book.
    /// books50-l2-tbt: 50 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// books-l2-tbt: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="orderBookType">Order Book Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookAsync(Action<OkxOrderBook> onData, string instrumentId, OkxOrderBookType orderBookType, CancellationToken ct = default)
        => await SubscribeToOrderBookAsync(onData, new string[] { instrumentId }, orderBookType, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve order book data.
    /// Use books for 400 depth levels, book5 for 5 depth levels, books50-l2-tbt tick-by-tick 50 depth levels, and books-l2-tbt for tick-by-tick 400 depth levels.
    /// books: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed every 100 ms when there is change in order book.
    /// books5: 5 depth levels will be pushed every time.Data will be pushed every 200 ms when there is change in order book.
    /// books50-l2-tbt: 50 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// books-l2-tbt: 400 depth levels will be pushed in the initial full snapshot. Incremental data will be pushed tick by tick, i.e.whenever there is change in order book.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="orderBookType">Order Book Type</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookAsync(Action<OkxOrderBook> onData, IEnumerable<string> instrumentIds, OkxOrderBookType orderBookType, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxOrderBookUpdate>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                if (instrumentIds.Count() == 1) d.Instrument = instrumentIds.FirstOrDefault();
                d.Action = data.Data.Action;
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = JsonConvert.SerializeObject(orderBookType, new OrderBookTypeConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve detailed pricing information of all OPTION contracts. Data will be pushed at once.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOptionSummaryAsync(Action<OkxOptionSummary> onData, string instrumentFamily, CancellationToken ct = default)
        => await SubscribeToOptionSummaryAsync(onData, new string[] { instrumentFamily }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve detailed pricing information of all OPTION contracts. Data will be pushed at once.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentFamilies">List of Instrument Family</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOptionSummaryAsync(Action<OkxOptionSummary> onData, IEnumerable<string> instrumentFamilies, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxOptionSummary>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentFamily in instrumentFamilies) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "opt-summary",
            InstrumentFamily = instrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve funding rate. Data will be pushed every minute.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRatesAsync(Action<OkxFundingRate> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToFundingRatesAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve funding rate. Data will be pushed every minute.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRatesAsync(Action<OkxFundingRate> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxFundingRate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "funding-rate",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of the index. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexCandlesticksAsync(Action<OkxCandlestick> onData, string instrumentId, OkxPeriod period, CancellationToken ct = default)
        => await SubscribeToIndexCandlesticksAsync(onData, new string[] { instrumentId }, period, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve the candlesticks data of the index. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexCandlesticksAsync(Action<OkxCandlestick> onData, IEnumerable<string> instrumentIds, OkxPeriod period, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                if (instrumentIds.Count() == 1) d.Instrument = instrumentIds.FirstOrDefault();
                onData(d);
            }
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "index-candle" + JsonConvert.SerializeObject(period, new PeriodConverter(false)),
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve index tickers data
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickersAsync(Action<OkxIndexTicker> onData, string instrumentId, CancellationToken ct = default)
        => await SubscribeToIndexTickersAsync(onData, new string[] { instrumentId }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve index tickers data
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentIds">List of Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickersAsync(Action<OkxIndexTicker> onData, IEnumerable<string> instrumentIds, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxIndexTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var instrumentId in instrumentIds) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "index-tickers",
            InstrumentId = instrumentId,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Get the status of system maintenance and push when the system maintenance status changes. First subscription: "Push the latest change data"; every time there is a state change, push the changed content
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToSystemStatusAsync(Action<OkxStatus> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxStatus>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "status",
        });
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    // TODO: Option trades channel
    // TODO: Liquidation orders channel
    #endregion

    #region Private Channels
    /// <summary>
    /// Retrieve account information. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(
        Action<OkxAccountBalance> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxAccountBalance>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "account"
        });
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
        Action<OkxPosition> onData,
        OkxInstrumentType instrumentType,
        string instrumentFamily = null,
        string instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToPositionUpdatesAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
        Action<OkxPosition> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxPosition>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "positions",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve account balance and position information. Data will be pushed when triggered by events such as filled order, funding transfer.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToBalanceAndPositionUpdatesAsync(
        Action<OkxPositionRisk> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxPositionRisk>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "balance_and_position"
        });
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve order information. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        Action<OkxOrder> onData,
        OkxInstrumentType instrumentType,
        string instrumentFamily = null,
        string instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToOrderUpdatesAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve order information. Data will not be pushed when first subscribed. Data will only be pushed when there are order updates.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        Action<OkxOrder> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "orders",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve algo orders (includes trigger order, oco order, conditional order). Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        OkxInstrumentType instrumentType,
        string instrumentFamily = null,
        string instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToAlgoOrderUpdatesAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve algo orders (includes trigger order, oco order, conditional order). Data will not be pushed when first subscribed. Data will only be pushed when there are order updates.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "orders-algo",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve advance algo orders (includes iceberg order and twap order). Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentFamily">Instrument Family</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        OkxInstrumentType instrumentType,
        string instrumentFamily = null,
        string instrumentId = null,
        CancellationToken ct = default)
        => await SubscribeToAdvanceAlgoOrderUpdatesAsync(onData, new OkxSocketSymbolRequest[] { new(instrumentType, instrumentFamily, instrumentId) }, ct).ConfigureAwait(false);

    /// <summary>
    /// Retrieve advance algo orders (including Iceberg order, TWAP order, Trailing order). Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="onData">On Data Handler</param>
    /// <param name="symbols">Symbols to subscribe</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        Action<OkxAlgoOrder> onData,
        IEnumerable<OkxSocketSymbolRequest> symbols,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var arguments = new List<OkxSocketRequestArgument>();
        foreach (var symbol in symbols) arguments.Add(new OkxSocketRequestArgument
        {
            Channel = "algo-advance",
            InstrumentId = symbol.InstrumentId,
            InstrumentType = symbol.InstrumentType,
            InstrumentFamily = symbol.InstrumentFamily,
        });
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, arguments);
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    // TODO: Position risk warning
    // TODO: Account greeks channel
    // TODO: Deposit info channel
    // TODO: Withdrawal info channel
    // TODO: Spot grid algo orders channel
    // TODO: Contract grid algo orders channel
    // TODO: Moon grid algo orders channel
    // TODO: Grid positions channel
    // TODO: Grid sub orders channel
    // TODO: Recurring buy orders channel

    #endregion

}