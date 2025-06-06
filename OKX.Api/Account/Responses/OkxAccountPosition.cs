﻿namespace OKX.Api.Account;

/// <summary>
/// OkxPosition
/// </summary>
public record OkxAccountPosition
{
    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType")]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Margin mode
    /// </summary>
    [JsonProperty("mgnMode")]
    public OkxAccountMarginMode MarginMode { get; set; }

    /// <summary>
    /// Position ID
    /// </summary>
    [JsonProperty("posId")]
    public long PositionId { get; set; }

    /// <summary>
    /// Position side
    /// </summary>
    [JsonProperty("posSide")]
    public OkxTradePositionSide PositionSide { get; set; }

    /// <summary>
    /// Quantity of positions. In the mode of autonomous transfer from position to position, after the deposit is transferred, a position with pos of 0 will be generated
    /// </summary>
    [JsonProperty("pos")]
    public decimal PositionsQuantity { get; set; }

    /// <summary>
    /// Position currency, only applicable to MARGIN positions.
    /// </summary>
    [JsonProperty("posCcy")]
    public string PositionCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Position that can be closed
    /// Only applicable to MARGIN, FUTURES/SWAP in the long-short mode and OPTION
    /// </summary>
    [JsonProperty("availPos")]
    public decimal? AvailablePositions { get; set; }

    /// <summary>
    /// Average open price
    /// </summary>
    [JsonProperty("avgPx")]
    public decimal? AveragePrice { get; set; }

    /// <summary>
    /// Non-settlement entry price
    /// The non-settlement entry price only reflects the average price at which the position is opened or increased.
    /// Applicable to cross FUTURES positions.
    /// </summary>
    [JsonProperty("nonSettleAvgPx")]
    public decimal? NonSettlementAveragePrice { get; set; }

    /// <summary>
    /// Latest Mark price
    /// </summary>
    [JsonProperty("markPx")]
    public decimal? MarkPrice { get; set; }

    /// <summary>
    /// Unrealized profit and loss calculated by mark price.
    /// </summary>
    [JsonProperty("upl")]
    public decimal? UnrealizedProfitAndLoss { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio calculated by mark price.
    /// </summary>
    [JsonProperty("uplRatio")]
    public decimal? UnrealizedProfitAndLossRatio { get; set; }

    /// <summary>
    /// Unrealized profit and loss calculated by last price. Main usage is showing, actual value is upl.
    /// </summary>
    [JsonProperty("uplLastPx")]
    public decimal? UnrealizedProfitAndLossLastPrice { get; set; }

    /// <summary>
    /// Unrealized profit and loss ratio calculated by last price.
    /// </summary>
    [JsonProperty("uplRatioLastPx")]
    public decimal? UnrealizedProfitAndLossRatioLastPrice { get; set; }

    /// <summary>
    /// Instrument ID, e.g. BTC-USD-180216
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; } = string.Empty;

    /// <summary>
    /// Leverage, not applicable to OPTION
    /// </summary>
    [JsonProperty("lever")]
    public decimal? Leverage { get; set; }

    /// <summary>
    /// Estimated liquidation price
    /// Not applicable to OPTION
    /// </summary>
    [JsonProperty("liqPx")]
    public decimal? LiquidationPrice { get; set; }

    /// <summary>
    /// Initial margin requirement, only applicable to cross.
    /// </summary>
    [JsonProperty("imr")]
    public decimal? InitialMarginRequirement { get; set; }

    /// <summary>
    /// Margin, can be added or reduced. Only applicable to isolated.
    /// </summary>
    [JsonProperty("margin")]
    public decimal? Margin { get; set; }

    /// <summary>
    /// Margin ratio
    /// </summary>
    [JsonProperty("mgnRatio")]
    public decimal? MarginRatio { get; set; }

    /// <summary>
    /// Maintenance margin requirement
    /// </summary>
    [JsonProperty("mmr")]
    public decimal? MaintenanceMarginRequirement { get; set; }

