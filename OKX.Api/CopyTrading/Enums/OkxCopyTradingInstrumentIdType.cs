namespace OKX.Api.CopyTrading;

/// <summary>
/// OKX CopyTrading Instrument Id Type
/// </summary>
public enum OkxCopyTradingInstrumentIdType
{
    /// <summary>
    /// custom: custom by instId which is required；
    /// </summary>
    Custom,

    /// <summary>
    /// copy: Keep your contracts consistent with this trader by automatically adding or removing contracts when they do
    /// </summary>
    Copy,
}