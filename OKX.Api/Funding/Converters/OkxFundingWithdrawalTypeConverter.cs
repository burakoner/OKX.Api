﻿using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxFundingWithdrawalTypeConverter(bool quotes) : BaseConverter<OkxFundingWithdrawalType>(quotes)
{
    public OkxFundingWithdrawalTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingWithdrawalType, string>> Mapping =>
    [
        new(OkxFundingWithdrawalType.InternalDeposit, "3"),
        new(OkxFundingWithdrawalType.BlockchainDeposit, "4"),
    ];
}