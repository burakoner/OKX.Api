﻿using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxFundingSavingActionSideConverter(bool quotes) : BaseConverter<OkxFundingSavingActionSide>(quotes)
{
    public OkxFundingSavingActionSideConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingSavingActionSide, string>> Mapping =>
    [
        new(OkxFundingSavingActionSide.Purchase, "purchase"),
        new(OkxFundingSavingActionSide.Redempt, "redempt"),
    ];
}