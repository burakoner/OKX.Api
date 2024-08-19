using OKX.Api.Financial.OnChainEarn.Enums;

namespace OKX.Api.Financial.OnChainEarn.Converters;

internal class OkxFinancialOnChainEarnOfferStateConverter(bool quotes) : BaseConverter<OkxFinancialOnChainEarnOfferState>(quotes)
{
    public OkxFinancialOnChainEarnOfferStateConverter() : this(true) { }

    protected override List<KeyValuePair<OkxFinancialOnChainEarnOfferState, string>> Mapping =>
    [
        new(OkxFinancialOnChainEarnOfferState.Purchasable, "purchasable"),
        new(OkxFinancialOnChainEarnOfferState.SoldOut, "sold_out"),
        new(OkxFinancialOnChainEarnOfferState.Stop, "Stop"),
        new(OkxFinancialOnChainEarnOfferState.Stop, "stop"),
    ];
}