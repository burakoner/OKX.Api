#pragma warning disable CS1591
namespace OKX.Api.Funding;

/// <summary>
/// Funding withdrawal request
/// </summary>
public record OkxFundingWithdrawalRequest
{
    public string? Currency { get; set; }
    public decimal Amount { get; set; }
    public OkxFundingWithdrawalDestination Destination { get; set; }
    public string? ToAddress { get; set; }
    public OkxFundingWithdrawalAddressType ToAddressType { get; set; }
    public string? Chain { get; set; }
    public string? AreaCode { get; set; }
    public OkxFundingWithdrawalReceiver? ReceiverInfo { get; set; }
    public string? ClientOrderId { get; set; }
}
#pragma warning restore CS1591
