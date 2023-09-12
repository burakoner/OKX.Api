using System.Net;
using System.Net.WebSockets;

namespace OKX.Api.Authentication;

public class OkxAuthenticationProvider : AuthenticationProvider
{
    //
    public string GetApiKey() => Credentials.Key!.GetString();
    public string GetPassPhrase() => (Credentials as OkxApiCredentials)?.PassPhrase.GetString();
    public Dictionary<string, string> inputParameters { get; set; }
    public Dictionary<string, string> outputParameters { get; set; }
    //
    private readonly HMACSHA256 encryptor;

    public OkxAuthenticationProvider(OkxApiCredentials credentials) : base(credentials)
    {
        if (credentials == null || credentials.Secret == null)
            throw new ArgumentException("No valid API credentials provided. Key/Secret needed.");

        encryptor = new HMACSHA256(Encoding.ASCII.GetBytes(credentials.Secret.GetString()));
    }

    public override void AuthenticateRestApi(RestApiClient apiClient, Uri uri, HttpMethod method, bool signed, ArraySerialization serialization, SortedDictionary<string, object> query, SortedDictionary<string, object> body, string bodyContent, SortedDictionary<string, string> headers)
    {
        // Options
        var options = (OKXRestApiClientOptions)apiClient.ClientOptions;
        var credentials = (OkxApiCredentials)Credentials;

        // Check Point
        if (!signed && !options.SignPublicRequests)
            return;

        // Check Point
        if (Credentials == null || Credentials.Key == null || Credentials.Secret == null || ((OkxApiCredentials)Credentials).PassPhrase == null)
            throw new ArgumentException("No valid API credentials provided. Key/Secret/PassPhrase needed.");

        // Set Uri
        uri = uri.SetParameters(query, serialization);

        // Signature
        // var time = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
        var time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.sssZ");
        var signtext = time + method.Method.ToUpper() + uri.PathAndQuery.Trim('?') + bodyContent;
        var signature = Base64Encode(encryptor.ComputeHash(Encoding.UTF8.GetBytes(signtext)));

        // Headers
        headers.Add("OK-ACCESS-KEY", Credentials.Key.GetString());
        headers.Add("OK-ACCESS-SIGN", signature);
        headers.Add("OK-ACCESS-TIMESTAMP", time);
        headers.Add("OK-ACCESS-PASSPHRASE", credentials.PassPhrase.GetString());

        // Demo Trading Flag
        if (options.DemoTradingService)
            headers.Add("x-simulated-trading", "1");
    }

    public override void AuthenticateTcpSocketApi()
    {
        var creds = Credentials as OkxApiCredentials;
        var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
        var sortedParameters = new SortedDictionary<string, string>(inputParameters)
        {
                { "apiKey", Credentials.Key!.GetString() },

                { "apiSecret", Credentials.Secret!.GetString() },

                { "passphrase", creds.PassPhrase!.GetString() },

                { "timestamp", timestamp }
            };
        var paramString = string.Join("&", sortedParameters.Select(p => p.Key + "=" + Convert.ToString(p.Value, CultureInfo.InvariantCulture)));

        var sign = SignHMACSHA256(paramString); //HMAC SHA256
        outputParameters = sortedParameters.ToDictionary(p => p.Key, p => p.Value);
        outputParameters.Add("sign", sign);
    }

    public override void AuthenticateWebSocketApi()
    {
        var creds = Credentials as OkxApiCredentials;
        var timestamp = (DateTime.UtcNow.ToUnixTimeMilliSeconds() / 1000.0m).ToString(CultureInfo.InvariantCulture);
        var sortedParameters = new SortedDictionary<string, string>(inputParameters)
        {
                { "apiKey", Credentials.Key!.GetString() },

                { "apiSecret", Credentials.Secret!.GetString() },

                { "passphrase", creds.PassPhrase!.GetString() },

                { "timestamp", timestamp }
            };
        var paramString = string.Join("&", sortedParameters.Select(p => p.Key + "=" + Convert.ToString(p.Value, CultureInfo.InvariantCulture)));

        var sign = SignHMACSHA256(paramString); //HMAC SHA256
        outputParameters = sortedParameters.ToDictionary(p => p.Key, p => p.Value);
        outputParameters.Add("sign", sign);
    }

    public static string Base64Encode(byte[] plainBytes)
    {
        return Convert.ToBase64String(plainBytes);
    }

    public static string Base64Encode(string plainText)
    {
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainTextBytes);
    }

    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        return Encoding.UTF8.GetString(base64EncodedBytes);
    }
}
