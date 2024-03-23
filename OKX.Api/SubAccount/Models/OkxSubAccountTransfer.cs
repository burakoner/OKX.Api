namespace OKX.Api.SubAccount.Models;

public class OkxSubAccountTransfer
{
    [JsonProperty("transId")]
    public long? TransferId { get; set; }
}
