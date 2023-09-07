﻿namespace OKX.Api.Models.Rubik;

public class OkxSupportCoins
{
    [JsonProperty("contract")]
    public IEnumerable<string> Contract { get; set; }

    [JsonProperty("option")]
    public IEnumerable<string> Option { get; set; }

    [JsonProperty("spot")]
    public IEnumerable<string> Spot { get; set; }
}
