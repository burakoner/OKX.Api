using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OKX Risk Offset Type
/// </summary>
public class OkxAccountRiskOffsetTypeModel
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxAccountRiskOffsetTypeConverter))]
    public OkxAccountRiskOffsetType Type { get; set; }
}