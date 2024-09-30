namespace OKX.Api.Grid;

/// <summary>
/// OKX Grid Algo Order Type
/// </summary>
public enum OkxGridAlgoOrderType
{
    /// <summary>
    /// SpotGrid
    /// </summary>
    [Map("grid")]
    SpotGrid = 1,

    /// <summary>
    /// ContractGrid
    /// </summary>
    [Map("contract_grid")]
    ContractGrid = 2,
}