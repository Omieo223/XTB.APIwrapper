using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class Trade_record
    {
        public float close_price { get; set; }
        public float close_time { get; set; }
        public string? close_timeString { get; set; }
        public bool closed { get; set; }
        public int cmd { get; set; }
        public string? comment { get; set; }
        public float commission { get; set; }
        public string? customComment { get; set; }
        public int digits { get; set; }
        public float expiration { get; set; }
        public string? expirationString { get; set; }
        public float margin_rate { get; set; }
        public int offset { get; set; }
        public float open_price { get; set; }
        public float open_time { get; set; }
        public string? open_timeString { get; set; }
        public int order { get; set; }
        public int order2 { get; set; }
        public int position { get; set; }
        public float profit { get; set; }
        public float sl { get; set; }
        public float storage { get; set; }
        public string? symbol { get; set; }
        public float timestamp { get; set; }
        public float tp { get; set; }
        public float volume { get; set; }
    }
}
