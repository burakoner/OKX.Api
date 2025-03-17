namespace OKX.Api.Broker;

/// <summary>
/// OKX Rest API Broker Client
/// </summary>
public class OkxBrokerRestClient
{
    /// <summary>
    /// Fully Disclosed Broker Client
    /// </summary>
    public OkxFDBrokerRestClient FD { get; }

    /// <summary>
    /// Eth Staking Client
    /// </summary>
    public OkxDMABrokerRestClient DMA { get; }

    internal OkxBrokerRestClient(OkxRestApiClient root)
    {
        FD = new OkxFDBrokerRestClient(root);
        DMA = new OkxDMABrokerRestClient(root);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        FD.SetApiCredentials(credentials);
        DMA.SetApiCredentials(credentials);
    }
}