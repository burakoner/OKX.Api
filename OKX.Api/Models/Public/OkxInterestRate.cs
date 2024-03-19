namespace OKX.Api.Models.Public;

public class OkxInterestRate
{
    [JsonProperty("basic")]
    public IEnumerable<OkxPublicInterestRateBasic> Basic { get; set; }

    [JsonProperty("vip")]
    public IEnumerable<OkxPublicInterestRateVip> Vip { get; set; }

    [JsonProperty("regular")]
    public IEnumerable<OkxPublicInterestRateRegular> regular { get; set; }

}

public class OkxPublicInterestRateBasic
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("quota")]
    public decimal? Quota { get; set; }

    [JsonProperty("rate")]
    public decimal? Rate { get; set; }
}

public class OkxPublicInterestRateVip
{
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }
}

public class OkxPublicInterestRateRegular
{
    [JsonProperty("irDiscount")]
    public decimal? InterestRateDiscount { get; set; }

    [JsonProperty("loanQuotaCoef")]
    public decimal? LoanQuotaCoef { get; set; }

    [JsonProperty("level")]
    public string Level { get; set; }
}
