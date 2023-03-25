namespace OKX.Api.Models.Account;

public class OkxAccountConfiguration
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("acctLv"), JsonConverter(typeof(AccountLevelConverter))]
    public OkxAccountLevel AccountLevel { get; set; }

    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OkxPositionMode PositionMode { get; set; }

    [JsonProperty("autoLoan"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool AutoLoan { get; set; }

    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OkxGreeksType GreeksType { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("levelTmp")]
    public string LevelTemporary { get; set; }

    [JsonProperty("ctIsoMode"), JsonConverter(typeof(MarginTransferModeConverter))]
    public OkxMarginTransferMode ContractIsolatedMarginTradingMode { get; set; }

    [JsonProperty("mgnIsoMode"), JsonConverter(typeof(MarginTransferModeConverter))]
    public OkxMarginTransferMode MarginIsolatedMarginTradingMode { get; set; }

    [JsonProperty("liquidationGear")]
    public string liquidationGear { get; set; }

    [JsonProperty("spotOffsetType")]
    public string spotOffsetType { get; set; }
}
