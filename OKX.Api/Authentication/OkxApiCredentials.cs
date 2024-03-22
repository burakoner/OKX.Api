namespace OKX.Api.Authentication;

/// <summary>
/// OKX API Credentials
/// </summary>
/// <param name="apiKey">API Key</param>
/// <param name="apiSecret">API Secret</param>
/// <param name="apiPassPhrase">Pass Phrase</param>
public class OkxApiCredentials(string apiKey, string apiSecret, string apiPassPhrase) : ApiCredentials(apiKey, apiSecret)
{
    /// <summary>
    /// Pass Phrase
    /// </summary>
    public SensitiveString PassPhrase { get; } = apiPassPhrase.ToSensitiveString();

    /// <summary>
    /// Copies and creates a new OkxApiCredentials instance
    /// </summary>
    /// <returns></returns>
    public override ApiCredentials Copy()
    {
        return new OkxApiCredentials(Key!.GetString(), Secret!.GetString(), PassPhrase!.GetString());
    }
}