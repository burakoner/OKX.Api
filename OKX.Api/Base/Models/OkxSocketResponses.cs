namespace OKX.Api.Base;

/// <summary>
/// OKX Socket Response
/// </summary>
public class OkxSocketResponse
{
    /// <summary>
    /// Success
    /// </summary>
    public bool Success => string.IsNullOrEmpty(ErrorCode) || ErrorCode.Trim() == "0";

    /// <summary>
    /// Event
    /// </summary>
    [JsonProperty("event")]
    public string Event { get; set; }

    /// <summary>
    /// Error Code
    /// </summary>
    [JsonProperty("code")]
    public string ErrorCode { get; set; }

    /// <summary>
    /// Error Message
    /// </summary>
    [JsonProperty("msg")]
    public string ErrorMessage { get; set; }
}

/// <summary>
/// OKX Socket Update Response
/// </summary>
/// <typeparam name="T">Data Type</typeparam>
public class OkxSocketUpdateResponse<T> : OkxSocketResponse
{
    /// <summary>
    /// Arguments
    /// </summary>
    [JsonProperty("arg")]
    public OkxSocketUpdateArguments Arguments { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}

/// <summary>
/// OKX Order Book Update
/// </summary>
public class OkxOrderBookUpdate
{
    /// <summary>
    /// Action
    /// </summary>
    [JsonProperty("action")]
    public string Action { get; set; }

    /// <summary>
    /// Arguments
    /// </summary>
    [JsonProperty("arg")]
    public OkxSocketUpdateArguments Arguments { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    [JsonProperty("data")]
    public List<OkxPublicOrderBookStream> Data { get; set; } = default!;
}

/// <summary>
/// OKX Socket Update Arguments
/// </summary>
public class OkxSocketUpdateArguments
{
    /// <summary>
    /// Channel
    /// </summary>
    [JsonProperty("channel")]
    public string Channel { get; set; }

    /// <summary>
    /// Instrument Id
    /// </summary>
    [JsonProperty("instId")]
    public string InstrumentId { get; set; }
}
