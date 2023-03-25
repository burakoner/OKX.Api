namespace OKX.Api.Models.Core;

public class OkxRestApiResponse<T>
{
    [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
    public int ErrorCode { get; set; }

    [JsonProperty("msg", NullValueHandling = NullValueHandling.Ignore)]
    public string ErrorMessage { get; set; }

    [JsonProperty("data", NullValueHandling = NullValueHandling.Ignore)]
    public T Data { get; set; }
}