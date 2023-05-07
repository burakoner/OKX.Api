namespace OKX.Api.Models;

public class OkxRestApiResponseModel
{
    [JsonProperty("sCode")]
    public string ErrorCode { get; set; }

    [JsonProperty("sMsg")]
    public string ErrorMessage { get; set; }
}