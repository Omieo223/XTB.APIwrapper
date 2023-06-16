using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TradingStation.Api.XTB.Request
{
    public class HistoryParameters
    {

        public long end { get; private set; }
        public long start { get; private set; }

        public HistoryParameters(long end, long start)
        {
            this.end = end;
            this.start = start;
        }
       
    }
}
