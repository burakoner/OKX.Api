#pragma warning disable CS1591
namespace OKX.Api.SubAccount;

/// <summary>
/// Transfer between sub-accounts request
/// </summary>
public record OkxSubAccountTransferRequest
{
    public string? Currency { get; set; }
    public decimal Amount { get; set; }
    public OkxAccount FromAccount { get; set; }
    public OkxAccount ToAccount { get; set; }
    public string? FromSubAccountName { get; set; }
    public string? ToSubAccountName { get; set; }
    public bool? LoanTransfer { get; set; }
    public bool? OmitPositionRisk { get; set; }
}
#pragma warning restore CS1591
