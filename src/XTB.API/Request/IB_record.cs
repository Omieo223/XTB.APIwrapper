using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace TradingStation.Api.XTB.Request
{
    public class IB_record
    {
        public float closePrice {get; set;}
        public string login { get; set;}
        public float nominal { get; set;}
        public float openPrice { get; set;}
        public int side { get; set;}
        public string surname { get; set;}
        public string symbol { get; set;}
        public float timestamp { get; set;}
        public float volume { get; set;}


    }
}
