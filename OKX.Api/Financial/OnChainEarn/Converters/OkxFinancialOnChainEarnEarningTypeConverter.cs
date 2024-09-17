﻿using OKX.Api.Financial.OnChainEarn.Enums;

namespace OKX.Api.Financial.OnChainEarn.Converters;

internal class OkxFinancialOnChainEarnEarningTypeConverter(bool quotes) : BaseConverter<OkxFinancialOnChainEarnEarningType>(quotes)
{
    public OkxFinancialOnChainEarnEarningTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFinancialOnChainEarnEarningType, string>> Mapping =>
    [
        new(OkxFinancialOnChainEarnEarningType.EstimatedEarning, "0"),
        new(OkxFinancialOnChainEarnEarningType.CumulativeEarning, "1"),
    ];
}