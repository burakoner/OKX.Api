namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest API SOL Staking Client
/// </summary>
public class OkxFinancialSolStakingRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    private const string v5FinanceStakingDefiSolPurchase = "api/v5/finance/staking-defi/sol/purchase";
    private const string v5FinanceStakingDefiSolRedeem = "api/v5/finance/staking-defi/sol/redeem";
    private const string v5FinanceStakingDefiSolBalance = "api/v5/finance/staking-defi/sol/balance";
    private const string v5FinanceStakingDefiSolPurchaseRedeemHistory = "api/v5/finance/staking-defi/sol/purchase-redeem-history";
    private const string v5FinanceStakingDefiSolApyHistory = "api/v5/finance/staking-defi/sol/apy-history";
}