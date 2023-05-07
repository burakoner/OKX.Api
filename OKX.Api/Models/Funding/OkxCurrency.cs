namespace OKX.Api.Models.Funding;

public class OkxCurrency
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
    
    [JsonProperty("logoLink")]
    public string LogoLink { get; set; }

    [JsonProperty("chain")]
    public string Chain { get; set; }

    [JsonProperty("canDep")]
    public bool AllowDeposit { get; set; }

    [JsonProperty("canWd")]
    public bool AllowWithdrawal { get; set; }

    [JsonProperty("canInternal")]
    public bool AllowInternalTransfer { get; set; }

    [JsonProperty("minDep")]
    public decimal MinimumDepositAmount { get; set; }

    [JsonProperty("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }
    
    [JsonProperty("maxWd")]
    public decimal MaximumWithdrawalAmount { get; set; }
    
    [JsonProperty("wdTickSz")]
    public decimal WithdrawalTickSize { get; set; }
    
    [JsonProperty("wdQuota")]
    public decimal WithdrawalQuota { get; set; }
    
    [JsonProperty("usedWdQuota")]
    public decimal? UsedWithdrawalQuota { get; set; }

    [JsonProperty("minFee")]
    public decimal MinimumWithdrawalFee { get; set; }

    [JsonProperty("maxFee")]
    public decimal MaximumWithdrawalFee { get; set; }

    [JsonProperty("mainNet")]
    public bool MainNet { get; set; }

    [JsonProperty("needTag")]
    public bool NeedTag { get; set; }

    [JsonProperty("minDepArrivalConfirm")]
    public int MinimumDepositArrivalConfirms { get; set; }

    [JsonProperty("minWdUnlockConfirm")]
    public int MinimumWithdrawalUnlockConfirms { get; set; }

    [JsonProperty("depQuotaFixed")]
    public decimal? FixedDepositLimit { get; set; }
    
    [JsonProperty("usedDepQuotaFixed")]
    public decimal? UsedFixedDepositLimit { get; set; }
    
    [JsonProperty("depQuoteDailyLayer2")]
    public decimal? DepositQuoteDailyLayer2 { get; set; }
}
