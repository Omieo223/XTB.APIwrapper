using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB
{
    public enum ServerType
    {
        Real,
        Demo
    }
    public static class Server
    {
        public static string ServerDemo = "wss://ws.xtb.com/demo";
        public static string ServerDemoStream = "wss://ws.xtb.com/demoStream";
        public static string ServerReal = "wss://ws.xtb.com/real";
        public static string ServerRealStream = "wss://ws.xtb.com/realStream";

        public static int PortDemo = 5124;
        public static int PortDemoStream = 5125;
        public static int PortReal = 5112;
        public static int PortRealStream = 5113;
    }
}
