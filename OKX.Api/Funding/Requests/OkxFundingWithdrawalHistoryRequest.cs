#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Funding withdrawal history query request
/// </summary>
public record OkxFundingWithdrawalHistoryRequest
{
    public string? Currency { get; set; }
    public long? WithdrawalId { get; set; }
    public string? ClientOrderId { get; set; }
    public string? TransactionId { get; set; }
    public OkxFundingWithdrawalType? Type { get; set; }
    public OkxFundingWithdrawalState? State { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
