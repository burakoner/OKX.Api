﻿using OKX.Api.SignalBot.Converters;
using OKX.Api.SignalBot.Enums;

namespace OKX.Api.SignalBot.Models;

/// <summary>
/// OKX Signal Entry Paramaters
/// </summary>
public class OkxSignalBotEntryParamaters
{
    /// <summary>
    /// Whether or not allow multiple entries in the same direction for the same trading pairs.The default value is true。 true：Allow false：Prohibit
    /// </summary>
    [JsonProperty("allowMultipleEntry")]
    public bool AllowMultipleEntry { get; set; }

    /// <summary>
    /// Entry type
    /// </summary>
    [JsonProperty("entryType"), JsonConverter(typeof(OkxSignalBotEntryTypeConverter))]
    public OkxSignalBotEntryType EntryType { get; set; }

    /// <summary>
    /// Amount per order. Only applicable to entryType in 2/3
    /// </summary>
    [JsonProperty("amt")]
    public string Amount { get; set; }

    /// <summary>
    /// Amount ratio per order. Only applicable to entryType in 4/5
    /// </summary>
    [JsonProperty("ratio")]
    public string Ratio { get; set; }
}