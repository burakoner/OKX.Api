namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate invitation link type.
/// </summary>
public enum OkxAffiliateLinkType : byte
{
    /// <summary>Regular affiliate invitation link.</summary>
    [Map("standard")]
    Standard = 0,

    /// <summary>Co-inviter shared link.</summary>
    [Map("co_inviter")]
    CoInviter = 1,
}
