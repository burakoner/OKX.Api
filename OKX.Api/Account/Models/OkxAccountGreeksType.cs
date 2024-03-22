using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxAccountGreeksType
/// </summary>
public class OkxAccountGreeksType
{
    /// <summary>
    /// Display type of Greeks.
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(OkxGreeksTypeConverter))]
    public OkxGreeksType GreeksType { get; set; }
}
