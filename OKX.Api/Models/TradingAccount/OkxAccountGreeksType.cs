namespace OKX.Api.Models.TradingAccount;

/// <summary>
/// OkxAccountGreeksType
/// </summary>
public class OkxAccountGreeksType
{
    /// <summary>
    /// Display type of Greeks.
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OkxGreeksType GreeksType { get; set; }
}
