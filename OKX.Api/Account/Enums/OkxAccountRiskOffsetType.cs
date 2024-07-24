namespace OKX.Api.Account.Enums;

/// <summary>
/// OKX Spot Derivatives Risk Offset Mode
/// </summary>
public enum OkxAccountRiskOffsetType
{
    /// <summary>
    /// Spot-derivatives (USDT)
    /// </summary>
    UsdtDerivatives,

    /// <summary>
    /// Spot-derivatives (crypto)
    /// </summary>
    CryptoDerivatives,
    
    /// <summary>
    /// Derivatives-only
    /// </summary>
    DerivativesOnly,
}