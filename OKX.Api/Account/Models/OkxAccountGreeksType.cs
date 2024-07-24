using OKX.Api.Account.Converters;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxAccountGreeksType
/// </summary>
public class OkxAccountGreeksType
{
    /// <summary>
    /// Display type of Greeks.
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(OkxAccountGreeksTypeConverter))]
    public Enums.OkxAccountGreeksType GreeksType { get; set; }
}
