﻿using OKX.Api.Common.Models;

namespace OKX.Api.Trade.Models;

public class OkxOrderAmendResponse : OkxRestApiResponseModel
{
    [JsonProperty("ordId")]
    public long? OrderId { get; set; }

    [JsonProperty("clOrdId")]
    public string ClientOrderId { get; set; }

    [JsonProperty("reqId")]
    public string RequestId { get; set; }
}
