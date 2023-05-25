using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class ResponseMessage : IResponseMessage
    {
        public bool status { get; set; }
        public string? streamSessionId { get; set; }

        public object ? returnData { get; set; }

        public string? customTag { get; set; }
       
    }
    public interface IResponseMessage
    {

    }
}
