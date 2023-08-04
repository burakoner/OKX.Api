using System;
using System.Collections.Generic;
using System.Text;

namespace OKX.Api.Models.MarketData;
public class OkxCandlestickBase
{
    public string InstrumentId { get; set; }
    public string InstId { get; set; }
    public OkxCandlestick Data { get; set; }
}
