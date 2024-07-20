using OKX.Api.Trading.Enums;

namespace OKX.Api.Trading.Converters;

internal class OkxDebtTypeConverter(bool quotes) : BaseConverter<OkxDebtType>(quotes)
{
    public OkxDebtTypeConverter() : this(true) { }

    protected override List<KeyValuePair<OkxDebtType, string>> Mapping =>
    [
        new(OkxDebtType.Isolated, "isolated"),
        new(OkxDebtType.Cross, "cross"),
    ];
}