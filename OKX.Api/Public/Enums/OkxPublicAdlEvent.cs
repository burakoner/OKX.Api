namespace OKX.Api.Public;

/// <summary>
/// OKX ADL Event
/// </summary>
public enum OkxPublicAdlEvent : byte
{
    /// <summary>
    /// ADL begins due to high security fund decline rate
    /// </summary>
    [Map("rate_adl_start")]
    RateAdlStartEvent = 1,

    /// <summary>
    /// ADL begins due to security fund balance falling
    /// </summary>
    [Map("bal_adl_start")]
    BalanceAdlStartEvent = 2,

    /// <summary>
    /// ADL begins due to the volume of liquidation orders falls to a certain level (only applicable to premarket symbols)
    /// </summary>
    [Map("pos_adl_start")]
    PositionAdlStartEvent = 3,

    /// <summary>
    /// ADL ends
    /// </summary>
    [Map("adl_end")]
    AdlEndEvent = 4,
}