namespace OKX.Api.Models.TradingAccount;

/// <summary>
/// OkxAccountConfiguration
/// </summary>
public class OkxAccountConfiguration
{
    /// <summary>
    /// Account ID of current request.
    /// </summary>
    [JsonProperty("uid")]
    public long UserId { get; set; }

    /// <summary>
    /// Main Account ID of current request.
    /// The current request account is main account if uid = mainUid.
    /// The current request account is sub-account if uid != mainUid.
    /// </summary>
    [JsonProperty("mainUid")]
    public long MainUserId { get; set; }

    /// <summary>
    /// Account level
    /// </summary>
    [JsonProperty("acctLv"), JsonConverter(typeof(AccountLevelConverter))]
    public OkxAccountLevel AccountLevel { get; set; }

    /// <summary>
    /// Position mode
    /// </summary>
    [JsonProperty("posMode"), JsonConverter(typeof(PositionModeConverter))]
    public OkxPositionMode PositionMode { get; set; }

    /// <summary>
    /// Whether to borrow coins automatically
    /// </summary>
    [JsonProperty("autoLoan"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool AutoLoan { get; set; }

    /// <summary>
    /// Current display type of Greeks
    /// </summary>
    [JsonProperty("greeksType"), JsonConverter(typeof(GreeksTypeConverter))]
    public OkxGreeksType GreeksType { get; set; }

    /// <summary>
    /// The user level of the current real trading volume on the platform, e.g lv1
    /// </summary>
    [JsonProperty("level")]
    public string Level { get; set; }

    /// <summary>
    /// Temporary experience user level of special users, e.g lv3
    /// </summary>
    [JsonProperty("levelTmp")]
    public string LevelTemporary { get; set; }

    /// <summary>
    /// Contract isolated margin trading settings
    /// </summary>
    [JsonProperty("ctIsoMode"), JsonConverter(typeof(IsolatedMarginModeConverter))]
    public Enums.OkxIsolatedMarginMode ContractIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Margin isolated margin trading settings
    /// </summary>
    [JsonProperty("mgnIsoMode"), JsonConverter(typeof(IsolatedMarginModeConverter))]
    public Enums.OkxIsolatedMarginMode MarginIsolatedMarginTradingMode { get; set; }

    /// <summary>
    /// Risk offset type
    /// Only applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotOffsetType"), JsonConverter(typeof(RiskOffsetTypeConverter))]
    public OkxRiskOffsetType? RiskOffsetType { get; set; }

    /// <summary>
    /// Role type.
    /// </summary>
    [JsonProperty("roleType"), JsonConverter(typeof(AccountRoleTypeConverter))]
    public OkxAccountRoleType? AccountRoleType { get; set; }

    /// <summary>
    /// Leading trade instruments, only applicable to Leading trader
    /// </summary>
    [JsonProperty("traderInsts")]
    public IEnumerable<string> LeadingTraderInstruments { get; set; }

    /// <summary>
    /// SPOT copy trading role type
    /// 0: General user；1：Leading trader；2：Copy trader
    /// </summary>
    [JsonProperty("spotRoleType"), JsonConverter(typeof(SpotCopyTradingRoleConverter))]
    public OkxSpotCopyTradingRole? SpotCopyTradingRole { get; set; }

    /// <summary>
    /// Spot lead trading instruments, only applicable to Leanding trader
    /// </summary>
    [JsonProperty("spotTraderInsts")]
    public IEnumerable<string> SpotLeadingTraderInstruments { get; set; }

    /// <summary>
    /// Whether the optional trading was activated
    /// </summary>
    [JsonProperty("opAuth"), JsonConverter(typeof(OkxBooleanConverter))]
    public bool OptionalTradingActivated { get; set; }

    /// <summary>
    /// Main account KYC level
    /// 0: No verification 1: level 1 completed, 2: level 2 completed, 3: level 3 completed.
    /// If the request originates from a subaccount, kycLv is the KYC level of the main account.
    /// If the request originates from the main account, kycLv is the KYC level of the current account.
    /// </summary>
    [JsonProperty("kycLv"), JsonConverter(typeof(KycLevelConverter))]
    public OkxKycLevel KycLevel { get; set; }

    /// <summary>
    /// API key note of current request API key. No more than 50 letters (case sensitive) or numbers, which can be pure letters or pure numbers.
    /// </summary>
    [JsonProperty("label")]
    public string Label { get; set; }

    /// <summary>
    /// IP addresses that linked with current API key, separate with commas if more than one, e.g. 117.37.203.58,117.37.203.57. It is an empty string "" if there is no IP bonded.
    /// </summary>
    [JsonProperty("ip")]
    public string IpAddresses { get; set; }

    /// <summary>
    /// The permission of the urrent request API Key. read_only：Read only；trade ：Trade; withdraw: Withdraw
    /// </summary>
    [JsonProperty("perm"), JsonConverter(typeof(ApiKeyPermissionConverter))]
    public OkxApiKeyPermission ApiKeyPermission { get; set; }
}
