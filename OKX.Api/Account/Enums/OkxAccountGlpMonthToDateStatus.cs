namespace OKX.Api.Account;

/// <summary>
/// GLP month-to-date qualification status.
/// </summary>
public enum OkxAccountGlpMonthToDateStatus : byte
{
    /// <summary>
    /// The enrolled tier is currently qualified.
    /// </summary>
    [Map("QUALIFIED")]
    Qualified = 1,

    /// <summary>
    /// The current performance qualifies for an upgrade.
    /// </summary>
    [Map("UPGRADE")]
    Upgrade = 2,

    /// <summary>
    /// The current performance results in a downgrade.
    /// </summary>
    [Map("DOWNGRADE")]
    Downgrade = 3,
}
