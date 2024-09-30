namespace OKX.Api.CopyTrade;

/// <summary>
/// OKX SpotCopyTradingRole
/// </summary>
public enum OkxCopyTradingRole
{
    /// <summary>
    /// General user
    /// </summary>
    [Map("0")]
    GeneralUser,

    /// <summary>
    /// Leading trader
    /// </summary>
    [Map("1")]
    LeadingTrader,

    /// <summary>
    /// Copy trader
    /// </summary>
    [Map("2")]
    CopyTrader,
}