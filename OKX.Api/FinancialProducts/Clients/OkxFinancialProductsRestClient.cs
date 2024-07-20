namespace OKX.Api.FinancialProducts.Clients;

/// <summary>
/// OKX Rest Api Financial Products Client
/// </summary>
public class OkxFinancialProductsRestClient
{
    /// <summary>
    /// OnChain Earn Client
    /// </summary>
    public OkxOnChainEarnRestClient OnChainEarn { get; }
    
    /// <summary>
    /// Eth Staking Client
    /// </summary>
    public OkxEthStakingRestClient EthStaking { get; }

    /// <summary>
    /// Flexible Simple Earn Client
    /// </summary>
    public OkxFlexibleSimpleEarnRestClient FlexibleSimpleEarn { get; }

    /// <summary>
    /// Fixed Simple Earn Client
    /// </summary>
    public OkxFixedSimpleEarnRestClient FixedSimpleEarn { get; }


    internal OkxFinancialProductsRestClient(OkxRestApiClient root)
    {
        OnChainEarn = new OkxOnChainEarnRestClient(root);
        EthStaking = new OkxEthStakingRestClient(root);
        FlexibleSimpleEarn = new OkxFlexibleSimpleEarnRestClient(root);
        FixedSimpleEarn = new OkxFixedSimpleEarnRestClient(root);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        OnChainEarn.SetApiCredentials(credentials);
        EthStaking.SetApiCredentials(credentials);
        FlexibleSimpleEarn.SetApiCredentials(credentials);
        FixedSimpleEarn.SetApiCredentials(credentials);
    }
}