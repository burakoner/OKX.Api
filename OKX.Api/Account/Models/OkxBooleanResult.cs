namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Boolean Result
/// </summary>
public class OkxBooleanResult
{
    [JsonProperty("result")]
    public bool Result { get; set; }
}