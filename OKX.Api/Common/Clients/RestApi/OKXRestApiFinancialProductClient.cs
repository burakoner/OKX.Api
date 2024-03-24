namespace OKX.Api.Common.Clients.RestApi;

/// <summary>
/// OKX Rest Api Financial Product Client
/// </summary>
public class OKXRestApiFinancialProductClient
{
    /// <summary>
    /// Earn Client
    /// </summary>
    public OKXRestApiEarnClient Earn { get; }

    /// <summary>
    /// Savings Client
    /// </summary>
    public OKXRestApiSavingsClient Savings { get; }

    /// <summary>
    /// OKXFinancialProductBaseClient Constructor
    /// </summary>
    public OKXRestApiFinancialProductClient(OkxRestApiClient root)
    {
        Earn = new OKXRestApiEarnClient(root);
        Savings = new OKXRestApiSavingsClient(root);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        Earn.SetApiCredentials(credentials);
        Savings.SetApiCredentials(credentials);
    }
}
