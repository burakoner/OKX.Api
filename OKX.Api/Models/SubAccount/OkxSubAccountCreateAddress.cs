using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Api.Models.SubAccount;
public class OkxSubAccountCreateAddress
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }
    [JsonProperty("addr")]
    public string Address { get; set; }
    [JsonProperty("chain")]
    public string Chain { get; set; }
    [JsonProperty("pmtId")]
    public string PmtId { get; set; }
    [JsonProperty("tag")]
    public string Tag { get; set; }
    [JsonProperty("memo")]
    public string Memo { get; set; }
}
