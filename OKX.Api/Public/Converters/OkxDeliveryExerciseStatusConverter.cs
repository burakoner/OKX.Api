using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxDeliveryExerciseStatusConverter : BaseConverter<OkxDeliveryExerciseStatus>
{
    public OkxDeliveryExerciseStatusConverter() : this(true) { }
    public OkxDeliveryExerciseStatusConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxDeliveryExerciseStatus, string>> Mapping =>
    [
        new(OkxDeliveryExerciseStatus.Delivery, "delivery"),
        new(OkxDeliveryExerciseStatus.Exercised, "exercised"),
        new(OkxDeliveryExerciseStatus.ExpiredOtm, "expired_otm"),
    ];
}