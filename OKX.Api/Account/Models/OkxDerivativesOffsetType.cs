using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OKX Derivatives Offset Type
/// </summary>
public class OkxDerivativesOffsetType
{
    /// <summary>
    /// Type
    /// </summary>
    [JsonProperty("type"), JsonConverter(typeof(OkxDerivativesOffsetModeConverter))]
    public OkxDerivativesOffsetMode Type { get; set; }
}