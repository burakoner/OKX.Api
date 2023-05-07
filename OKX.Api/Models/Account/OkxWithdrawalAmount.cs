namespace OKX.Api.Models.Account;

public class OkxWithdrawalAmount
{
    [JsonProperty("ccy")]
    public string Currency { get; set; }

    [JsonProperty("maxWd")]
    public decimal? MaximumWithdrawalExcludingBorrowedAssets { get; set; }

    [JsonProperty("maxWdEx")]
    public decimal? MaximumWithdrawalIncludingBorrowedAssets { get; set; }

    [JsonProperty("spotOffsetMaxWd")]
    public decimal? SpotMaximumWithdrawalExcludingBorrowedAssets { get; set; }

    [JsonProperty("spotOffsetMaxWdEx")]
    public decimal? SpotMaximumWithdrawalIncludingBorrowedAssets { get; set; }
}
