namespace OKX.Api.Models;

public class OkxSocketMessage
{
    [JsonProperty("op")]
    public string Operation { get; set; } = string.Empty;

    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("args")]
    public IEnumerable<object> Args { get; set; } = Array.Empty<object>();
}


