using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Api.Models.SubAccount;
public class OkxSubCreateAccount
{

    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }
    [JsonProperty("acctLv")]
    public string SubAccLevel { get; set; }
    [JsonProperty("uid")]
    public string Uid { get; set; }
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }

}
