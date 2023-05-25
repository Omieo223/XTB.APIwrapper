using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingStation.Api.XTB.Request
{
    public class LoginParameters
    {
        public string userId { get; private set; }
        public string password { get; private set; }

        public LoginParameters(string userId, string password)
        {
            this.userId = userId;
            this.password = password;
        }
    }
}
