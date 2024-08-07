﻿using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxFundingWithdrawalDestinationConverter(bool quotes) : BaseConverter<OkxFundingWithdrawalDestination>(quotes)
{
    public OkxFundingWithdrawalDestinationConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingWithdrawalDestination, string>> Mapping =>
    [
        new(OkxFundingWithdrawalDestination.OKX, "3"),
        new(OkxFundingWithdrawalDestination.DigitalCurrencyAddress, "4"),
    ];
}