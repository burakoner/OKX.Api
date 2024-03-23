using OKX.Api.SubAccount.Converters;
using OKX.Api.SubAccount.Enums;

namespace OKX.Api.SubAccount.Models;

public class OkxSubAccountBill
{
    [JsonProperty("billId")]
    public long BillId { get; set; }
    
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    
    [JsonProperty("amt")]
    public decimal Amount { get; set; }
    
    [JsonProperty("type"), JsonConverter(typeof(OkxSubAccountTransferTypeConverter))]
    public OkxSubAccountTransferType Type { get; set; }
    
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("ts")]
    public long Timestamp { get; set; }

    [JsonIgnore]
    public DateTime Time { get { return Timestamp.ConvertFromMilliseconds(); } }
}

public class OkxManagedSubAccountBill : OkxSubAccountBill
{
    [JsonProperty("subUid")]
    public long SubAccountId { get; set; }
}