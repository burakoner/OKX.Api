using OKX.Api.Public.Enums;

namespace OKX.Api.Public.Converters;

internal class OkxDeliveryExerciseHistoryTypeConverter : BaseConverter<OkxDeliveryExerciseHistoryType>
{
    public OkxDeliveryExerciseHistoryTypeConverter() : this(true) { }
    public OkxDeliveryExerciseHistoryTypeConverter(bool quotes) : base(quotes) { }

    protected override List<KeyValuePair<OkxDeliveryExerciseHistoryType, string>> Mapping =>
    [
        new(OkxDeliveryExerciseHistoryType.Delivery, "delivery"),
        new(OkxDeliveryExerciseHistoryType.Exercised, "exercised"),
        new(OkxDeliveryExerciseHistoryType.ExpiredOtm, "expired_otm"),
    ];
}