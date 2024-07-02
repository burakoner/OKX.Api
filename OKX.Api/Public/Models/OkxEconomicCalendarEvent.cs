using OKX.Api.Public.Converters;

namespace OKX.Api.Public.Models;

/// <summary>
/// OKX Economic Calendar Event
/// </summary>
public class OkxEconomicCalendarEvent
{
    /// <summary>
    /// Calendar ID
    /// </summary>
    [JsonProperty("calendarId")]
    public long CalendarId { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    [JsonProperty("date")]
    public long DateStamp { get; set; }

    /// <summary>
    /// Date
    /// </summary>
    [JsonIgnore]
    public DateTime Date { get { return DateStamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Region
    /// </summary>
    [JsonProperty("region")]
    public string Region { get; set; }

    /// <summary>
    /// Category
    /// </summary>
    [JsonProperty("category")]
    public string Category { get; set; }

    /// <summary>
    /// Event Name
    /// </summary>
    [JsonProperty("event")]
    public string EventName { get; set; }

    /// <summary>
    /// Reference Date
    /// </summary>
    [JsonProperty("refDate")]
    public long RefDateStamp { get; set; }

    /// <summary>
    /// Reference Date
    /// </summary>
    [JsonIgnore]
    public DateTime RefDate { get { return RefDateStamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Actual
    /// </summary>
    [JsonProperty("actual")]
    public string Actual { get; set; }

    /// <summary>
    /// Previous
    /// </summary>
    [JsonProperty("previous")]
    public string Previous { get; set; }

    /// <summary>
    /// Forecast
    /// </summary>
    [JsonProperty("forecast")]
    public string Forecast { get; set; }

    /// <summary>
    /// Has exact time
    /// </summary>
    [JsonProperty("dateSpan")]
    public bool HasExactTime { get; set; }

    /// <summary>
    /// Importance
    /// </summary>
    [JsonProperty("importance"), JsonConverter(typeof(OkxEventImportanceConverter))]
    public string Importance { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    /// <summary>
    /// Update Time
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateTime { get { return UpdateTimestamp.ConvertFromMilliseconds(); } }

    /// <summary>
    /// Previous Initial
    /// </summary>
    [JsonProperty("prevInitial")]
    public string PreviousInitial { get; set; }

    /// <summary>
    /// Currency
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Unit
    /// </summary>
    [JsonProperty("unit")]
    public string Unit { get; set; }
}