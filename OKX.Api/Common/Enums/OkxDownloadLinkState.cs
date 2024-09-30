namespace OKX.Api.Common;

/// <summary>
/// OKX Download Link State
/// </summary>
public enum OkxDownloadLinkState
{
    /// <summary>
    /// Ongoing
    /// </summary>
    [Map("ongoing")]
    Ongoing = 1,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finished")]
    Finished = 2,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed = 3,
}