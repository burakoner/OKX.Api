namespace OKX.Api.Rubik;

/// <summary>
/// OKX Contract Open Interest History
/// </summary>
[JsonConverter(typeof(ArrayConverter))]
public record OkxRubikContractOpenInterestHistory
{
    /// <summary>
    /// Timestamp, millisecond format of Unix timestamp, e.g. 1597026383085
    /// </summary>
    [ArrayProperty(0)]
    public long Timestamp { get; set; }

    /// <summary>
    /// Time
    /// </summary>
    [JsonIgnore]
    public DateTime Time => Timestamp.ConvertFromMilliseconds();
    
    /// <summary>
    /// Open interest in the unit of contracts
    /// </summary>
    [ArrayProperty(1)]
    public decimal OpenInterestInTheUnitOfContracts { get; set; }
    
    /// <summary>
    /// Open interest in the unit of crypto
    /// </summary>
    [ArrayProperty(2)]
    public decimal OpenInterestInTheUnitOfCrypto { get; set; }
    
    /// <summary>
    /// Open interest in the unit of USD
    /// </summary>
    [ArrayProperty(3)]
    public decimal OpenInterestInTheUnitOfUsd { get; set; }
}
