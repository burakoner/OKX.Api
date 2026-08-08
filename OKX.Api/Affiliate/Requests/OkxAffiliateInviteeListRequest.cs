namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate invitee list query.
/// </summary>
public record OkxAffiliateInviteeListRequest
{
    /// <summary>One-based page number.</summary>
    public int Page { get; set; } = 1;

    /// <summary>Items per page, from 1 through 100.</summary>
    public int Limit { get; set; } = 100;

    /// <summary>Statistics window.</summary>
    public OkxAffiliatePeriodType? PeriodType { get; set; }

    /// <summary>Inclusive custom-window start, as a Unix timestamp in milliseconds.</summary>
    public long? Begin { get; set; }

    /// <summary>Inclusive custom-window end, as a Unix timestamp in milliseconds.</summary>
    public long? End { get; set; }

    /// <summary>Search by UID or channel name.</summary>
    public string? Keyword { get; set; }

    /// <summary>Commission calculation category.</summary>
    public OkxAffiliateCommissionCategory? CommissionCategory { get; set; }

    /// <summary>Sort field.</summary>
    public OkxAffiliateSortField? OrderBy { get; set; }

    /// <summary>Sort direction.</summary>
    public OkxAffiliateSortDirection? OrderDirection { get; set; }

    /// <summary>KYC status filter.</summary>
    public OkxAffiliateKycStatus? KycStatus { get; set; }

    /// <summary>External UID of a sub-affiliate whose invitees should be returned.</summary>
    public string? SubAffiliateUserId { get; set; }

    /// <summary>Up to 100 external invitee UIDs for exact matching.</summary>
    public IEnumerable<string>? UserIds { get; set; }

    /// <summary>Inclusive lower bound on relationship establishment time, as Unix milliseconds.</summary>
    public long? JoinTimeBegin { get; set; }

    /// <summary>Inclusive upper bound on relationship establishment time, as Unix milliseconds.</summary>
    public long? JoinTimeEnd { get; set; }
}
