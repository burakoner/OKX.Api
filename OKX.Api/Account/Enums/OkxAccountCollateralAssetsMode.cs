namespace OKX.Api.Account;

/// <summary>
/// OKX Account Collateral Assets Mode
/// </summary>
public enum OkxAccountCollateralAssetsMode
{
    /// <summary>
    /// All
    /// </summary>
    [Map("all")]
    All = 1,

    /// <summary>
    /// Custom
    /// </summary>
    [Map("custom")]
    Custom = 2,
}