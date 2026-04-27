namespace OKX.Api.Dca;

/// <summary>
/// OKX DCA Algo Order Type
/// </summary>
public enum OkxDcaAlgoOrderType : byte
{
    /// <summary>
    /// Contract DCA order
    /// </summary>
    [Map("contract_dca")]
    ContractDca = 1,

    /// <summary>
    /// Spot DCA order
    /// </summary>
    [Map("spot_dca")]
    SpotDca = 2,
}
