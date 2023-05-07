namespace OKX.Api.Models.PublicData;

public class OkxVipInterestRate
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    [JsonProperty("levelList")]
    public IEnumerable<OkxVipInterestRateLevel> LevelList { get; set; }
}

public class OkxVipInterestRateLevel
{
    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("loanQuota")]
    public decimal? LoanQuota { get; set; }
}
