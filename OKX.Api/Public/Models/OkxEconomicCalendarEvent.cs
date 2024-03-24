using OKX.Api.Public.Converters;

namespace OKX.Api.Public.Models;

public class OkxEconomicCalendarEvent
{
    [JsonProperty("calendarId")]
    public long CalendarId { get; set; }

    [JsonProperty("date")]
    public long DateStamp { get; set; }

    [JsonIgnore]
    public DateTime Date { get { return DateStamp.ConvertFromMilliseconds(); } }

    [JsonProperty("region")]
    public string Region { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; }

    [JsonProperty("event")]
    public string EventName { get; set; }

    [JsonProperty("refDate")]
    public long RefDateStamp { get; set; }

    [JsonIgnore]
    public DateTime RefDate { get { return RefDateStamp.ConvertFromMilliseconds(); } }

    [JsonProperty("actual")]
    public string Actual { get; set; }

    [JsonProperty("previous")]
    public string Previous { get; set; }

    [JsonProperty("forecast")]
    public string Forecast { get; set; }

    [JsonProperty("dateSpan")]
    public bool HasExactTime { get; set; }

    [JsonProperty("importance"), JsonConverter(typeof(OkxEventImportanceConverter))]
    public string Importance { get; set; }

    [JsonProperty("uTime")]
    public long UpdateTimestamp { get; set; }

    [JsonIgnore]
    public DateTime UpdateTime { get { return UpdateTimestamp.ConvertFromMilliseconds(); } }

    [JsonProperty("prevInitial")]
    public string PreviousInitial { get; set; }

    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("unit")]
    public string Unit { get; set; }
}