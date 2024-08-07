﻿using OKX.Api.CopyTrading.Converters;
using OKX.Api.CopyTrading.Enums;

namespace OKX.Api.CopyTrading.Models;

/// <summary>
/// OkxCopyTradingConfiguration
/// </summary>
public class OkxCopyTradingAccountConfiguration
{
    /// <summary>
    /// Nickname
    /// </summary>
    [JsonProperty("nickName")]
    public string NickName { get; set; }

    /// <summary>
    /// Portrait link
    /// </summary>
    [JsonProperty("portLink")]
    public string PortraitLink { get; set; }

    /// <summary>
    /// User unique code
    /// </summary>
    [JsonProperty("uniqueCode")]
    public string UniqueCode { get; set; }

    /// <summary>
    /// Details
    /// </summary>
    [JsonProperty("details")]
    public List<OkxCopyTradingAccountConfigurationDetails> Details { get; set; } = [];
}

/// <summary>
/// OkxCopyTradingConfigurationDetails
/// </summary>
public class OkxCopyTradingAccountConfigurationDetails
{
    /// <summary>
    /// Current number of copy traders
    /// </summary>
    [JsonProperty("copyTraderNum")]
    public int? NumberOfCopyTraders { get; set; }

    /// <summary>
    /// Maximum number of copy traders
    /// </summary>
    [JsonProperty("maxCopyTraderNum")]
    public int? MaximumCopyTraderNum { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Profit sharing ratio.
    /// Only applicable to lead trader, or it will be "". 0.1 represents 10%
    /// </summary>
    [JsonProperty("profitSharingRatio")]
    public decimal? ProfitSharingRatio { get; set; }

    /// <summary>
    /// Role type
    /// </summary>
    [JsonProperty("roleType"), JsonConverter(typeof(OkxCopyTradingRoleConverter))]
    public OkxCopyTradingRole RoleType { get; set; }
}