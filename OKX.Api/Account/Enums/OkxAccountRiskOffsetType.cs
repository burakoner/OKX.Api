namespace OKX.Api.Account.Enums;

/// <summary>
/// OKX Spot Derivatives Risk Offset Mode
/// </summary>
public enum OkxAccountRiskOffsetType
{
    /// <summary>
    /// Spot-derivatives (USDT)
    /// </summary>
    UsdtDerivativesUsdt,

    /// <summary>
    /// Spot-derivatives (Crypto)
    /// </summary>
    CryptoDerivatives,
    
    /// <summary>
    /// Derivatives-only
    /// </summary>
    DerivativesOnly,

    /// <summary>
    /// Spot-derivatives (USDC)
    /// </summary>
    UsdtDerivativesUsdc,
}