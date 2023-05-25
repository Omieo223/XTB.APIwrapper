using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class CurrentUserData
    {
        public int companyUnit { get; set; }
        public string currency { get; set; }
        public string group { get; set; }
        public bool ibAccount { get; set; }
        //public int leverage { get; set; }
        public float leverageMultiplier { get; set; }
        public string spreadType { get; set; }
        public bool trailingStop { get; set; }
    }
}
