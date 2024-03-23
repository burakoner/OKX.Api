using OKX.Api.Account.Converters;
using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Models;

public class OkxDerivativesOffsetType
{
    [JsonProperty("type"), JsonConverter(typeof(OkxDerivativesOffsetModeConverter))]
    public OkxDerivativesOffsetMode Type { get; set; }
}