using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class RequestParametersDto
    {
        public string command { get; set; }
        public object arguments { get; set; }
        public string streamSesionId { get; set; }
        public string? customTag { get; set; }
    }
}
