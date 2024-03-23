namespace OKX.Api.Funding.Models;

public class OkxNonTradableAssetBalance
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("logoLink")]
    public string LogoLink { get; set; }

    [JsonProperty("bal")]
    public decimal Available { get; set; }

    [JsonProperty("canWd")]
    public bool Withdrawable  { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }

    [JsonProperty("wdAll")]
    public bool WithdrawAtOnce { get; set; }
    
    [JsonProperty("fee")]
    public decimal Fee { get; set; }
    
    [JsonProperty("ctAddr")]
    public string ContractAddress { get; set; }
    
    [JsonProperty("wdTickSz")]
    public decimal WithdrawTickSize { get; set; }
    
    [JsonProperty("needTag")]
    public bool NeedTag { get; set; }
}
