namespace OKX.Api;

/// <summary>
/// OKX Rest API Options
/// </summary>
public class OkxRestApiOptions : RestApiClientOptions
{
    /// <summary>
    /// Receive Window
    /// </summary>
    public TimeSpan ReceiveWindow { get; set; }

    /// <summary>
    /// Auto Timestamp
    /// </summary>
    public bool AutoTimestamp { get; set; }

    /// <summary>
    /// Auto Timestamp Interval
    /// </summary>
    public TimeSpan AutoTimestampInterval { get; set; }

    /// <summary>
    /// Use Demo Trading Service
    /// </summary>
    public bool DemoTradingService
    {
        get
        {
            return _demoTradingService;
        }
        set
        {
            _demoTradingService = value;
            BaseAddress = value ? OkxAddress.Demo.RestApiAddress : OkxAddress.Default.RestApiAddress;
        }
    }
    private bool _demoTradingService = false;

    /// <summary>
    /// Flag for signing public requests with API credentials
    /// </summary>
    public bool SignPublicRequests { get; set; } = false;

    /// <summary>
    /// Constructor
    /// </summary>
    public OkxRestApiOptions() : this(null)
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="credentials">OkxApiCredentials</param>
    public OkxRestApiOptions(OkxApiCredentials? credentials)
    {
        // API Credentials
        ApiCredentials = credentials;

        // Api Addresses
        BaseAddress = OkxAddress.Default.RestApiAddress;

        // Rate Limiters
#pragma warning disable CS0612 // Type or member is obsolete
        RateLimiters =
        [
            new RateLimiter()
            .AddTotalRateLimit(20, TimeSpan.FromSeconds(2), false)
            .AddPartialEndpointLimit("/api/v5/public/delivery-exercise-history", 40, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/estimated-price", 10, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/discount-rate-interest-free-quota", 2, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/time", 10, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/liquidation-orders", 40, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/mark-price", 10, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/position-tiers", 10, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/interest-rate-loan-quota", 2, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/vip-interest-rate-loan-quota", 2, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/insurance-fund", 10, TimeSpan.FromSeconds(2), null, true, true)
            .AddPartialEndpointLimit("/api/v5/public/convert-contract-coin", 10, TimeSpan.FromSeconds(2), null, true, true)
            .AddEndpointLimit("/api/v5/public/event-contract/series", 10, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/public/event-contract/markets", 10, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/public/mm-instrument-types", 5, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/market/books-rpi", 20, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/market/trades", 100, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/account/trade-fee", 5, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/finance/okusd/limits", 2, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/finance/okusd/subscribe", 1, TimeSpan.FromSeconds(2), HttpMethod.Post, true)
            .AddEndpointLimit("/api/v5/finance/okusd/redeem", 1, TimeSpan.FromSeconds(2), HttpMethod.Post, true)
            .AddEndpointLimit("/api/v5/users/glp/today-performance", 5, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/users/glp/historical-performance", 5, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/trade/order-algo", 20, TimeSpan.FromSeconds(2), HttpMethod.Post, true)
            .AddEndpointLimit("/api/v5/trade/amend-algos", 20, TimeSpan.FromSeconds(2), HttpMethod.Post, true)
            .AddEndpointLimit("/api/v5/trade/order-algo", 20, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/trade/orders-algo-pending", 20, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/trade/orders-algo-history", 20, TimeSpan.FromSeconds(2), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/asset/bills", 6, TimeSpan.FromSeconds(1), HttpMethod.Get, true)
            .AddEndpointLimit("/api/v5/asset/bills-history", 1, TimeSpan.FromSeconds(1), HttpMethod.Get, true)
            .AddPartialEndpointLimit("/api/v5/account/bills-history-archive", 1, TimeSpan.FromSeconds(10), HttpMethod.Post, false, true)
            .AddPartialEndpointLimit("/api/v5/account/bills-history-archive", 10, TimeSpan.FromSeconds(2), HttpMethod.Get, false, true)
        ];
#pragma warning restore CS0612 // Type or member is obsolete

        // Receive Window
        ReceiveWindow = TimeSpan.FromSeconds(5);

        // Auto Timestamp
        AutoTimestamp = true;
        AutoTimestampInterval = TimeSpan.FromHours(1);

        // Http Options
        HttpOptions = new HttpOptions
        {
            UserAgent = RestApiConstants.USER_AGENT,
            AcceptMimeType = RestApiConstants.JSON_CONTENT_HEADER,
            RequestTimeout = TimeSpan.FromSeconds(30),
            EncodeQueryString = true,
        };
    }
}
