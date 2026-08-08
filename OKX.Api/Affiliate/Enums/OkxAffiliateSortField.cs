namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate list sort field.
/// </summary>
public enum OkxAffiliateSortField : byte
{
    /// <summary>Creation or relationship time.</summary>
    [Map("cTime")]
    CreationTime = 0,

    /// <summary>Deposit amount.</summary>
    [Map("depAmt")]
    DepositAmount = 1,

    /// <summary>Trading volume.</summary>
    [Map("vol")]
    Volume = 2,

    /// <summary>Trading fee.</summary>
    [Map("fee")]
    Fee = 3,

    /// <summary>Commission or rebate.</summary>
    [Map("rebate")]
    Rebate = 4,
}
