namespace OKX.Api.Funding.Models;

/// <summary>
/// OKX Exchange List
/// </summary>
public class OkxExchangeList
{
    /// <summary>
    /// Exchange name, e.g. 1xbet
    /// </summary>
    [JsonProperty("exchName")]
    public string ExchangeName { get; set; }

    /// <summary>
    /// Exchange ID, e.g. did:ethr:0xfeb4f99829a9acdf52979abee87e83addf22a7e1
    /// </summary>
    [JsonProperty("exchId")]
    public string ExchangeId { get; set; }
}