using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Api.Models.SubAccount;
public class OkxSubAccountDetailListModel
{
    [JsonProperty("details")]
    public List<OkxSubAccountBroker> Details { get; set; }
    [JsonProperty("page")]
    public int Page { get; set; }
    [JsonProperty("totalPage")]
    public int TotalPage { get; set; }
}
