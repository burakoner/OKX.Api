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
    Ongoing,

    /// <summary>
    /// Finished
    /// </summary>
    [Map("finished")]
    Finished,

    /// <summary>
    /// Failed
    /// </summary>
    [Map("failed")]
    Failed,
}