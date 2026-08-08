namespace OKX.Api.Account;

/// <summary>
/// GLP enrollment status.
/// </summary>
public enum OkxAccountGlpEnrollmentStatus : byte
{
    /// <summary>
    /// Enrolled in the program.
    /// </summary>
    [Map("ENROLLED")]
    Enrolled = 1,
}
