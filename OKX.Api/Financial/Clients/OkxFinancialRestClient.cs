using OKX.Api.Financial.EthStaking.Clients;
using OKX.Api.Financial.FixedSimpleEarn.Clients;
using OKX.Api.Financial.FlexibleSimpleEarn.Clients;
using OKX.Api.Financial.OnChainEarn.Clients;

namespace OKX.Api.Financial.Clients;

/// <summary>
/// OKX Rest Api Financial Products Client
/// </summary>
public class OkxFinancialRestClient
{
    /// <summary>
    /// Eth Staking Client
    /// </summary>
    public OkxEthStakingRestClient EthStaking { get; }

    /// <summary>
    /// OnChain Earn Client
    /// </summary>
    public OkxOnChainEarnRestClient OnChainEarn { get; }

    /// <summary>
    /// Fixed Simple Earn Client
    /// </summary>
    public OkxFixedSimpleEarnRestClient FixedSimpleEarn { get; }

    /// <summary>
    /// Flexible Simple Earn Client
    /// </summary>
    public OkxFlexibleSimpleEarnRestClient FlexibleSimpleEarn { get; }


    internal OkxFinancialRestClient(OkxRestApiClient root)
    {
        EthStaking = new OkxEthStakingRestClient(root);
        OnChainEarn = new OkxOnChainEarnRestClient(root);
        FixedSimpleEarn = new OkxFixedSimpleEarnRestClient(root);
        FlexibleSimpleEarn = new OkxFlexibleSimpleEarnRestClient(root);
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