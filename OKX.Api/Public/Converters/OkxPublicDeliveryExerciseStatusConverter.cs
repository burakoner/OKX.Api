namespace OKX.Api.Public;

internal class OkxPublicDeliveryExerciseStatusConverter(bool quotes) : BaseConverter<OkxPublicDeliveryExerciseStatus>(quotes)
{
    public OkxPublicDeliveryExerciseStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxPublicDeliveryExerciseStatus, string>> Mapping =>
    [
        new(OkxPublicDeliveryExerciseStatus.Delivery, "delivery"),
        new(OkxPublicDeliveryExerciseStatus.Exercised, "exercised"),
        new(OkxPublicDeliveryExerciseStatus.ExpiredOtm, "expired_otm"),
    ];
}