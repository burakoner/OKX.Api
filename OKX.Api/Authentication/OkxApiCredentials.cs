namespace OKX.Api.Authentication;

public class OkxApiCredentials : ApiCredentials
{
    public SensitiveString PassPhrase { get; }

    public OkxApiCredentials(string apiKey, string apiSecret, string apiPassPhrase) : base(apiKey, apiSecret)
    {
        PassPhrase = apiPassPhrase.ToSensitiveString();
    }

    public override ApiCredentials Copy()
    {
        return new OkxApiCredentials(Key!.GetString(), Secret!.GetString(), PassPhrase!.GetString());
    }
}