namespace OKX.Api.Public.Models;

public class OkxVipInterestRate
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("rate")]
    public decimal? Rate { get; set; }

    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    [JsonProperty("levelList")]
    public List<OkxVipInterestRateLevel> LevelList { get; set; }
}

public class OkxVipInterestRateLevel
{
    [JsonProperty("level")]
    public string Level { get; set; }

    [JsonProperty("loanQuota")]
    public decimal? LoanQuota { get; set; }
}
