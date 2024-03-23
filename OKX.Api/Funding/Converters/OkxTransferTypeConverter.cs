using OKX.Api.Funding.Enums;

namespace OKX.Api.Funding.Converters;

internal class OkxTransferTypeConverter : BaseConverter<OkxTransferType>
{
    public OkxTransferTypeConverter() : this(true) { }
    public OkxTransferTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxTransferType, string>> Mapping =>
    [
        new(OkxTransferType.TransferWithinAccount, "0"),
        new(OkxTransferType.MasterAccountToSubAccount, "1"),
        new(OkxTransferType.SubAccountToMasterAccount_MasterApiKey, "2"),
        new(OkxTransferType.SubAccountToMasterAccount_SubAccountApiKey, "3"),
        new(OkxTransferType.SubAccountToSubAccount, "4"),
    ];
}