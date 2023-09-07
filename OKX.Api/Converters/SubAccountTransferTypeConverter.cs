namespace OKX.Api.Converters;

internal class SubAccountTransferTypeConverter : BaseConverter<OkxSubAccountTransferType>
{
    public SubAccountTransferTypeConverter() : this(true) { }
    public SubAccountTransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxSubAccountTransferType, string>> Mapping => new List<KeyValuePair<OkxSubAccountTransferType, string>>
    {
        new KeyValuePair<OkxSubAccountTransferType, string>(OkxSubAccountTransferType.FromMasterAccountToSubAccout, "0s"),
        new KeyValuePair<OkxSubAccountTransferType, string>(OkxSubAccountTransferType.FromSubAccountToMasterAccout, "1"),
    };
}