    /// <summary>
    /// Liabilities, only applicable to MARGIN.
    /// </summary>
    [JsonProperty("liab")]
    public decimal? Liabilities { get; set; }

    /// <summary>
    /// Liabilities currency, only applicable to MARGIN.
    /// </summary>
    [JsonProperty("liabCcy")]
    public string LiabilitiesCurrency { get; set; } = string.Empty;

    /// <summary>
    /// Interest. Undeducted interest that has been incurred.
    /// </summary>
    [JsonProperty("interest")]
    public decimal? Interest { get; set; }

    /// <summary>
    /// Last trade ID
    /// </summary>
    [JsonProperty("tradeId")]
    public long? TradeId { get; set; }

    /// <summary>
    /// Option Value, only applicable to OPTION.
    /// </summary>
    [JsonProperty("optVal")]
    public decimal? OptionValue { get; set; }

    /// <summary>
    /// The amount of close orders of isolated margin liability.
    /// </summary>
    [JsonProperty("pendingCloseOrdLiabVal")]
    public decimal? PendingCloseOrdersLiabilityValue { get; set; }

    /// <summary>
    /// Notional value of positions in USD
    /// </summary>
    [JsonProperty("notionalUsd")]
    public decimal? NotionalUsd { get; set; }

    /// <summary>
    /// Auto-deleveraging (ADL) indicator
    /// Divided into 5 levels, from 1 to 5, the smaller the number, the weaker the adl intensity.
    /// </summary>
    [JsonProperty("adl")]
    public decimal? AutoDeleveragingIndicator { get; set; }

    /// <summary>
    /// Currency used for margin
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Latest traded price
    /// </summary>
    [JsonProperty("last")]
    public decimal? LastPrice { get; set; }

    /// <summary>
    /// Latest underlying index price
    /// </summary>
    [JsonProperty("idxPx")]
    public decimal? IndexPrice { get; set; }

    /// <summary>
    /// USD price
    /// </summary>
    [JsonProperty("usdPx")]
    public decimal? UsdPrice { get; set; }

    /// <summary>
    /// Breakeven price
    /// </summary>
    [JsonProperty("bePx")]
    public decimal? BreakevenPrice { get; set; }

    /// <summary>
    /// delta：Black-Scholes Greeks in dollars, only applicable to OPTION
    /// </summary>
    [JsonProperty("deltaBS")]
    public decimal? DeltaBS { get; set; }

    /// <summary>
    /// delta：Greeks in coins, only applicable to OPTION
    /// </summary>
    [JsonProperty("deltaPA")]
    public decimal? DeltaPA { get; set; }

    /// <summary>
    /// gamma：Black-Scholes Greeks in dollars, only applicable to OPTION
    /// </summary>
    [JsonProperty("gammaBS")]
    public decimal? GammaBS { get; set; }

    /// <summary>
    /// gamma：Greeks in coins, only applicable to OPTION
    /// </summary>
    [JsonProperty("gammaPA")]
    public decimal? GammaPA { get; set; }

    /// <summary>
    /// theta：Black-Scholes Greeks in dollars, only applicable to OPTION
    /// </summary>
    [JsonProperty("thetaBS")]
    public decimal? ThetaBS { get; set; }

    /// <summary>
    /// theta：Greeks in coins, only applicable to OPTION
    /// </summary>
    [JsonProperty("thetaPA")]
    public decimal? ThetaPA { get; set; }

    /// <summary>
    /// vega：Black-Scholes Greeks in dollars, only applicable to OPTION
    /// </summary>
    [JsonProperty("vegaBS")]
    public decimal? VegaBS { get; set; }

    /// <summary>
    /// vega：Greeks in coins, only applicable to OPTION
    /// </summary>
    [JsonProperty("vegaPA")]
    public decimal? VegaPA { get; set; }

    /// <summary>
    /// Spot in use amount
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotInUseAmt")]
    public decimal? SpotInUseAmount { get; set; }

