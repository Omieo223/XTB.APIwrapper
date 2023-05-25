using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class MarginTradeArg
    {

        public string symbol { get; private set; }
        public float volume { get; private set; }

        public MarginTradeArg(string symbol, float volume)
        {
            this.symbol = symbol;
            this.volume = volume;
        }
    }
}
