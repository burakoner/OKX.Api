using OKX.Api.SubAccount.Enums;

namespace OKX.Api.SubAccount.Converters;

internal class OkxSubAccountTransferTypeConverter : BaseConverter<OkxSubAccountTransferType>
{
    public OkxSubAccountTransferTypeConverter() : this(true) { }
    public OkxSubAccountTransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSubAccountTransferType, string>> Mapping =>
    [
        new(OkxSubAccountTransferType.FromMasterAccountToSubAccout, "0"),
        new(OkxSubAccountTransferType.FromSubAccountToMasterAccout, "1"),
    ];
}