using OKX.Api.GridTrade.Enums;

namespace OKX.Api.GridTrade.Converters;

internal class GridAlgoTimeFrameConverter : BaseConverter<OkxGridAlgoTimeFrame>
{
    public GridAlgoTimeFrameConverter() : this(true) { }
    public GridAlgoTimeFrameConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxGridAlgoTimeFrame, string>> Mapping => new List<KeyValuePair<OkxGridAlgoTimeFrame, string>>
    {
        new KeyValuePair<OkxGridAlgoTimeFrame, string>(OkxGridAlgoTimeFrame.ThreeMinutes, "3m"),
        new KeyValuePair<OkxGridAlgoTimeFrame, string>(OkxGridAlgoTimeFrame.FiveMinutes, "5m"),
        new KeyValuePair<OkxGridAlgoTimeFrame, string>(OkxGridAlgoTimeFrame.FifteenMinutes, "15m"),
        new KeyValuePair<OkxGridAlgoTimeFrame, string>(OkxGridAlgoTimeFrame.ThirtyMinutes, "30m"),
        new KeyValuePair<OkxGridAlgoTimeFrame, string>(OkxGridAlgoTimeFrame.OneHour, "1H"),
        new KeyValuePair<OkxGridAlgoTimeFrame, string>(OkxGridAlgoTimeFrame.FourHours, "4H"),
        new KeyValuePair<OkxGridAlgoTimeFrame, string>(OkxGridAlgoTimeFrame.OneDay, "1D"),
    };
}