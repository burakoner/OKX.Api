namespace OKX.Api.Financial;

/// <summary>
/// Flexible loan collateral adjustment type
/// </summary>
public enum OkxFinancialFlexibleLoanAdjustType : byte
{
    /// <summary>
    /// Add collateral
    /// </summary>
    [Map("add")]
    Add = 1,

    /// <summary>
    /// Reduce collateral
    /// </summary>
    [Map("reduce")]
    Reduce = 2,
}
