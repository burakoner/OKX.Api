namespace OKX.Api.Account;

/// <summary>
/// OkxAccountGreeksType
/// </summary>
public class OkxAccountGreeksTypeResponse
{
    /// <summary>
    /// Display type of Greeks.
    /// </summary>
    [JsonProperty("greeksType")]
    public OkxAccountGreeksType GreeksType { get; set; }
}
