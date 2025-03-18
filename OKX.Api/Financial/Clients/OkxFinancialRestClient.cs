namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest Api Financial Products Client
/// </summary>
public class OkxFinancialRestClient
{
    /// <summary>
    /// OnChain Earn Client
    /// </summary>
    public OkxFinancialOnChainEarnRestClient OnChainEarn { get; }

    /// <summary>
    /// Eth Staking Client
    /// </summary>
    public OkxFinancialEthStakingRestClient EthStaking { get; }

    /// <summary>
    /// SOL Staking Client
    /// </summary>
    public OkxFinancialSolStakingRestClient SolStaking { get; }

    /// <summary>
    /// Simple Earn Flexible Client
    /// </summary>
    public OkxFinancialSimpleEarnRestClient SimpleEarn { get; }

    /// <summary>
    /// Flexible Loan Client
    /// </summary>
    public OkxFinancialFlexibleLoanRestClient FlexibleLoan { get; }

    internal OkxFinancialRestClient(OkxRestApiClient root)
    {
        OnChainEarn = new OkxFinancialOnChainEarnRestClient(root);
        EthStaking = new OkxFinancialEthStakingRestClient(root);
        SolStaking = new OkxFinancialSolStakingRestClient(root);
        SimpleEarn = new OkxFinancialSimpleEarnRestClient(root);
        FlexibleLoan = new OkxFinancialFlexibleLoanRestClient(root);
    }

    /// <summary>
    /// Sets API Credentials
    /// </summary>
    /// <param name="credentials">OkxApiCredentials Object</param>
    public void SetApiCredentials(OkxApiCredentials credentials)
    {
        OnChainEarn.SetApiCredentials(credentials);
        EthStaking.SetApiCredentials(credentials);
        SolStaking.SetApiCredentials(credentials);
        SimpleEarn.SetApiCredentials(credentials);
        FlexibleLoan.SetApiCredentials(credentials);
    }
}