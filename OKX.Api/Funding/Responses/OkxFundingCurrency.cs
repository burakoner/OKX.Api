namespace OKX.Api.Funding;

/// <summary>
/// OKX Currency
/// </summary>
public record OkxFundingCurrency
{
    /// <summary>
    /// Currency, e.g. BTC
    /// </summary>
    [JsonProperty("ccy")]
    public string Currency { get; set; } = string.Empty;

    /// <summary>
    /// Name of currency. There is no related name when it is not shown.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The logo link of currency
    /// </summary>
    [JsonProperty("logoLink")]
    public string LogoLink { get; set; } = string.Empty;

    /// <summary>
    /// Chain name, e.g. USDT-ERC20, USDT-TRC20
    /// </summary>
    [JsonProperty("chain")]
    public string Chain { get; set; } = string.Empty;

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
    /// Estimated opening time for deposit, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("depEstOpenTime")]
    public long? DepositEstimatedOpenTimestamp { get; set; }

    /// <summary>
    /// Estimated opening time for deposit
    /// </summary>
    [JsonIgnore]
    public DateTime? DepositEstimatedOpenTime => DepositEstimatedOpenTimestamp?.ConvertFromMilliseconds();

    /// <summary>
    /// Estimated opening time for withdraw, Unix timestamp format in milliseconds, e.g. 1597026383085
    /// </summary>
    [JsonProperty("wdEstOpenTime")]
    public long? WithdrawalEstimatedOpenTimestamp { get; set; }

    /// <summary>
    /// Estimated opening time for withdraw
    /// </summary>
    [JsonIgnore]
    public DateTime? WithdrawalEstimatedOpenTime => WithdrawalEstimatedOpenTimestamp?.ConvertFromMilliseconds();

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
    /// The minimum internal transfer amount of currency in a single transaction
    /// No maximum internal transfer limit in a single transaction, subject to the withdrawal limit in the past 24 hours(wdQuota).
    /// </summary>
    [JsonProperty("minInternal")]
    public decimal MinimumInternalAmount { get; set; }

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
    /// The fixed withdrawal fee
    /// Apply to on-chain withdrawal
    /// </summary>
    [JsonProperty("fee")]
    public decimal? Fee { get; set; }

    /// <summary>
    /// The minimum withdrawal fee for normal address
    /// </summary>
    [Obsolete]
    [JsonProperty("minFee")]
    public decimal MinimumWithdrawalFeeForNormalAddress { get; set; }

    /// <summary>
    /// The maximum withdrawal fee for normal address
    /// </summary>
    [Obsolete]
    [JsonProperty("maxFee")]
    public decimal MaximumWithdrawalFeeForNormalAddress { get; set; }

    /// <summary>
    /// The minimum withdrawal fee for contract address
    /// </summary>
    [Obsolete]
    [JsonProperty("minFeeForCtAddr")]
    public decimal? MinimumWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// The maximum withdrawal fee for contract address
    /// </summary>
    [Obsolete]
    [JsonProperty("maxFeeForCtAddr")]
    public decimal? MaximumWithdrawalFeeForContractAddress { get; set; }

    /// <summary>
    /// Burning fee rate, e.g "0.05" represents "5%".
    /// Some currencies may charge combustion fees.The burning fee is deducted based on the withdrawal quantity (excluding gas fee) multiplied by the burning fee rate.
    /// Apply to on-chain withdrawal
    /// </summary>
    [JsonProperty("burningFeeRate")]
    public decimal? BurningFeeRate { get; set; }

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
