namespace OKX.Api.Base;

/// <summary>
/// OKX Socket Update Response
/// </summary>
/// <typeparam name="T">Data Type</typeparam>
public record OkxSocketUpdateResponse<T> : OkxSocketResponse
{
    /// <summary>
    /// Arguments
    /// </summary>
    [JsonProperty("arg")]
    public OkxSocketUpdateArguments? Arguments { get; set; }

    /// <summary>
    /// Data
    /// </summary>
    [JsonProperty("data")]
    public T Data { get; set; } = default!;
}