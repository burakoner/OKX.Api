namespace OKX.Api.Account;

/// <summary>
/// OKX Spot Derivatives Risk Offset Mode
/// </summary>
public enum OkxAccountRiskOffsetType
{
    /// <summary>
    /// Spot-derivatives (USDT)
    /// </summary>
    [Map("1")]
    UsdtDerivativesUsdt,

    /// <summary>
    /// Spot-derivatives (Crypto)
    /// </summary>
    [Map("2")]
    CryptoDerivatives,
    
    /// <summary>
    /// Derivatives-only
    /// </summary>
    [Map("3")]
    DerivativesOnly,

    /// <summary>
    /// Spot-derivatives (USDC)
    /// </summary>
    [Map("4")]
    UsdtDerivativesUsdc,
}