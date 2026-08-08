namespace OKX.Api.Affiliate;

/// <summary>
/// Sub-affiliate list query.
/// </summary>
public record OkxAffiliateSubAffiliateListRequest
{
    /// <summary>One-based page number.</summary>
    public int Page { get; set; } = 1;

    /// <summary>Items per page, from 1 through 100.</summary>
    public int Limit { get; set; } = 100;

    /// <summary>Search by sub-affiliate UID.</summary>
    public string? Keyword { get; set; }

    /// <summary>Commission calculation category.</summary>
    public OkxAffiliateCommissionCategory? CommissionCategory { get; set; }

    /// <summary>Sort field.</summary>
    public OkxAffiliateSortField? OrderBy { get; set; }

    /// <summary>Sort direction.</summary>
    public OkxAffiliateSortDirection? OrderDirection { get; set; }
}
