namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX SpotCopyTradingRole
/// </summary>
public enum OkxCopyTradingRole : byte
{
    /// <summary>
    /// General user
    /// </summary>
    [Map("0")]
    GeneralUser = 0,

    /// <summary>
    /// Leading trader
    /// </summary>
    [Map("1")]
    LeadingTrader = 1,

    /// <summary>
    /// Copy trader
    /// </summary>
    [Map("2")]
    CopyTrader = 2,
}