namespace OKX.Api.Authentication;

/// <summary>
/// OKX API Credentials
/// </summary>
public class OkxApiCredentials : ApiCredentials
{
    /// <summary>
    /// Pass Phrase
    /// </summary>
    public SensitiveString PassPhrase { get; }

    /// <summary>
    /// OkxApiCredentials Constructor
    /// </summary>
    /// <param name="apiKey">API Key</param>
    /// <param name="apiSecret">API Secret</param>
    /// <param name="apiPassPhrase">Pass Phrase</param>
    public OkxApiCredentials(string apiKey, string apiSecret, string apiPassPhrase) : base(apiKey, apiSecret)
    {
        PassPhrase = apiPassPhrase.ToSensitiveString();
    }

    /// <summary>
    /// Copies and creates a new OkxApiCredentials instance
    /// </summary>
    /// <returns></returns>
    public override ApiCredentials Copy()
    {
        return new OkxApiCredentials(Key!.GetString(), Secret!.GetString(), PassPhrase!.GetString());
    }
}