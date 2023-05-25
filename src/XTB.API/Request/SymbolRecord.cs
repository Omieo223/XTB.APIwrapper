using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class SymbolRecord
    {
        public float ask { get; set; }
        public float bid { get; set; }
        public string categoryName { get; set; }
        public int contractSize { get; set; }
        public string currency { get; set; }
        public bool currencyPair { get; set; }
        public string currencyProfit { get; set; }
        public string description { get; set; }
        public DateTime? expiration { get; set; }
        public string groupName { get; set; }
        public float high { get; set; }
        public int initialMargin { get; set; }
        public int instantMaxVolume { get; set; }
        public float leverage { get; set; }
        public bool longOnly { get; set; }
        public float lotMax { get; set; }
        public float lotMin { get; set; }
        public float lotStep { get; set; }
        public float low { get; set; }
        public int marginHedged { get; set; }
        public bool marginHedgedStrong { get; set; }
        public int marginMaintenance { get; set; }
        public int marginMode { get; set; }
        public float percentage { get; set; }
        public int pipsPrecision { get; set; }
        public int precision { get; set; }
        public int profitMode { get; set; }
        public int quoteId { get; set; }
        public bool shortSelling { get; set; }
        public float spreadRaw { get; set; }
        public float spreadTable { get; set; }
        public DateTime? starting { get; set; }
        public int stepRuleId { get; set; }
        public int stopsLevel { get; set; }
        public int swap_rollover3days { get; set; }
        public bool swapEnable { get; set; }
        public float swapLong { get; set; }
        public float swapShort { get; set; }
        public int swapType { get; set; }
        public string symbol { get; set; }
        public float tickSize { get; set; }
        public float tickValue { get; set; }
        public long time { get; set; }
        public string timeString { get; set; }
        public bool trailingEnabled { get; set; }
        public int type { get; set; }





    }
}