    /// <summary>
    /// Spot in use unit, e.g. BTC
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("spotInUseCcy")]
    public string SpotInUseCurrency { get; set; } = string.Empty;

    /// <summary>
    /// User-defined spot risk offset amount
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("clSpotInUseAmt")]
    public decimal? ClientSpotInUseAmount { get; set; }

    /// <summary>
    /// Max possible spot risk offset amount
    /// Applicable to Portfolio margin
    /// </summary>
    [JsonProperty("maxSpotInUseAmt")]
    public decimal? MaximumSpotInUseAmount { get; set; }

    /// <summary>
    /// External business id, e.g. experience coupon id
    /// </summary>
    [JsonProperty("bizRefId")]
    public string ExternalBusinessId { get; set; } = string.Empty;

    /// <summary>
    /// External business type
    /// </summary>
    [JsonProperty("bizRefType")]
    public string ExternalBusinessType { get; set; } = string.Empty;

    /// <summary>
    /// Realized profit and loss
    /// </summary>
    [JsonProperty("realizedPnl")]
    public decimal? RealizedPnl { get; set; }

    /// <summary>
    /// Settled profit and loss (calculated by settlement price)
    /// Only applicable to cross FUTURES
    /// </summary>
    [JsonProperty("settledPnl")]
    public decimal? SettledPnl { get; set; }

    /// <summary>
    /// Accumulated pnl of closing order(s)
    /// </summary>
    [JsonProperty("pnl")]
    public decimal? Pnl { get; set; }

    /// <summary>
    /// Accumulated fee
    /// Negative number represents the user transaction fee charged by the platform.Positive number represents rebate.
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// Accumulated funding fee
    /// </summary>
    [JsonProperty("fundingFee")]
    public decimal? FundingFee { get; set; }

    /// <summary>
    /// Accumulated liquidation penalty. It is negative when there is a value.
    /// </summary>
    [JsonProperty("liqPenalty")]
    public decimal? LiquidationPenalty { get; set; }

    /// <summary>
    /// Close position algo orders attached to the position. This array will have values only after you request "Place algo order" with closeFraction=1.
    /// </summary>
    [JsonProperty("closeOrderAlgo")]
    public List<OkxAccountPositionCloseOrder> CloseAlgoOrders { get; set; } = [];

    /// <summary>
    /// Creation time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Creation time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime => CreateTimestamp.ConvertFromMilliseconds();

    /// <summary>
    /// Latest time position was adjusted, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long? UpdateTimestamp { get; set; }

    /// <summary>
    /// Latest time position was adjusted
    /// </summary>
    [JsonIgnore]
    public DateTime? UpdateTime => UpdateTimestamp?.ConvertFromMilliseconds();
}

/// <summary>
/// OKX Close Algo Order
/// </summary>
public record OkxAccountPositionCloseOrder
{
    /// <summary>
    /// Algo ID
    /// </summary>
    [JsonProperty("algoId")]
    public long AlgoId { get; set; }

    /// <summary>
    /// Stop-loss trigger price.
    /// </summary>
    [JsonProperty("slTriggerPx")]
    public decimal? StopLossTriggerPrice { get; set; }

    /// <summary>
    /// Stop-loss trigger price type.
    /// </summary>
    [JsonProperty("slTriggerPxType")]
    public OkxAlgoPriceType? StopLossTriggerPriceType { get; set; }

    /// <summary>
    /// Take-profit trigger price.
    /// </summary>
    [JsonProperty("tpTriggerPx")]
    public decimal? TakeProfitTriggerPrice { get; set; }

    /// <summary>
    /// Take-profit trigger price type.
    /// </summary>
    [JsonProperty("tpTriggerPxType")]
    public OkxAlgoPriceType? TakeProfitTriggerPriceType { get; set; }

    /// <summary>
    /// Fraction of position to be closed when the algo order is triggered
    /// </summary>
    [JsonProperty("closeFraction")]
    public decimal? CloseFraction { get; set; }
}