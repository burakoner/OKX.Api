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
    Last,

    /// <summary>
    /// Index
    /// </summary>
    [Map("index")]
    Index,

    /// <summary>
    /// Mark
    /// </summary>
    [Map("mark")]
    Mark,
}