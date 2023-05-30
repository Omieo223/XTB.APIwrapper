using Microsoft.Win32.SafeHandles;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using TradingStation.Api.XTB;
using TradingStation.Api.XTB.Request;

namespace XtbTestsApi
{
    internal class Program
    {
        static void Main(string[] args)
         => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            #region  Tworzenie nowego zapytania (requestu)

            RequestParametersDto request_Login = new RequestParametersDto();
            var wczytywanieLog = wczytywanieLoginuZplikuJson(); 
            request_Login.arguments = wczytywanieLog;  
            request_Login.command = RequestCommands.Login;

            APIclient nowka = new APIclient(ServerType.Demo);
            await nowka.LogInsync(wczytywanieLog);
            while (true) ;
        }
        public static LoginParameters wczytywanieLoginuZplikuJson()
        {
            string LoginParameters = "Login.secrets.json";

            var basepath = AppDomain.CurrentDomain.BaseDirectory;

            var filepath = Path.Combine(basepath, LoginParameters);

            var logJon = File.ReadAllText(filepath);

            LoginParameters text = System.Text.Json.JsonSerializer.Deserialize<LoginParameters>(logJon);
            return text;
        }


    }
}