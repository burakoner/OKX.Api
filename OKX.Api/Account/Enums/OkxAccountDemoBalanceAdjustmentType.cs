namespace OKX.Api.Account;

/// <summary>
/// Demo account balance adjustment direction.
/// </summary>
public enum OkxAccountDemoBalanceAdjustmentType : byte
{
    /// <summary>
    /// Add to the demo account balance.
    /// </summary>
    [Map("increase")]
    Increase = 0,

    /// <summary>
    /// Deduct from the demo account balance.
    /// </summary>
    [Map("reduce")]
    Reduce = 1,
}
