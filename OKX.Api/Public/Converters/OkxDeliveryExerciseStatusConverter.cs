using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

public class OkxDeliveryExerciseStatusConverter(bool quotes) : BaseConverter<OkxDeliveryExerciseStatus>(quotes)
{
    public OkxDeliveryExerciseStatusConverter() : this(true) { }

    protected override List<KeyValuePair<OkxDeliveryExerciseStatus, string>> Mapping =>
    [
        new(OkxDeliveryExerciseStatus.Delivery, "delivery"),
        new(OkxDeliveryExerciseStatus.Exercised, "exercised"),
        new(OkxDeliveryExerciseStatus.ExpiredOtm, "expired_otm"),
    ];
}