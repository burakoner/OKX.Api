namespace OKX.Api.Public;

/// <summary>
/// Event contract hit direction.
/// </summary>
public enum OkxPublicEventContractHitDirection : byte
{
    /// <summary>
    /// Price hit the strike from below.
    /// </summary>
    [Map("up")]
    Up = 1,

    /// <summary>
    /// Price hit the strike from above.
    /// </summary>
    [Map("dn")]
    Down = 2,
}
