namespace OKX.Api.SubAccount;

/// <summary>
/// OKX Sub Account Loan Allocation
/// </summary>
public class OkxSubAccountLoanAllocation
{
    /// <summary>
    /// Sub Account Name
    /// </summary>
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; } = string.Empty;

    /// <summary>
    /// VIP Loan allocation. The unit is percent(%).
    /// Range is [0, 100]. Precision is 0.01% (2 decimal places)
    /// "0" to remove loan allocation for the sub-account
    /// </summary>
    [JsonProperty("loanAlloc")]
    public string LoanAllocation { get; set; } = string.Empty;
}
