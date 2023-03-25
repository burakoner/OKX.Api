namespace OKX.Api;

public sealed class OKXRestApiClient
{
    // Options
    internal OKXRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Master Clients
    public TradeRestApiClient Trade { get; }
    public RubikDataRestApiClient Rubik { get; }
    public FundingRestApiClient Funding { get; }
    public AccountRestApiClient Account { get; }
    public SubAccountRestApiClient SubAccount { get; }
    public MarketDataRestApiClient MarketData { get; }

    public OKXRestApiClient() : this(new OKXRestApiClientOptions())
    {
    }

    public OKXRestApiClient(OKXRestApiClientOptions options)
    {
        ClientOptions = options;

        Trade = new TradeRestApiClient(this);
        Rubik = new RubikDataRestApiClient(this);
        Funding = new FundingRestApiClient(this);
        Account = new AccountRestApiClient(this);
        SubAccount = new SubAccountRestApiClient(this);
        MarketData = new MarketDataRestApiClient(this);
    }

    /// <summary>
    /// Sets the API Credentials
    /// </summary>
    /// <param name="credentials">API Credentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        Trade.SetApiCredentials(credentials);
        Rubik.SetApiCredentials(credentials);
        Funding.SetApiCredentials(credentials);
        Account.SetApiCredentials(credentials);
        SubAccount.SetApiCredentials(credentials);
        MarketData.SetApiCredentials(credentials);
    }

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

    internal Uri GetUri(string endpoint, string param = "")
    {
        var x = endpoint.IndexOf('<');
        var y = endpoint.IndexOf('>');
        if (x > -1 && y > -1) endpoint = endpoint.Replace(endpoint.Substring(x, y - x + 1), param);

        return new Uri($"{ClientOptions.BaseAddress.TrimEnd('/')}/{endpoint}");
    }

    internal Error ParseErrorResponse(JToken error)
    {
        if (!error.HasValues)
            return new ServerError(error.ToString());

        if ((error["msg"] == null && error["code"] == null) || (string.IsNullOrWhiteSpace((string)error["msg"])))
            return new ServerError(error.ToString());

        if (error["msg"] != null && error["code"] == null || (!string.IsNullOrWhiteSpace((string)error["msg"])))
            return new ServerError((string)error["message"]!);

        return new ServerError((int)error["code"], (string)error["message"]);
    }
}
