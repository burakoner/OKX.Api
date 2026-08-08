namespace OKX.Api.Account;

/// <summary>
/// GLP pool used to determine the current tier.
/// </summary>
public enum OkxAccountGlpQualifyingPool : byte
{
    /// <summary>
    /// Type A pool.
    /// </summary>
    [Map("TYPE_A")]
    TypeA = 1,

    /// <summary>
    /// Adjusted Type B pool.
    /// </summary>
    [Map("TYPE_B_ADJ")]
    TypeBAdjusted = 2,

    /// <summary>
    /// Type A and adjusted Type B pools.
    /// </summary>
    [Map("TYPE_A_AND_B")]
    TypeAAndB = 3,
}
