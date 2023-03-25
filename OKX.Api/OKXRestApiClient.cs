namespace OKX.Api;

public sealed class OKXRestApiClient
{
    // Options
    internal OKXRestApiClientOptions ClientOptions { get; }
    internal CultureInfo CI { get; } = CultureInfo.InvariantCulture;

    // Master Clients
    public OKXTradeRestApiClient Trade { get; }
    public OKXRubikDataRestApiClient Rubik { get; }
    public OKXFundingRestApiClient Funding { get; }
    public OKXWalletRestApiClient Wallet { get; }
    public OKXSubAccountRestApiClient SubAccount { get; }
    public OKXMarketDataRestApiClient MarketData { get; }

    public OKXRestApiClient() : this(new OKXRestApiClientOptions())
    {
    }

    public OKXRestApiClient(OKXRestApiClientOptions options)
    {
        ClientOptions = options;

        Trade = new OKXTradeRestApiClient(this);
        Rubik = new OKXRubikDataRestApiClient(this);
        Funding = new OKXFundingRestApiClient(this);
        Wallet = new OKXWalletRestApiClient(this);
        SubAccount = new OKXSubAccountRestApiClient(this);
        MarketData = new OKXMarketDataRestApiClient(this);
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
        Wallet.SetApiCredentials(credentials);
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
