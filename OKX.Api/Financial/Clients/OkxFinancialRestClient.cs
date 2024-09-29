namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest Api Financial Products Client
/// </summary>
public class OkxFinancialRestClient
{
    /// <summary>
    /// Eth Staking Client
    /// </summary>
    public OkxFinancialEthStakingRestClient EthStaking { get; }

    /// <summary>
    /// OnChain Earn Client
    /// </summary>
    public OkxFinancialOnChainEarnRestClient OnChainEarn { get; }

    /// <summary>
    /// Fixed Simple Earn Client
    /// </summary>
    public OkxFinancialFixedSimpleEarnRestClient FixedSimpleEarn { get; }

    /// <summary>
    /// Flexible Simple Earn Client
    /// </summary>
    public OkxFinancialFlexibleSimpleEarnRestClient FlexibleSimpleEarn { get; }


    internal OkxFinancialRestClient(OkxRestApiClient root)
    {
        EthStaking = new OkxFinancialEthStakingRestClient(root);
        OnChainEarn = new OkxFinancialOnChainEarnRestClient(root);
        FixedSimpleEarn = new OkxFinancialFixedSimpleEarnRestClient(root);
        FlexibleSimpleEarn = new OkxFinancialFlexibleSimpleEarnRestClient(root);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        EthStaking.SetApiCredentials(credentials);
        OnChainEarn.SetApiCredentials(credentials);
        FixedSimpleEarn.SetApiCredentials(credentials);
        FlexibleSimpleEarn.SetApiCredentials(credentials);
    }
}