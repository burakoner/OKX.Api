using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OKX Risk Offset Type
/// </summary>
public class OkxRiskOffsetTypeModel
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxRiskOffsetTypeConverter))]
    public OkxRiskOffsetType Type { get; set; }
}