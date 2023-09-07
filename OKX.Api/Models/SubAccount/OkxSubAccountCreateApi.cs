using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Api.Models.SubAccount;
public class OkxSubAccountCreateApi
{
    [JsonProperty("subAcct")]
    public string SubAccountName { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }
    [JsonProperty("apiKey")]
    public string ApiKey { get; set; }
    [JsonProperty("secretKey")]
    public string ApiSecret { get; set; }
    [JsonProperty("passphrase")]
    public string ApiPassphrase { get; set; }
    [JsonProperty("perm")]
    public string Perm { get; set; }
    [JsonProperty("ip")]
    public string Ip { get; set; }
    [JsonProperty("ts"), JsonConverter(typeof(DateTimeConverter))]
    public DateTime Time { get; set; }
}
