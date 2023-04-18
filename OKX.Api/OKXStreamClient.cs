namespace OKX.Api;

public class OKXStreamClient : StreamApiClient
{
    internal bool IsAuthendicated { get; private set; }

    public OKXStreamClient() : this(new OKXStreamClientOptions())
    {
    }

    public OKXStreamClient(OKXStreamClientOptions options) : base("OKX Stream", options)
    {
        RateLimitPerConnectionPerSecond = 4;
        this.IgnoreHandlingList = new List<string> { "pong" };

        SetDataInterpreter(DecompressData, null);
        SendPeriodic("Ping", TimeSpan.FromSeconds(5), con => "ping");
    }

    #region Overrided Methods
    protected override AuthenticationProvider CreateAuthenticationProvider(ApiCredentials credentials)
        => new OkxAuthenticationProvider((OkxApiCredentials)credentials);

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
                    reqArg.Underlying == resArg.Underlying &&
                    reqArg.InstrumentId == resArg.InstrumentId &&
                    reqArg.InstrumentType == resArg.InstrumentType)
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected override bool MessageMatchesHandler(StreamConnection connection, JToken message, string identifier)
    {
        return true;
    }

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
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToInstrumentsAsync(OkxInstrumentType instrumentType, Action<OkxInstrument> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxInstrument>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "instruments", instrumentType);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the last traded price, bid price, ask price and 24-hour trading volume of instruments. Data will be pushed every 100 ms.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTickersAsync(string intrumentId, Action<OkxTicker> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "tickers", intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the open interest. Data will by pushed every 3 seconds.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="onData">On Data Handler</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToInterestsAsync(string intrumentId, Action<OkxOpenInterest> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxOpenInterest>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "open-interest", intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of an instrument. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="period"></param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToCandlesticksAsync(string intrumentId, OkxPeriod period, Action<OkxCandlestick> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = intrumentId;
                onData(d);
            }
        });

        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "candle" + jc, intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the recent trades data. Data will be pushed whenever there is a trade.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToTradesAsync(string intrumentId, Action<OkxTrade> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxTrade>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "trades", intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the estimated delivery/exercise price of FUTURES contracts and OPTION.
    /// Only the estimated delivery/exercise price will be pushed an hour before delivery/exercise, and will be pushed if there is any price change.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToEstimatedPriceAsync(OkxInstrumentType instrumentType, string underlying, Action<OkxEstimatedPrice> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxEstimatedPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "estimated-price", instrumentType, underlying);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the mark price. Data will be pushed every 200 ms when the mark price changes, and will be pushed every 10 seconds when the mark price does not change.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceAsync(string intrumentId, Action<OkxMarkPrice> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxMarkPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "mark-price", intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of the mark price. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceCandlesticksAsync(string intrumentId, OkxPeriod period, Action<OkxCandlestick> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = intrumentId;
                onData(d);
            }
        });

        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "mark-price-candle" + jc, intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the maximum buy price and minimum sell price of the instrument. Data will be pushed every 5 seconds when there are changes in limits, and will not be pushed when there is no changes on limit.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPriceLimitAsync(string intrumentId, Action<OkxLimitPrice> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxLimitPrice>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "price-limit", intrumentId);
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
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="orderBookType">Order Book Type</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderBookAsync(string intrumentId, OkxOrderBookType orderBookType, Action<OkxOrderBook> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxOrderBookUpdate>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = intrumentId;
                d.Action = data.Data.Action;
                onData(d);
            }
        });

        var jc = JsonConvert.SerializeObject(orderBookType, new OrderBookTypeConverter(false));
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, jc, intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve detailed pricing information of all OPTION contracts. Data will be pushed at once.
    /// </summary>
    /// <param name="underlying">Underlying</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOptionSummaryAsync(string underlying, Action<OkxOptionSummary> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxOptionSummary>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "opt-summary", string.Empty, underlying);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve funding rate. Data will be pushed every minute.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToFundingRatesAsync(string intrumentId, Action<OkxFundingRate> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxFundingRate>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "funding-rate", intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve the candlesticks data of the index. Data will be pushed every 500 ms.
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="period">Period</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexCandlesticksAsync(string intrumentId, OkxPeriod period, Action<OkxCandlestick> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxCandlestick>>>>(data =>
        {
            foreach (var d in data.Data.Data)
            {
                d.Instrument = intrumentId;
                onData(d);
            }
        });

        var jc = JsonConvert.SerializeObject(period, new PeriodConverter(false));
        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "index-candle" + jc, intrumentId);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve index tickers data
    /// </summary>
    /// <param name="intrumentId">Instrument ID</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToIndexTickersAsync(string intrumentId, Action<OkxIndexTicker> onData, CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxIndexTicker>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "index-tickers", intrumentId);
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

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "status", string.Empty, string.Empty);
        return await SubscribeAsync(request, null, false, internalHandler, ct).ConfigureAwait(false);
    }

    // TODO: Public structure block trades channel
    // TODO: Block tickers channel
    #endregion

    #region Private Signed Feeds
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

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "account");
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve position information. Initial snapshot will be pushed according to subscription granularity. Data will be pushed when triggered by events such as placing/canceling order, and will also be pushed in regular interval according to subscription granularity.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToPositionUpdatesAsync(
        OkxInstrumentType instrumentType,
        string instrumentId,
        string underlying,
        Action<OkxPosition> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxPosition>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "positions",
            InstrumentId = instrumentId,
            InstrumentType = instrumentType,
            Underlying = underlying,
        });
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

        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, "balance_and_position");
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve order information. Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(
        OkxInstrumentType instrumentType,
        string instrumentId,
        string underlying,
        Action<OkxOrder> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });


        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "orders",
            InstrumentId = instrumentId,
            InstrumentType = instrumentType,
            Underlying = underlying,
        });
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve algo orders (includes trigger order, oco order, conditional order). Data will not be pushed when first subscribed. Data will only be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAlgoOrderUpdatesAsync(
        OkxInstrumentType instrumentType,
        string instrumentId,
        string underlying,
        Action<OkxAlgoOrder> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });


        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "orders-algo",
            InstrumentId = instrumentId,
            InstrumentType = instrumentType,
            Underlying = underlying,
        });
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieve advance algo orders (includes iceberg order and twap order). Data will be pushed when first subscribed. Data will be pushed when triggered by events such as placing/canceling order.
    /// </summary>
    /// <param name="instrumentType">Instrument Type</param>
    /// <param name="instrumentId">Instrument ID</param>
    /// <param name="underlying">Underlying</param>
    /// <param name="onData">On Data Handler</param>
    /// <param name="ct">Cancellation Token</param>
    /// <returns></returns>
    public virtual async Task<CallResult<UpdateSubscription>> SubscribeToAdvanceAlgoOrderUpdatesAsync(
        OkxInstrumentType instrumentType,
        string instrumentId,
        string underlying,
        Action<OkxAlgoOrder> onData,
        CancellationToken ct = default)
    {
        var internalHandler = new Action<StreamDataEvent<OkxSocketUpdateResponse<IEnumerable<OkxAlgoOrder>>>>(data =>
        {
            foreach (var d in data.Data.Data)
                onData(d);
        });


        var request = new OkxSocketRequest(OkxSocketOperation.Subscribe, new OkxSocketRequestArgument
        {
            Channel = "algo-advance",
            InstrumentId = instrumentId,
            InstrumentType = instrumentType,
            Underlying = underlying,
        });
        return await SubscribeAsync(request, null, true, internalHandler, ct).ConfigureAwait(false);
    }

    // TODO: Position risk warning
    // TODO: Account greeks channel
    // TODO: Rfqs channel
    // TODO: Quotes channel
    // TODO: Structure block trades channel
    // TODO: Spot grid algo orders channel
    // TODO: Contract grid algo orders channel
    // TODO: Grid positions channel
    // TODO: Grid sub orders channel

    #endregion
}