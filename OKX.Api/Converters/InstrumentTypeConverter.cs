namespace OKX.Api.Converters;

internal class InstrumentTypeConverter : BaseConverter<OkxInstrumentType>
{
    public InstrumentTypeConverter() : this(true) { }
    public InstrumentTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxInstrumentType, string>> Mapping => new List<KeyValuePair<OkxInstrumentType, string>>
    {
        new KeyValuePair<OkxInstrumentType, string>(OkxInstrumentType.Any, "ANY"),
        new KeyValuePair<OkxInstrumentType, string>(OkxInstrumentType.Spot, "SPOT"),
        new KeyValuePair<OkxInstrumentType, string>(OkxInstrumentType.Margin, "MARGIN"),
        new KeyValuePair<OkxInstrumentType, string>(OkxInstrumentType.Swap, "SWAP"),
        new KeyValuePair<OkxInstrumentType, string>(OkxInstrumentType.Futures, "FUTURES"),
        new KeyValuePair<OkxInstrumentType, string>(OkxInstrumentType.Option, "OPTION"),
        new KeyValuePair<OkxInstrumentType, string>(OkxInstrumentType.Contracts, "CONTRACTS"),
    };
}