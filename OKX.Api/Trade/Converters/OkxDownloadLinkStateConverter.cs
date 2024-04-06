using OKX.Api.Trade.Enums;

namespace OKX.Api.Trade.Converters;

public class OkxDownloadLinkStateConverter(bool quotes) : BaseConverter<OkxDownloadLinkState>(quotes)
{
    public OkxDownloadLinkStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxDownloadLinkState, string>> Mapping =>
    [
        new(OkxDownloadLinkState.Ongoing, "ongoing"),
        new(OkxDownloadLinkState.Finished, "finished"),
    ];
}