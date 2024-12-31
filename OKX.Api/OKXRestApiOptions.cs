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
        ];

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
