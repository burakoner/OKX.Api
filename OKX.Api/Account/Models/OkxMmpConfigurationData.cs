namespace OKX.Api.Account.Models;

/// <summary>
/// Okx MMP Configuration
/// </summary>
public class OkxMmpConfigurationData
{
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; }

    [JsonProperty("timeInterval")]
    public int TimeInterval { get; set; }

    [JsonProperty("frozenInterval")]
    public int FrozenInterval { get; set; }

    [JsonProperty("qtyLimit")]
    public int QuantityLimit { get; set; }

    [JsonProperty("mmpFrozen")]
    public bool MmpFrozen { get; set; }

    [JsonProperty("mmpFrozenUntil")]
    public string MmpFrozenUntil { get; set; }
}