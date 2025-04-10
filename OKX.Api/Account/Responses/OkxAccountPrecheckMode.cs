namespace OKX.Api.Account;

/// <summary>
/// Precheck Account Mode
/// </summary>
public record OkxAccountPrecheckMode
{
    /// <summary>
    /// Check code
    /// 0: pass all checks
    /// 1: unmatched information
    /// 3: leverage setting is not finished
    /// 4: position tier or margin check is not passed
    /// </summary>
    [JsonProperty("sCode")]
    public int CheckCode { get; set; }

    /// <summary>
    /// Current account mode
    /// </summary>
    [JsonProperty("curAcctLv")]
    public OkxAccountMode CurrentAccountMode { get; set; }

    /// <summary>
    /// Account mode after switch
    /// </summary>
    [JsonProperty("acctLv")]
    public OkxAccountMode AccountMode { get; set; }

    /// <summary>
    /// Unmatched information list
    /// Applicable when sCode is 1, indicating there is unmatched information; return [] for other scenarios
    /// </summary>
    [JsonProperty("unmatchedInfoCheck")]
    public List<OkxAccountPrecheckModeUnmatchedInformation>? UnmatchedInformationList { get; set; } = [];

    /// <summary>
    /// Cross margin contract position list
    /// Applicable when curAcctLv is 4, acctLv is 2/3 and user has cross margin contract positions
    /// Applicable when sCode is 0/3/4
    /// </summary>
    [JsonProperty("posList")]
    public List<OkxAccountPrecheckModeUnmatchedPosition>? PositionList { get; set; } = [];

    /// <summary>
    /// Cross margin contract positions that don't pass the position tier check
    /// Only applicable when sCode is 4
    /// </summary>
    [JsonProperty("posTierCheck")]
    public List<OkxAccountPrecheckModePositionTierCheck>? PositionTierCheckList { get; set; } = [];

    /// <summary>
    /// The margin related information before switching account mode
    /// Applicable when sCode is 0/4, return null for other scenarios
    /// </summary>
    [JsonProperty("mgnBf")]
    public OkxAccountPrecheckModeMargin? MarginBefore { get; set; }

    /// <summary>
    /// The margin related information after switching account mode
    /// Applicable when sCode is 0/4, return null for other scenarios
    /// </summary>
    [JsonProperty("mgnAft")]
    public OkxAccountPrecheckModeMargin? MarginAfter { get; set; }
}

/// <summary>
/// OkxAccountPrecheckModeUnmatchedInformation
/// </summary>
public record OkxAccountPrecheckModeUnmatchedInformation
{
    /// <summary>
    /// Unmatched information type
    /// asset_validation: asset validation
    /// pending_orders: order book pending orders
    /// pending_algos: pending algo orders and trading bots, such as iceberg, recurring buy and twap
    /// isolated_margin: isolated margin(quick margin and manual transfers)
    /// isolated_contract: isolated contract(manual transfers)
    /// contract_long_short: contract positions in hedge mode
    /// cross_margin: cross margin positions
    /// cross_option_buyer: cross options buyer
    /// isolated_option: isolated options(only applicable to spot mode)
    /// growth_fund: positions with trial funds
    /// all_positions: all positions
    /// spot_lead_copy_only_simple_single: copy trader and customize lead trader can only use spot mode or spot and futures mode
    /// stop_spot_custom: spot customize copy trading
    /// stop_futures_custom: contract customize copy trading
    /// lead_portfolio: lead trader can not switch to portfolio margin mode
    /// futures_smart_sync: you can not switch to spot mode when having smart contract sync
    /// vip_fixed_loan: vip loan
    /// repay_borrowings: borrowings
    /// compliance_restriction: due to compliance restrictions, margin trading services are unavailable
    /// compliance_kyc2: Due to compliance restrictions, margin trading services are unavailable.If you are not a resident of this region, please complete kyc2 identity verification.
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; } = "";

    /// <summary>
    /// Total assets
    /// Only applicable when type is asset_validation, return "" for other scenarios
    /// </summary>
    [JsonProperty("totalAsset")]
    public decimal? TotalAsset { get; set; }

    /// <summary>
    /// Unmatched position list (posId)
    /// Applicable when type is related to positions, return [] for other scenarios
    /// </summary>
    [JsonProperty("posList")]
    public List<OkxAccountPrecheckModeUnmatchedPosition>? PositionList { get; set; } = [];
}

/// <summary>
/// OkxAccountPrecheckModeUnmatchedPosition
/// </summary>
public record OkxAccountPrecheckModeUnmatchedPosition
{
    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("lever")]
    public decimal Leverage { get; set; }

    /// <summary>
    /// Leverage of cross margin contract positions after switch
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }
}

/// <summary>
/// OkxAccountPrecheckModePositionTierCheck
/// </summary>
public record OkxAccountPrecheckModePositionTierCheck
{
    /// <summary>
    /// Instrument family
    /// </summary>
    [JsonProperty("instFamily")]
    public string InstrumentFamily { get; set; } = "";

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Quantity of position
    /// </summary>
    [JsonProperty("pos")]
    public decimal? Quantity { get; set; }

    /// <summary>
    /// Leverage
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// If acctLv is 2/3, it refers to the maximum position size allowed at the current leverage. If acctLv is 4, it refers to the maximum position limit for cross-margin positions under the PM mode.
    /// </summary>
    [JsonProperty("maxSz")]
    public decimal? MaximumSize { get; set; }
}

/// <summary>
/// OkxAccountPrecheckModeMargin
/// </summary>
public record OkxAccountPrecheckModeMargin
{
    /// <summary>
    /// Account available equity in USD
    /// Applicable when acctLv is 3/4, return "" for other scenarios
    /// </summary>
    [JsonProperty("acctAvailEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Margin ratio in USD
    /// Applicable when acctLv is 3/4, return "" for other scenarios
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Detailed information
    /// Only applicable when acctLv is 2, return "" for other scenarios
    /// </summary>
    [JsonProperty("details")]
    public List<OkxAccountPrecheckModeMarginDetail>? Details { get; set; } = [];
}

/// <summary>
/// OkxAccountPrecheckModeMarginDetail
/// </summary>
public record OkxAccountPrecheckModeMarginDetail
{
    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = "";

    /// <summary>
    /// Available equity of currency
    /// </summary>
    [JsonProperty("availEq")]
    public decimal? AvailableEquity { get; set; }

    /// <summary>
    /// Margin ratio of currency
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

}