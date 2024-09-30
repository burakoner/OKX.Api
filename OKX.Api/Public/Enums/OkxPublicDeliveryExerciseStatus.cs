namespace OKX.Api.Public;

/// <summary>
/// OKX Delivery Exercise History Type
/// </summary>
public enum OkxPublicDeliveryExerciseStatus
{
    /// <summary>
    /// Delivery
    /// </summary>
    [Map("delivery")]
    Delivery,

    /// <summary>
    /// Exercised
    /// </summary>
    [Map("exercised")]
    Exercised,

    /// <summary>
    /// ExpiredOtm
    /// </summary>
    [Map("expired_otm")]
    ExpiredOtm,
}