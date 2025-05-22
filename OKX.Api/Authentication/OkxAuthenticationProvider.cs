namespace OKX.Api.Authentication;

internal class OkxAuthenticationProvider : AuthenticationProvider
{
    private readonly HMACSHA256 encryptor;

    public OkxAuthenticationProvider(OkxApiCredentials credentials) : base(credentials ?? new OkxApiCredentials("", "", ""))
    {
        //if (credentials is null || credentials.Secret is null || credentials.PassPhrase is null)
        //    throw new ArgumentException("No valid API credentials provided. Key, Secret and PassPhrase needed.");

        encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials?.Secret.GetString() ?? ""));
    }

    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        // Options
        var options = (OkxRestApiOptions)apiClient.ClientOptions;

        // Demo Trading Flag
        if (options.DemoTradingService)
            headers.Add("x-simulated-trading", "1");

        // Check Point
        if (!signed && !options.SignPublicRequests)
            return;

        // Check Point
        if (Credentials is null || Credentials.Key is null || Credentials.Secret is null || ((OkxApiCredentials)Credentials).PassPhrase is null
            || string.IsNullOrEmpty(Credentials.Key.GetString()))
            throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        // Set Uri
        uri = uri.SetParameters(query, serialization);

        // Signature
        // var time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var time = GetTimestamp(apiClient).ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var signtext = time + method.Method.ToUpper() + uri.PathAndQuery.Trim('?') + bodyContent;
        var signature = Base64Encode(encryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));

        // Headers
        headers.Add("OK-ACCESS-KEY", Credentials.Key.GetString());
        headers.Add("OK-ACCESS-SIGN", signature);
        headers.Add("OK-ACCESS-TIMESTAMP", time);
        headers.Add("OK-ACCESS-PASSPHRASE", ((OkxApiCredentials)Credentials).PassPhrase.GetString());
    }

    public static string Base64Encode(byte[] plainBytes) => Convert.ToBase64String(plainBytes);
    public static string Base64Encode(string plainText) => Convert.ToBase64String(Encoding.UTF8.GetBytes(plainText));
    public static string Base64Decode(string base64EncodedData) => Encoding.UTF8.GetString(Convert.FromBase64String(base64EncodedData));
}
