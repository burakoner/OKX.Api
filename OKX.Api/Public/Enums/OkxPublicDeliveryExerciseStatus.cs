namespace OKX.Api.Public;

/// <summary>
/// OKX Delivery Exercise History Type
/// </summary>
public enum OkxPublicDeliveryExerciseStatus : byte
{
    /// <summary>
    /// Delivery
    /// </summary>
    [Map("delivery")]
    Delivery = 1,

    /// <summary>
    /// Exercised
    /// </summary>
    [Map("exercised")]
    Exercised = 2,

    /// <summary>
    /// ExpiredOtm
    /// </summary>
    [Map("expired_otm")]
    ExpiredOtm = 3,
}