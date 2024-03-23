using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Models.FundingAccount;

public class OkxDepositAddress
{
    [JsonProperty("addr")]
    public string Address { get; set; }

    [JsonProperty("tag")]
    public string Tag { get; set; }

    [JsonProperty("memo")]
    public string Memo { get; set; }

    [JsonProperty("pmtId")]
    public string DepositPaymentId { get; set; }

    [JsonProperty("addrEx")]
    public string DepositAddressAttachment { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("to"), JsonConverter(typeof(OkxAccountConverter))]
    public OkxAccount? Account { get; set; }

    [JsonProperty("selected")]
    public bool Selected { get; set; }

    [JsonProperty("ctAddr")]
    public string ContractAddr { get; set; }

}