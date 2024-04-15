using OKX.Api.Account.Converters;
using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.Account.Models;

/// <summary>
/// OkxMultipleOperation
/// </summary>
public class OkxMultipleOperation
{
    /// <summary>
    /// Instrument ID setted successfully
    /// </summary>
    [JsonProperty("succInstId")]
    public string SuccessInstrumentId{ get; set; }

    /// <summary>
    /// Instrument ID setted unsuccessfully
    /// </summary>
    [JsonProperty("failInstId")]
    public string FailInstrumentId { get; set; }

    /// <summary>
    /// Result
    /// </summary>
    [JsonProperty("result"), JsonConverter(typeof(OkxMultipleOperationStatusConverter))]
    public OkxMultipleOperationStatus Result { get; set; }
}
