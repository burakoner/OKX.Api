#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Funding deposit history query request
/// </summary>
public record OkxFundingDepositHistoryRequest
{
    public string? Currency { get; set; }
    public string? DepositId { get; set; }
    public long? FromWithdrawalId { get; set; }
    public string? TransactionId { get; set; }
    public OkxFundingDepositType? Type { get; set; }
    public OkxFundingDepositState? State { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
