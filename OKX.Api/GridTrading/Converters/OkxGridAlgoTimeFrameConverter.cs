using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridAlgoTimeFrameConverter : BaseConverter<OkxGridAlgoTimeFrame>
{
    public OkxGridAlgoTimeFrameConverter() : this(true) { }
    public OkxGridAlgoTimeFrameConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTimeFrame, string>> Mapping =>
    [
        new(OkxGridAlgoTimeFrame.ThreeMinutes, "3m"),
        new(OkxGridAlgoTimeFrame.FiveMinutes, "5m"),
        new(OkxGridAlgoTimeFrame.FifteenMinutes, "15m"),
        new(OkxGridAlgoTimeFrame.ThirtyMinutes, "30m"),
        new(OkxGridAlgoTimeFrame.OneHour, "1H"),
        new(OkxGridAlgoTimeFrame.FourHours, "4H"),
        new(OkxGridAlgoTimeFrame.OneDay, "1D"),
    ];
}