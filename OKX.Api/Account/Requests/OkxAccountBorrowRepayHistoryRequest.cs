#pragma warning disable CS1591
namespace OKX.Api.Account;

/// <summary>
/// Borrow/repay history query request
/// </summary>
public record OkxAccountBorrowRepayHistoryRequest
{
    public string? Currency { get; set; }
    public string? Type { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
