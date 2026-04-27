#pragma warning disable CS1591
namespace OKX.Api.Trade;

/// <summary>
/// Trade fills query request
/// </summary>
public record OkxTradeTransactionQueryRequest
{
    public OkxInstrumentType? InstrumentType { get; set; }
    public string? InstrumentId { get; set; }
    public string? InstrumentFamily { get; set; }
    public long? OrderId { get; set; }
    public OkxAccountBillSubType? TransactionType { get; set; }
    public long? After { get; set; }
    public long? Before { get; set; }
    public long? Begin { get; set; }
    public long? End { get; set; }
    public int Limit { get; set; } = 100;
}
#pragma warning restore CS1591
