namespace OKX.Api.Account.Models;

/// <summary>
/// Okx Boolean Result
/// </summary>
public class OkxBooleanResult
{
    /// <summary>
    /// Result
    /// </summary>
    [JsonProperty("result")]
    public bool Result { get; set; }
}