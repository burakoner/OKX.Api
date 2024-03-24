using OKX.Api.GridTrading.Enums;

namespace OKX.Api.GridTrading.Converters;

internal class OkxGridContractDirectionConverter(bool quotes) : BaseConverter<OkxGridContractDirection>(quotes)
{
    public OkxGridContractDirectionConverter() : this(true) { }

    protected override List<KeyValuePair<OkxGridContractDirection, string>> Mapping =>
    [
        new(OkxGridContractDirection.Long, "long"),
        new(OkxGridContractDirection.Short, "short"),
        new(OkxGridContractDirection.Neutral, "neutral"),
    ];
}