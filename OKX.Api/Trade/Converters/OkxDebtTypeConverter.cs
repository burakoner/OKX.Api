using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

internal class OkxDebtTypeConverter(bool quotes) : BaseConverter<OkxDebtType>(quotes)
{
    public OkxDebtTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxDebtType, string>> Mapping =>
    [
        new(OkxDebtType.Cross, "cross"),
        new(OkxDebtType.Isolated, "isolated"),
    ];
}