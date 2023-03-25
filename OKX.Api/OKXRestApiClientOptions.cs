namespace OKX.Api;

public class OKXRestApiClientOptions : RestApiClientOptions
{
    // Receive Window
    public TimeSpan ReceiveWindow { get; set; }

    // Auto Timestamp
    public bool AutoTimestamp { get; set; }
    public TimeSpan TimestampRecalculationInterval { get; set; }

    // Demo
    public bool DemoTradingService { get; set; } = false;
    public bool SignPublicRequests { get; set; } = false;

    public OKXRestApiClientOptions() : this(null)
    {
    }

    public OKXRestApiClientOptions(ApiCredentials credentials)
    {
        // API Credentials
        ApiCredentials = credentials;

        // Api Addresses
        BaseAddress = OKXApiAddresses.Default.RestApiAddress;

        // Rate Limiters
        RateLimiters = new List<IRateLimiter>
        {
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
        };

        // Receive Window
        ReceiveWindow = TimeSpan.FromSeconds(5);

        // Auto Timestamp
        AutoTimestamp = true;
        TimestampRecalculationInterval = TimeSpan.FromHours(1);

        // Http Options
        HttpOptions = new HttpOptions
        {
            UserAgent = RestApiConstants.USER_AGENT,
            AcceptMimeType = RestApiConstants.JSON_CONTENT_HEADER,
            RequestTimeout = TimeSpan.FromSeconds(30),
            EncodeQueryString = true,
            BodyFormat = RestBodyFormat.Json,
        };

        // Broker Id
        BrokerId = "538a3965e538BCDE";

        // Request Body
        RequestBodyParameterKey = "BODY";
    }
}