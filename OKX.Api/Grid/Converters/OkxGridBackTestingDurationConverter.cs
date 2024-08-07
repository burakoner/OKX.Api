﻿using OKX.Api.Grid.Enums;

namespace OKX.Api.Grid.Converters;

internal class OkxGridBackTestingDurationConverter(bool quotes) : BaseConverter<OkxGridBackTestingDuration>(quotes)
{
    public OkxGridBackTestingDurationConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridBackTestingDuration, string>> Mapping =>
    [
        new(OkxGridBackTestingDuration.OneWeek, "7D"),
        new(OkxGridBackTestingDuration.OneMonth, "30D"),
        new(OkxGridBackTestingDuration.SixMonths, "180D"),
    ];
}