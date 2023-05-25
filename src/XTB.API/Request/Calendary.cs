using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class Calendary
    {
        public string country { get; set; }
        public string current { get; set; }
        public string forecast { get; set; }
        public string impact { get; set; }
        public string period { get; set; }
        public string previous { get; set; }
        public float time { get; set; }
        public string title { get; set; }


    }
}
