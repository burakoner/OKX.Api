namespace OKX.Api.Account;

/// <summary>
/// Current GLP performance snapshot.
/// </summary>
public record OkxAccountGlpPerformance
{
    /// <summary>
    /// Whether performance data is available for <see cref="DataDate"/>.
    /// When false, <see cref="Programs"/> is empty.
    /// </summary>
    [JsonProperty("dataReady")]
    public bool DataReady { get; set; }

    /// <summary>
    /// Data snapshot date in yyyy-MM-dd format (UTC+8).
    /// Normally T-1 and T-2 when T-1 computation is incomplete.
    /// </summary>
    [JsonProperty("dataDate")]
    public string DataDate { get; set; } = string.Empty;

    /// <summary>
    /// Resolved GLP account identity.
    /// </summary>
    [JsonProperty("account")]
    public OkxAccountGlpAccountIdentity Account { get; set; } = new();

    /// <summary>
    /// Performance data for every enrolled GLP program.
    /// </summary>
    [JsonProperty("programs")]
    public List<OkxAccountGlpProgramPerformance> Programs { get; set; } = [];
}

/// <summary>
/// Resolved GLP account identity.
/// </summary>
public record OkxAccountGlpAccountIdentity
{
    /// <summary>
    /// Master account ID.
    /// </summary>
    [JsonProperty("masterAccountId")]
    public string MasterAccountId { get; set; } = string.Empty;

    /// <summary>
    /// Sibling account IDs in the same institutional group, excluding the current account.
    /// </summary>
    [JsonProperty("combinedAccountIds")]
    public List<string> CombinedAccountIds { get; set; } = [];
}

/// <summary>
/// GLP performance for one enrolled program.
/// </summary>
public record OkxAccountGlpProgramPerformance
{
    /// <summary>
    /// GLP business line.
    /// </summary>
    [JsonProperty("program")]
    public OkxAccountGlpProgram Program { get; set; }

    /// <summary>
    /// Market maker business ID for the program.
    /// </summary>
    [JsonProperty("marketMakerBusinessId")]
    public string MarketMakerBusinessId { get; set; } = string.Empty;

    /// <summary>
    /// Enrollment status.
    /// </summary>
    [JsonProperty("enrollmentStatus")]
    public OkxAccountGlpEnrollmentStatus EnrollmentStatus { get; set; }

    /// <summary>
    /// Current tier level ID.
    /// </summary>
    [JsonProperty("marketMakerLevelId")]
    public string MarketMakerLevelId { get; set; } = string.Empty;

    /// <summary>
    /// Current tier display name.
    /// </summary>
    [JsonProperty("enrolledTierDisplay")]
    public string EnrolledTierDisplay { get; set; } = string.Empty;

    /// <summary>
    /// Pool that determines the current tier.
    /// </summary>
    [JsonProperty("qualifyingPool")]
    public OkxAccountGlpQualifyingPool QualifyingPool { get; set; }

    /// <summary>
    /// Keys of rows that qualified for UI highlighting.
    /// </summary>
    [JsonProperty("qualifyingRows")]
    public List<string> QualifyingRows { get; set; } = [];

    /// <summary>
    /// Daily performance.
    /// </summary>
    [JsonProperty("daily")]
    public OkxAccountGlpPerformanceMetrics Daily { get; set; } = new();

    /// <summary>
    /// Month-to-date performance.
    /// </summary>
    [JsonProperty("mtd")]
    public OkxAccountGlpMonthToDatePerformance MonthToDate { get; set; } = new();
}

/// <summary>
/// GLP volume and market-share metrics.
/// </summary>
public record OkxAccountGlpPerformanceMetrics
{
    /// <summary>
    /// Trading volume by pool type in USD notional.
    /// </summary>
    [JsonProperty("volume")]
    public OkxAccountGlpVolume Volume { get; set; } = new();

    /// <summary>
    /// Market share by pool type, expressed as a decimal rather than a percentage.
    /// </summary>
    [JsonProperty("share")]
    public OkxAccountGlpShare Share { get; set; } = new();
}

/// <summary>
/// GLP month-to-date performance.
/// </summary>
public record OkxAccountGlpMonthToDatePerformance : OkxAccountGlpPerformanceMetrics
{
    /// <summary>
    /// Month-to-date qualification status.
    /// </summary>
    [JsonProperty("mtdStatus")]
    public OkxAccountGlpMonthToDateStatus Status { get; set; }

    /// <summary>
    /// Share in the qualifying pool.
    /// </summary>
    [JsonProperty("qualifyingShare")]
    public OkxAccountGlpMetricPair QualifyingShare { get; set; } = new();
}

/// <summary>
/// GLP volume breakdown in USD notional.
/// Categories other than total are null for Expiry and Nitro.
/// </summary>
public record OkxAccountGlpVolume
{
    /// <summary>
    /// Type A volume.
    /// </summary>
    [JsonProperty("typeA")]
    public OkxAccountGlpMetricPair? TypeA { get; set; }

    /// <summary>
    /// Type B total volume.
    /// </summary>
    [JsonProperty("typeBTotal")]
    public OkxAccountGlpMetricPair? TypeBTotal { get; set; }

    /// <summary>
    /// Doubled TradFi volume.
    /// </summary>
    [JsonProperty("tradfiX2")]
    public OkxAccountGlpMetricPair? TradFiX2 { get; set; }

    /// <summary>
    /// Total volume across all types.
    /// </summary>
    [JsonProperty("total")]
    public OkxAccountGlpMetricPair Total { get; set; } = new();
}

/// <summary>
/// GLP market-share breakdown.
/// Categories other than total are null for Expiry and Nitro.
/// </summary>
public record OkxAccountGlpShare
{
    /// <summary>
    /// Type A share.
    /// </summary>
    [JsonProperty("typeA")]
    public OkxAccountGlpMetricPair? TypeA { get; set; }

    /// <summary>
    /// Adjusted Type B share.
    /// </summary>
    [JsonProperty("typeBAdj")]
    public OkxAccountGlpMetricPair? TypeBAdjusted { get; set; }

    /// <summary>
    /// Total share across all types.
    /// </summary>
    [JsonProperty("total")]
    public OkxAccountGlpMetricPair Total { get; set; } = new();
}

/// <summary>
/// Maker and taker values for a GLP metric.
/// </summary>
public record OkxAccountGlpMetricPair
{
    /// <summary>
    /// Maker value.
    /// </summary>
    [JsonProperty("maker"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Maker { get; set; }

    /// <summary>
    /// Taker value.
    /// </summary>
    [JsonProperty("taker"), JsonConverter(typeof(DecimalAsStringNullableConverter))]
    public decimal? Taker { get; set; }
}
