namespace OKX.Api.Broker.Clients;

/// <summary>
/// OKX Rest API Broker Client
/// </summary>
public class OkxBrokerRestClient
{
    /// <summary>
    /// Fully Disclosed Broker Client
    /// </summary>
    public OkxFullyDisclosedBrokerRestClient FullyDisclosed { get; }

    /// <summary>
    /// Eth Staking Client
    /// </summary>
    public OkxNonDisclosedBrokerRestClient NonDisclosed { get; }

    internal OkxBrokerRestClient(OkxRestApiClient root)
    {
        FullyDisclosed = new OkxFullyDisclosedBrokerRestClient(root);
        NonDisclosed = new OkxNonDisclosedBrokerRestClient(root);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        FullyDisclosed.SetApiCredentials(credentials);
        NonDisclosed.SetApiCredentials(credentials);
    }
}