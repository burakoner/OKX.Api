namespace OKX.Api.Financial;

/// <summary>
/// OKUSD Redemption Type
/// </summary>
public enum OkxFinancialOkusdRedemptionType : byte
{
    /// <summary>
    /// Fast redemption with real-time settlement
    /// </summary>
    [Map("1")]
    Fast = 1,

    /// <summary>
    /// Standard redemption with D+5 or D+6 calendar-day settlement
    /// </summary>
    [Map("2")]
    Standard = 2,
}