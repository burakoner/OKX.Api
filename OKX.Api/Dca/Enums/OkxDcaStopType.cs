namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Stop Type
/// </summary>
public enum OkxDcaStopType : byte
{
    /// <summary>
    /// Contract DCA: market close all positions
    /// Spot DCA: sell base currency
    /// </summary>
    [Map("1")]
    CloseOrSell = 1,

    /// <summary>
    /// Contract DCA: keep positions
    /// Spot DCA: keep base currency
    /// </summary>
    [Map("2")]
    Keep = 2,
}
