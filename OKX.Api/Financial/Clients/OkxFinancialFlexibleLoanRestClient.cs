namespace OKX.Api.Financial;

/// <summary>
/// OKX Rest API Flexible Loan Client
/// </summary>
public class OkxFinancialFlexibleLoanRestClient(OkxRestApiClient root) : OkxBaseRestClient(root)
{
    // Endpoints
    // api/v5/finance/flexible-loan/borrow-currencies
    // api/v5/finance/flexible-loan/collateral-assets
    // api/v5/finance/flexible-loan/max-loan
    // api/v5/finance/flexible-loan/max-collateral-redeem-amount
    // api/v5/finance/flexible-loan/adjust-collateral
    // api/v5/finance/flexible-loan/loan-info
    // api/v5/finance/flexible-loan/loan-history
    // api/v5/finance/flexible-loan/interest-accrued
}