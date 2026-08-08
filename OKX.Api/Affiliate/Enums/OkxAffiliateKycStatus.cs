namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate invitee KYC status.
/// </summary>
public enum OkxAffiliateKycStatus : byte
{
    /// <summary>KYC2 has not been completed.</summary>
    [Map("unverified")]
    Unverified = 0,

    /// <summary>At least KYC2 has been completed.</summary>
    [Map("verified")]
    Verified = 1,
}
