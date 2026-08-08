namespace OKX.Api.Affiliate;

/// <summary>
/// Affiliate invitee detail query.
/// </summary>
public record OkxAffiliateInviteeDetailRequest
{
    /// <summary>
    /// Master-account UID of the invitee.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Optional statistics window for period trading volume. Custom windows are not supported by this endpoint.
    /// </summary>
    public OkxAffiliatePeriodType? PeriodType { get; set; }
}
