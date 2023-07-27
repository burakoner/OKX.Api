namespace OKX.Api.Models.Funding;

/// <summary>
/// OKX Currency
/// </summary>
public class OkxCurrency
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    /// <summary>
    /// Name of currency. There is no related name when it is not shown.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// The logo link of currency
    /// </summary>
    [JsonProperty("logoLink")]
    public string LogoLink { get; set; }

    /// <summary>
    /// Chain name, e.g. USDT-ERC20, USDT-TRC20
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; }

    /// <summary>
    /// The availability to deposit from chain.
    /// </summary>
    [JsonProperty("canDep")]
    public bool AllowDeposit { get; set; }

    /// <summary>
    /// The availability to withdraw to chain.
    /// </summary>
    [JsonProperty("canWd")]
    public bool AllowWithdrawal { get; set; }

    /// <summary>
    /// The availability to internal transfer.
    /// </summary>
    [JsonProperty("canInternal")]
    public bool AllowInternalTransfer { get; set; }

    /// <summary>
    /// The minimum deposit amount of the currency in a single transaction
    /// </summary>
    [JsonProperty("minDep")]
    public decimal MinimumDepositAmount { get; set; }

    /// <summary>
    /// The minimum withdrawal amount of the currency in a single transaction
    /// </summary>
    [JsonProperty("minWd")]
    public decimal MinimumWithdrawalAmount { get; set; }

    /// <summary>
    /// The maximum amount of currency withdrawal in a single transaction
    /// </summary>
    [JsonProperty("maxWd")]
    public decimal MaximumWithdrawalAmount { get; set; }

    /// <summary>
    /// The withdrawal precision, indicating the number of digits after the decimal point.
    /// The withdrawal fee precision kept the same as withdrawal precision.
    /// The accuracy of internal transfer withdrawal is 8 decimal places.
    /// </summary>
    [JsonProperty("wdTickSz")]
    public decimal WithdrawalTickSize { get; set; }

    /// <summary>
    /// The withdrawal limit in the past 24 hours, unit in USD
    /// </summary>
    [JsonProperty("wdQuota")]
    public decimal WithdrawalQuota { get; set; }

    /// <summary>
    /// The amount of currency withdrawal used in the past 24 hours, unit in USD
    /// </summary>
    [JsonProperty("usedWdQuota")]
    public decimal? UsedWithdrawalQuota { get; set; }

    /// <summary>
    /// The minimum withdrawal fee for normal address
    /// </summary>
    [JsonProperty("minFee")]
    public decimal MinimumWithdrawalFeeForNormalAddress { get; set; }

    /// <summary>
    /// The maximum withdrawal fee for normal address
    /// </summary>
    [JsonProperty("maxFee")]
    public decimal MaximumWithdrawalFeeForNormalAddress { get; set; }

    /// <summary>
    /// The minimum withdrawal fee for contract address
    /// </summary>
    [JsonProperty("minFeeForCtAddr")]
    public decimal? MinimumWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// The maximum withdrawal fee for contract address
    /// </summary>
    [JsonProperty("maxFeeForCtAddr")]
    public decimal? MaximumWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// If current chain is main net then return true, otherwise return false
    /// </summary>
    [JsonProperty("mainNet")]
    public bool MainNet { get; set; }

    /// <summary>
    /// Whether tag/memo information is required for withdrawal
    /// </summary>
    [JsonProperty("needTag")]
    public bool NeedTag { get; set; }

    /// <summary>
    /// The minimum number of blockchain confirmations to acknowledge fund deposit.
    /// The account is credited after that, but the deposit can not be withdrawn
    /// </summary>
    [JsonProperty("minDepArrivalConfirm")]
    public int MinimumDepositArrivalConfirms { get; set; }

    /// <summary>
    /// The minimum number of blockchain confirmations required for withdrawal of a deposit
    /// </summary>
    [JsonProperty("minWdUnlockConfirm")]
    public int MinimumWithdrawalUnlockConfirms { get; set; }

    /// <summary>
    /// The fixed deposit limit, unit in USD
    /// Return empty string if there is no deposit limit
    /// </summary>
    [JsonProperty("depQuotaFixed")]
    public decimal? FixedDepositLimit { get; set; }

    /// <summary>
    /// The used amount of fixed deposit quota, unit in USD
    /// Return empty string if there is no deposit limit
    /// </summary>
    [JsonProperty("usedDepQuotaFixed")]
    public decimal? UsedFixedDepositLimit { get; set; }

    /// <summary>
    /// The layer2 network daily deposit limit
    /// </summary>
    [JsonProperty("depQuoteDailyLayer2")]
    public decimal? DepositQuoteDailyLayer2 { get; set; }
}
