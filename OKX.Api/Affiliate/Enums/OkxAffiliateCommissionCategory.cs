namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate commission calculation category.
/// </summary>
public enum OkxAffiliateCommissionCategory : byte
{
    /// <summary>Spot trading.</summary>
    [Map("SPOT")]
    Spot = 0,

    /// <summary>Derivative trading.</summary>
    [Map("DERIVATIVE")]
    Derivative = 1,

    /// <summary>BSC trading.</summary>
    [Map("BSC")]
    Bsc = 2,
}
