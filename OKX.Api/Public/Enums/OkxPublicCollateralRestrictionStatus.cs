namespace OKX.Api.Public;

/// <summary>
/// OKX Collateral Restriction Status
/// Refer to Introduction to the platform collateralized borrowing limit for more details.
/// </summary>
public enum OkxPublicCollateralRestrictionStatus : byte
{
    /// <summary>
    /// The restriction is not enabled.
    /// </summary>
    [Map("0")]
    Disabled = 0,

    /// <summary>
    /// The restriction is not enabled. But the crypto is close to the platform's collateral limit.
    /// </summary>
    [Map("1")]
    CloseToLimit = 1,

    /// <summary>
    /// The restriction is enabled. This crypto can't be used as margin for your new orders. This may result in failed orders. But it will still be included in the account's adjusted equity and doesn't impact margin ratio.
    /// </summary>
    [Map("2")]
    Enabled = 2,
}