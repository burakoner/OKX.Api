namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate invitation link status.
/// </summary>
public enum OkxAffiliateLinkStatus : byte
{
    /// <summary>Normal link.</summary>
    [Map("normal")]
    Normal = 0,

    /// <summary>Abnormal link.</summary>
    [Map("abnormal")]
    Abnormal = 1,
}
