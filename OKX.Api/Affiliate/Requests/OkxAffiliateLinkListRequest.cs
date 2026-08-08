namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate link list query.
/// </summary>
public record OkxAffiliateLinkListRequest
{
    /// <summary>One-based page number.</summary>
    public int Page { get; set; } = 1;

    /// <summary>Items per page, from 1 through 100.</summary>
    public int Limit { get; set; } = 100;

    /// <summary>Link type filter.</summary>
    public OkxAffiliateLinkType? LinkType { get; set; }

    /// <summary>Link status filter.</summary>
    public OkxAffiliateLinkStatus? LinkStatus { get; set; }
}
