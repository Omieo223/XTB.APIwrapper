using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class BalanceFormat
    {
        public float commandSub { private get; set; }
        public float streamSessionId { private get;  set; }

        public float commandUnsub { private get;  set; }

        public float commandStream { private get;  set; }
        public float data { private get;  set; }

       
    }
}
