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
    UsdtDerivativesUsdt = 1,

    /// <summary>
    /// Spot-derivatives (Crypto)
    /// </summary>
    [Map("2")]
    CryptoDerivatives = 2,
    
    /// <summary>
    /// Derivatives-only
    /// </summary>
    [Map("3")]
    DerivativesOnly = 3,

    /// <summary>
    /// Spot-derivatives (USDC)
    /// </summary>
    [Map("4")]
    UsdtDerivativesUsdc = 4,
}