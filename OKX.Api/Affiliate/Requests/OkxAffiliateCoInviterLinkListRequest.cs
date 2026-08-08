namespace OKX.Api.Affiliate;

/// <summary>
/// Co-inviter link list query.
/// </summary>
public record OkxAffiliateCoInviterLinkListRequest
{
    /// <summary>One-based page number.</summary>
    public int Page { get; set; } = 1;

    /// <summary>Items per page, from 1 through 100.</summary>
    public int Limit { get; set; } = 100;

    /// <summary>Link status filter.</summary>
    public OkxAffiliateLinkStatus? LinkStatus { get; set; }
}
