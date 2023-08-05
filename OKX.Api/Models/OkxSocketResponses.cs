namespace OKX.Api.Models;

public class OkxSocketResponse
{
    public bool Success
    {
        get
        {
            return
                string.IsNullOrEmpty(ErrorCode)
                || ErrorCode.Trim() == "0";
        }
    }

    [JsonProperty("event")]
    public string Event { get; set; }

    [JsonProperty("code")]
    public string ErrorCode { get; set; }

    [JsonProperty("msg")]
    public string ErrorMessage { get; set; }
}

public class OkxSocketUpdateResponse<T> : OkxSocketResponse
{
    [JsonProperty("arg")]
    public OkxSocketUpdateArgs? Args { get; set; }
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}
public class OkxSocketUpdateArgs
{
    [JsonProperty("channel")]
    public string Channel { get; set; }

    [JsonProperty("instId")]
    public string Instrument { get; set; }
}

public class OkxOrderBookUpdate
{
    [JsonProperty("action")]
    public string Action { get; set; }

    [JsonProperty("data")]
    public IEnumerable<OkxOrderBook> Data { get; set; } = default!;
}
