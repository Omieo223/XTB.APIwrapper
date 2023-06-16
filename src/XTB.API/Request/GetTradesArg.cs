using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class GetTradesArg
    {

        public bool openedOnly { get; set; }

        public GetTradesArg()
        {
            this.openedOnly = true;
        }
    }
    
    
}
