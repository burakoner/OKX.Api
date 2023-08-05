namespace OKX.Api.Models.TradingAccount;

public class OkxAccountConfiguration
{
    [JsonProperty("uid")]
    public long UserId { get; set; }

    [JsonProperty("mainUid")]
    public long MainUserId { get; set; }

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

    [JsonProperty("spotOffsetType"), JsonConverter(typeof(RiskOffsetTypeConverter))]
    public OkxRiskOffsetType? RiskOffsetType { get; set; }

    [JsonProperty("label")]
    public string Label { get; set; }

    [JsonProperty("roleType"), JsonConverter(typeof(AccountRoleTypeConverter))]
    public OkxAccountRoleType? AccountRoleType { get; set; }

    [JsonProperty("traderInsts")]
    public IEnumerable<string> TraderInstruments { get; set; }

    [JsonProperty("opAuth"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool OptionalTradingActivated { get; set; }

    [JsonProperty("ip")]
    public string IpAddresses { get; set; }

    [JsonProperty("perm"), JsonConverter(typeof(ApiKeyPermissionConverter))]
    public OkxApiKeyPermission ApiKeyPermission { get; set; }

    [JsonProperty("kycLv"), JsonConverter(typeof(KycLevelConverter))]
    public OkxKycLevel KycLevel { get; set; }
}
