namespace OKX.Api.SubAccount.Models;

public class OkxSubAccountTradingBalance
{
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    [JsonProperty("totalEq")]
    public decimal TotalEquity { get; set; }

    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    [JsonProperty("adjEq")]
    public decimal? AdjustedEquity { get; set; }

    [JsonProperty("ordFroz")]
    public decimal? OrderFrozen { get; set; }

    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    [JsonProperty("details")]
    public List<OkxSubAccountTradingBalanceDetail> Details { get; set; }
}

public class OkxSubAccountTradingBalanceDetail
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("eq")]
    public decimal? Equity { get; set; }

    [JsonProperty("cashBal")]
    public decimal? CashBalance { get; set; }

    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime? UpdateTime { get { return UpdateTimestamp?.ConvertFromMilliseconds(); } }

    [JsonProperty("isoEq")]
    public decimal? IsolatedMarginEquity { get; set; }

    [JsonProperty("availEq")]
    public decimal? AvailableEquity { get; set; }

    [JsonProperty("disEq")]
    public decimal? DiscountEquity { get; set; }

    [JsonProperty("availBal")]
    public decimal? AvailableBalance { get; set; }

    [JsonProperty("frozenBal")]
    public decimal? FrozenBalance { get; set; }

    [JsonProperty("ordFrozen")]
    public decimal? OrderFrozen { get; set; }

    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    [JsonProperty("uplLiab")]
    public decimal? UnrealizedProfitAndLossLiabilities { get; set; }

    [JsonProperty("crossLiab")]
    public decimal? CrossLiabilities { get; set; }

    [JsonProperty("isoLiab")]
    public decimal? IsolatedLiabilities { get; set; }

    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    [JsonProperty("twap")]
    public decimal? Twap { get; set; }

    [JsonProperty("maxLoan")]
    public decimal? MaximumLoan { get; set; }

    [JsonProperty("eqUsd")]
    public decimal? UsdEquity { get; set; }

    [JsonProperty("notionalLever")]
    public decimal? Leverage { get; set; }
}
