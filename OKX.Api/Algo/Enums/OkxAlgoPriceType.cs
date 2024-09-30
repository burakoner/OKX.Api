namespace OKX.Api.Algo;

/// <summary>
/// OKX Algo Price Type
/// </summary>
public enum OkxAlgoPriceType
{
    /// <summary>
    /// Last
    /// </summary>
    [Map("last")]
    Last = 1,

    /// <summary>
    /// Index
    /// </summary>
    [Map("index")]
    Index = 2,

    /// <summary>
    /// Mark
    /// </summary>
    [Map("mark")]
    Mark = 3,
}