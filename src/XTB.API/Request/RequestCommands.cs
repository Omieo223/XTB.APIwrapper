using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public static class RequestCommands
    {
        public const string Login = "login";
        public const string GetMarginLevel = "getMarginLevel";
        public const string GetAllSymbols = "getAllSymbols";
        public const string GetCalendar = "getCalendar";
        public const string GetCurrentUserData = "getCurrentUserData";
        public const string GetServerTime = "getServerTime";
        //public const string GetIbsHistory = "getIbsHistory"; nieaktywne
        public const string GetMarginTrade = "getMarginTrade";


    }
}
