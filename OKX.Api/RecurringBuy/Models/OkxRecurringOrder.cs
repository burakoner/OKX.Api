using OKX.Api.Common.Converters;
using OKX.Api.Common.Enums;
using OKX.Api.RecurringBuy.Converters;
using OKX.Api.RecurringBuy.Enums;

namespace OKX.Api.RecurringBuy.Models;

/// <summary>
/// Recurring Buy Order
/// </summary>
public class OkxRecurringOrder
{
    [JsonProperty("algoId")]
    public long OrderId { get; set; }

    [JsonProperty("algoClOrdId")]
    public string ClientOrderId { get; set; }

    /// <summary>
    /// Instrument type
    /// </summary>
    [JsonProperty("instType"), JsonConverter(typeof(OkxInstrumentTypeConverter))]
    public OkxInstrumentType InstrumentType { get; set; }

    /// <summary>
    /// Algo order created time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("cTime")]
    public long CreateTimestamp { get; set; }

    /// <summary>
    /// Algo order created time
    /// </summary>
    [JsonIgnore]
    public DateTime CreateTime { get { return CreateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Algo order updated  time, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Algo order updated  time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime { get { return UpdateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Algo order type. recurring: recurring buy
    /// </summary>
    [JsonProperty("algoOrdType")]
    public string AlgoOrderType { get; set; }

    [JsonProperty("state")]
    public string State { get; set; }

    [JsonProperty("stgyName")]
    public string StrategyName { get; set; }

    [JsonProperty("recurringList")]
    public List<OkxRecurringItemDetails> RecurringList { get; set; }
    
    [JsonProperty("period"), JsonConverter(typeof(OkxRecurringBuyPeriodConverter))]
    public OkxRecurringBuyPeriod Period { get; set; }

    [JsonProperty("recurringDay")]
    public int? RecurringDay { get; set; }

    [JsonProperty("recurringHour")]
    public int? RecurringHour { get; set; }

    [JsonProperty("recurringTime")]
    public int RecurringTime { get; set; }
    
    [JsonProperty("timeZone")]
    public string Timezone { get; set; }

    [JsonProperty("amt")]
    public decimal Amount { get; set; }

    [JsonProperty("investmentAmt")]
    public decimal InvestmentAmount { get; set; }

    [JsonProperty("investmentCcy")]
    public string InvestmentCurrency { get; set; }
    
    [JsonProperty("totalPnl")]
    public decimal TotalPnl { get; set; }
    
    [JsonProperty("totalAnnRate")]
    public decimal TotalAnnualRate { get; set; }
    
    [JsonProperty("pnlRatio")]
    public decimal PnlRatio { get; set; }
    
    [JsonProperty("mktCap")]
    public decimal MarketCap { get; set; }

    [JsonProperty("cycles")]
    public int Cycles { get; set; }
}