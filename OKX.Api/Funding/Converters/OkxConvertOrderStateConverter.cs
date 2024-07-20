using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxConvertOrderStateConverter(bool quotes) : BaseConverter<OkxConvertOrderState>(quotes)
{
    public OkxConvertOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxConvertOrderState, string>> Mapping =>
    [
        new(OkxConvertOrderState.Success, "fullyFilled"),
        new(OkxConvertOrderState.Failed, "rejected"),
    ];
}