namespace OKX.Api.Status;

/// <summary>
/// System Status
/// </summary>
public record OkxStatusMaintenance
{
    /// <summary>
    /// The title of system maintenance instructions
    /// </summary>
    [JsonProperty("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// System maintenance status
    /// </summary>
    [JsonProperty("state")]
    public OkxStatusMaintenanceState Status { get; set; }

    /// <summary>
    /// Begin time of system maintenance, Unix timestamp format in milliseconds, e.g. 1617788463867
    /// </summary>
    [JsonProperty("begin")]
    public long? BeginTimestamp { get; set; }

    /// <summary>
    /// Begin time of system maintenance
    /// </summary>
    [JsonIgnore]
    public DateTime? BeginTime => BeginTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// End time of system maintenance, Unix timestamp format in milliseconds, e.g. 1617788463867
    /// </summary>
    [JsonProperty("end")]
    public long? EndTimestamp { get; set; }

    /// <summary>
    /// End time of system maintenance
    /// </summary>
    [JsonIgnore]
    public DateTime? EndTime => EndTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// The time of pre_open. Canceling orders, placing Post Only orders, and transferring funds to trading accounts are back after preOpenBegin.
    /// </summary>
    [JsonProperty("preOpenBegin")]
    public long? PreOpenBeginTimestamp { get; set; }

    /// <summary>
    /// The time of pre_open. Canceling orders, placing Post Only orders, and transferring funds to trading accounts are back after preOpenBegin.
    /// </summary>
    [JsonIgnore]
    public DateTime? PreOpenBeginTime => PreOpenBeginTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Hyperlink for system maintenance details, if there is no return value, the default value will be empty. e.g. “”
    /// </summary>
    [JsonProperty("href")]
    public string Link { get; set; } = string.Empty;

    /// <summary>
    /// Service type, 0：WebSocket ; 1：Spot/Margin ; 2：Futures ; 3：Perpetual ; 4：Options ; 5：Trading service
    /// </summary>
    [JsonProperty("serviceType")]
    public OkxStatusMaintenanceService Service { get; set; }

    /// <summary>
    /// Service type, 0：WebSocket ; 1：Spot/Margin ; 2：Futures ; 3：Perpetual ; 4：Options ; 5：Trading service
    /// </summary>
    [JsonProperty("system")]
    public OkxStatusMaintenanceSystem System { get; set; }

    /// <summary>
    /// Rescheduled description，e.g. Rescheduled from 2021-01-26T16:30:00.000Z to 2021-01-28T16:30:00.000Z
    /// </summary>
    [JsonProperty("scheDesc")]
    public string RescheduledDescription { get; set; } = string.Empty;

    /// <summary>
    /// Maintenance type
    /// </summary>
    [JsonProperty("maintType")]
    public string MaintenanceType { get; set; } = string.Empty;

    /// <summary>
    /// Environment
    /// </summary>
    [JsonProperty("env")]
    public string Environment { get; set; } = string.Empty;
}
