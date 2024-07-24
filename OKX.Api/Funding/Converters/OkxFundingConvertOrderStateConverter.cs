using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxFundingConvertOrderStateConverter(bool quotes) : BaseConverter<OkxFundingConvertOrderState>(quotes)
{
    public OkxFundingConvertOrderStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFundingConvertOrderState, string>> Mapping =>
    [
        new(OkxFundingConvertOrderState.Success, "fullyFilled"),
        new(OkxFundingConvertOrderState.Failed, "rejected"),
    ];
}