namespace OKX.Api.Account;

/// <summary>
/// OkxAccountGreeksType
/// </summary>
internal record OkxAccountGreeksTypeContainer
{
    /// <summary>
    /// Display type of Greeks.
    /// </summary>
    [JsonProperty("greeksType")]
    public OkxAccountGreeksType Data { get; set; }
}
