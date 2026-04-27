#pragma warning disable CS1591
namespace OKX.Api.Account;

/// <summary>
/// Bill history/archive query request
/// </summary>
public record OkxAccountBillQueryRequest
{
    public OkxInstrumentType? InstrumentType { get; set; }
    public string? InstrumentId { get; set; }
    public string? Currency { get; set; }
    public OkxAccountMarginMode? MarginMode { get; set; }
    public OkxContractType? ContractType { get; set; }
    public OkxAccountBillType? BillType { get; set; }
    public OkxAccountBillSubType? BillSubType { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
