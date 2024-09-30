namespace OKX.Api.Common;

/// <summary>
/// OKX Kyc Level
/// </summary>
public enum OkxKycLevel
{
    /// <summary>
    /// NoVerification
    /// </summary>
    [Map("0")]
    NoVerification,

    /// <summary>
    /// Level1
    /// </summary>
    [Map("1")]
    Level1,

    /// <summary>
    /// Level2
    /// </summary>
    [Map("2")]
    Level2,

    /// <summary>
    /// Level3
    /// </summary>
    [Map("3")]
    Level3
}