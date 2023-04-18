namespace OKX.Api.Models.Core;

public class OkxRestApiResponseModel
{
    [JsonProperty("sCode")]
    public string ErrorCode { get; set; }

    [JsonProperty("sMsg")]
    public string ErrorMessage { get; set; }
}