namespace OKX.Api.Clients.RestApi;

/// <summary>
/// OKX Rest Api Broker Client
/// </summary>
public class OKXRestApiBrokerClient
{
    /// <summary>
    /// Earn Client
    /// </summary>
    public OKXRestApiNonDisclosedBrokerClient NonDisclosedBroker { get; }

    /// <summary>
    /// Savings Client
    /// </summary>
    public OKXRestApiFullyDisclosedBrokerClient FullyDisclosedBroker { get; }

    /// <summary>
    /// OKXFinancialProductBaseClient Constructor
    /// </summary>
    public OKXRestApiBrokerClient(OkxRestApiClient root)
    {
        NonDisclosedBroker = new OKXRestApiNonDisclosedBrokerClient(root);
        FullyDisclosedBroker = new OKXRestApiFullyDisclosedBrokerClient(root);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        NonDisclosedBroker.SetApiCredentials(credentials);
        FullyDisclosedBroker.SetApiCredentials(credentials);
    }
}
