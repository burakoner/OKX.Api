namespace OKX.Api.Account;

/// <summary>
/// Okx Account Strategy Type
/// </summary>
public enum OkxAccountStrategyType : byte
{
    /// <summary>
    /// General strategy
    /// </summary>
    [Map("0")]
    General = 0,

    /// <summary>
    /// Delta neutral strategy
    /// </summary>
    [Map("1")]
    DeltaNeutral = 1,
}