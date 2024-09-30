namespace OKX.Api.Account;

/// <summary>
/// OkxAccountGreeksType
/// </summary>
public class OkxAccountGreeksTypeResponse
{
    /// <summary>
    /// Display type of Greeks.
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(OkxAccountGreeksTypeConverter))]
    public OkxAccountGreeksType GreeksType { get; set; }
}
