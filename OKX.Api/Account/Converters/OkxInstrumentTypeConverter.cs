﻿using OKX.Api.Account.Enums;

namespace OKX.Api.Account.Converters;

internal class OkxInstrumentTypeConverter : BaseConverter<OkxInstrumentType>
{
    public OkxInstrumentTypeConverter() : this(true) { }
    public OkxInstrumentTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxInstrumentType, string>> Mapping => new List<KeyValuePair<OkxInstrumentType, string>>
    {
        new(OkxInstrumentType.Any, "ANY"),
        new(OkxInstrumentType.Spot, "SPOT"),
        new(OkxInstrumentType.Margin, "MARGIN"),
        new(OkxInstrumentType.Swap, "SWAP"),
        new(OkxInstrumentType.Futures, "FUTURES"),
        new(OkxInstrumentType.Option, "OPTION"),
        new(OkxInstrumentType.Contracts, "CONTRACTS"),
    };
}