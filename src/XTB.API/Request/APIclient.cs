using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace TradingStation.Api.XTB.Request
{
    public class APIclient
    {
        private Dictionary<Guid, string> GuidTags = new();

        private ClientWebSocket _ws = new ClientWebSocket();
        ClientWebSocket streamingWs = new();
        public Uri uri;
        public string _sesionID = "";

        public async Task LogInsync(LoginParameters loginParameters) 
        {
            await _ws.ConnectAsync(uri, CancellationToken.None);
            ListeningAsync(_ws,CancellationToken.None);
            await SendLoginRequest(loginParameters);
        } 
        private async Task ListeningAsync(WebSocket webSocket, CancellationToken cancellationToken)
        {
            Byte[] buffor = new byte[1024000];
            while (!cancellationToken.IsCancellationRequested)//czy token został anulowany
            {
                WebSocketReceiveResult nazwa = await webSocket.ReceiveAsync(buffor, cancellationToken);
                DekodowanieDeserialiacja(buffor, nazwa.Count);
            }
        }
        public void DekodowanieDeserialiacja(byte[] buffor, int nazwa2)
        {
            var response = Encoding.UTF8.GetString(buffor, 0, nazwa2);
            var message = System.Text.Json.JsonSerializer.Deserialize<ResponseMessage>(response);

            ProcessingRespo(message);
        }
        public async Task ProcessingRespo(ResponseMessage response)
        {
            if (Guid.TryParse(response.customTag, out var guid))
            {
                var guidFound = GuidTags.TryGetValue(guid, out string? command);
                if (guidFound)
                {
                    _ = GuidTags.Remove(guid, out _);



                    switch (command)
                    {
                        case RequestCommands.Login:
                            if (response.status == true)
                            {
                                var sessionId = response.streamSessionId;

                                if (string.IsNullOrWhiteSpace(sessionId))
                                {

                                    new Exception("statusId = false");

                                }
                                _= SendGetMarginLevelRequest();
                                _sesionID = sessionId;

                                SendGetTradesHistoryRequest(
                                    new DateTime(2024, 08, 15),
                                    new DateTime(2022, 06, 14));
                                SendGetTradesRequest(true);
                            }
                            break;
                        case RequestCommands.GetMarginLevel:
                            ;
                            break;
                        case RequestCommands.GetAllSymbols:
                            break;
                        case RequestCommands.GetCalendar:
                            break;
                        case RequestCommands.GetCurrentUserData:
                            break;
                        case RequestCommands.GetServerTime:
                            break;
                        case RequestCommands.GetMarginTrade:
                            break;
                        case RequestCommands.GetTradesHistory:
                            break;
                        case RequestCommands.GetTrades:
                            ;
                            break;
                    }
                }
            }
        }
        #region Nadanie zapytania CustomTag i dodanie do słownika
        public void NadajZapytaniuCustomTagIdodajDoSlownika(RequestParametersDto request)
        {

            var newGuid = GetRandomGuid();
            string naszCustomtag = newGuid.ToString();

            request.customTag = naszCustomtag;
            GuidTags.Add(newGuid, request.command);
        }
        public static Guid GetRandomGuid()
        {
            return Guid.NewGuid();
        }
        #endregion
        public APIclient(ServerType serwer)
        {
            switch (serwer)
            {
                case ServerType.Real:
                    Uri real = new Uri(Server.ServerReal);
                    uri = real;
                    break;

                case ServerType.Demo:
                    Uri demo = new Uri(Server.ServerDemo);
                    uri = demo;
                    break;
            }
        }
        public bool YouLogIn()
        {
            if (_ws.State == WebSocketState.Open)
            {
                if (string.IsNullOrWhiteSpace(_sesionID))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public async Task SendRequest(RequestParametersDto request)
        {
            NadajZapytaniuCustomTagIdodajDoSlownika(request);
            try
            {
                JsonSerializerOptions opts = new()
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };
                var requestJson = JsonSerializer.Serialize(request, opts);
                var bytes = Encoding.UTF8.GetBytes(requestJson);

                await _ws.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
            }
        }
        private RequestParametersDto NewRequest(string command, object arguments)
         => new() { command = command, arguments = arguments };
        public async Task SendLoginRequest(LoginParameters parameters)
            => await SendRequest(NewRequest(RequestCommands.Login,parameters));

        public async Task SendGetMarginLevelRequstTime()
        {
            
            while (true)
            {
                await Task.Delay(1000);
                await SendGetMarginLevelRequest();  
            }
        }
        public async Task SendGetAllSymbolsRequest()
        {
            RequestParametersDto request = new() { command = RequestCommands.GetAllSymbols };
            await SendRequest(request);
        }

        private RequestParametersDto NewRequest(string command)
           => new() { command = command };

        public async Task SendGetMarginLevelRequest()
            => await SendRequest(NewRequest(RequestCommands.GetMarginLevel));
        public async Task SendGetCalendarRequest()
            => await SendRequest(NewRequest(RequestCommands.GetCalendar));

        public async Task SendGetCurrentUserDataRequest() 
            => await SendRequest(NewRequest(RequestCommands.GetCurrentUserData));
        
        public async Task SendGetServerTimeRequest()
            => await SendRequest(NewRequest(RequestCommands.GetServerTime));

        public async Task SendGetMarginTradeRequest() 
            => await SendRequest(NewRequest(RequestCommands.GetMarginTrade));
       
        public async Task SendGetTradesHistoryRequest(DateTime end, DateTime start)
        {

            var hisparam = new HistoryParameters(
                D1ateTimeToUnixMilliseconds(end),
                D1ateTimeToUnixMilliseconds(start));

            //var hisparam = new HistoryParameters(
            //    0,
            //    0);

            RequestParametersDto getHisory = new RequestParametersDto()
            {
                command = RequestCommands.GetTradesHistory,
                arguments = hisparam,
            };

            await SendRequest(getHisory);
        }

        public long D1ateTimeToUnixMilliseconds(DateTime hisparam)
        {

            DateTimeOffset date = new DateTimeOffset(hisparam.ToUniversalTime());
            return date.ToUnixTimeMilliseconds();

        }
        public async Task SendGetTradesRequest(bool openedOnly)
        {

            var openedOnly1 = new GetTradesArg();


            RequestParametersDto getTrades = new RequestParametersDto()
            {
                command = RequestCommands.GetTrades,
                arguments = openedOnly1,
            };

            await SendRequest(getTrades);
        }


    }

}
