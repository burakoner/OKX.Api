namespace OKX.Api.Account;

/// <summary>
/// OKX Account Discount Type
/// </summary>
public enum OkxAccountDiscountType
{
    /// <summary>
    /// Original discount rate rules, the default value
    /// </summary>
    [Map("0")]
    OriginalDiscountRateRules = 0,

    /// <summary>
    /// New discount rules
    /// </summary>
    [Map("1")]
    NewDiscountRules = 1,
}