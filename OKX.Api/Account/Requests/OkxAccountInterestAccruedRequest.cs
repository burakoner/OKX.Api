#pragma warning disable CS1591
namespace OKX.Api.Account;

/// <summary>
/// Interest accrued query request
/// </summary>
public record OkxAccountInterestAccruedRequest
{
    public OkxAccountLoanType? Type { get; set; }
    public string? Currency { get; set; }
    public string? InstrumentId { get; set; }
    public OkxAccountMarginMode? MarginMode { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
