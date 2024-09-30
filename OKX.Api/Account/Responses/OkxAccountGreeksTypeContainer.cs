namespace OKX.Api.Account;

/// <summary>
/// OkxAccountGreeksType
/// </summary>
public record OkxAccountGreeksTypeContainer
{
    /// <summary>
    /// Display type of Greeks.
    /// </summary>
    [JsonProperty("greeksType")]
    public OkxAccountGreeksType GreeksType { get; set; }
}
