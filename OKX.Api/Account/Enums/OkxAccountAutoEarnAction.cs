namespace OKX.Api.Account;

/// <summary>
/// OKX Account Auto Earn Action
/// </summary>
public enum OkxAccountAutoEarnAction : byte
{
    /// <summary>
    /// turn off auto earn
    /// </summary>
    [Map("turn_off")]
    TurnOff = 0,

    /// <summary>
    /// turn on auto earn
    /// </summary>
    [Map("turn_on")]
    TurnOn = 1,

    /// <summary>
    /// Amend
    /// </summary>
    [Obsolete]
    [Map("amend")]
    Amend = 2,
}