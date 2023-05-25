using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class MarginDto
    {
#nullable disable
        public float balance { get; set; }
        public string currency { get; set; }
        public float equity { get; set; }
        public float margin { get; set; }
        public float margin_free { get; set; }
        public float margin_level { get; set; }
#nullable enable		
    }
}
