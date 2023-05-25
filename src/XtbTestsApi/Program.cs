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

        public Dictionary<Guid, string> GuidTags = new();

        public async Task MainAsync()
        {
            #region  Tworzenie nowego zapytania (requestu)

            RequestParametersDto request_Login = new RequestParametersDto(); // nowe wystapienie klasy  RequstParametersDtodeklaracja zmiennej lokalnej o nazwie login o typie RequstParametersDto
            var wczytywanieLog = wczytywanieLoginuZplikuJson(); // wywołanie metody
            // przypisanie do zmiennej warotści zwracanej przez metodę
            request_Login.arguments = wczytywanieLog;  // przypisanei wartości do zmiennej arguments
            request_Login.command = RequestCommands.Login;

            #region sposob 1 na nadanie taga
            var naszGudi = GetRandomGuid(); // wywołąnie metody, przypisanie do zmiennej warotści zwracanej przez metodę
            request_Login.customTag = naszGudi.ToString();
            #endregion

            #region sposob 2 na nadanie taga
            //NadajZapytaniuCustomTagIdodajDoSlownika(request_Login);
            #endregion

            #endregion

            #region dodanie do slownka klucza z komendą
            GuidTags.Add(naszGudi, RequestCommands.Login); // dodawnie do słownika aby móc odczytac dane??
            #endregion

            #region seriazlizacja komedny
            string loginRequestJson = System.Text.Json.JsonSerializer.Serialize(request_Login);
            #endregion

            await InitAsync(loginRequestJson);
        }

        private async Task<bool> SendRequestAsync(WebSocket ws, RequestParametersDto request) 
        {
            if (ws.State != WebSocketState.Open)
                return false;

            try
            {
                JsonSerializerOptions opts = new() // ustawienie opcji serializatora aby pomijał pola null
                {
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };
                var requestJson = JsonSerializer.Serialize(request, opts); 
                var bytes = Encoding.UTF8.GetBytes(requestJson);

                await ws.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                return false;
            }
            return true;

        }
        public void NadajZapytaniuCustomTagIdodajDoSlownika(RequestParametersDto request)
        {
            //tworzenie zmiennej typu guid zeby byla naszym tagiem
            var newGuid = GetRandomGuid();
            string naszCustomtag = newGuid.ToString();

            request.customTag = naszCustomtag;
            GuidTags.Add(newGuid, request.command);
        }
        private async Task InitAsync(string loginRequestJson)
        {
            #region Enkodowanie
            var bytes = Encoding.UTF8.GetBytes(loginRequestJson);
            #endregion

            ClientWebSocket klient = new();

            Uri uri = new Uri(Server.ServerDemo);

            try
            {

                await klient.ConnectAsync(uri, CancellationToken.None);
                if (klient.State == WebSocketState.Open)
                {
                    _ws = klient;
                    _ = nasluchiwanieWebsocetu(klient); //metoda dziala w tle

                    #region wyslie zapytania przez WS
                    await klient.SendAsync(bytes, WebSocketMessageType.Text, true, CancellationToken.None);
                    #endregion
                }

            }
            catch (Exception ex)
            {

            }
            while (true) ;

        }
        private WebSocket? _ws = null;
        public async Task nasluchiwanieWebsocetu(ClientWebSocket klient)

        {
            var buffor = new byte[10000000];

            while (true)
            {
                #region Oczekiwanie na odpowiedz oraz otrzymanie odpowiedzi

                var result = await klient.ReceiveAsync(new ArraySegment<byte>(buffor), CancellationToken.None);

                #endregion

                #region Dekodowanie wiadomości i deserializacja
                var response = Encoding.UTF8.GetString(buffor, 0, result.Count);

                try
                {

                    var respo = System.Text.Json.JsonSerializer.Deserialize<ResponseMessage>(response);
                    #endregion

                    if (respo is IResponseMessage { } response2)
                        await ResponseProcessing(response2);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private async Task ResponseProcessing(IResponseMessage iMessage)
        {
            //var re1 = iMessage as ResponseMessage;
            //var re2 = (ResponseMessage)iMessage; 

            if (iMessage is ResponseMessage respo)
            {
                await PrzetwarzanieWiadomosciAsync(respo);
            }
        }
        public async Task PrzetwarzanieWiadomosciAsync(ResponseMessage response)
        {
            #region Sprawdzanie czy odpowiedz posiada nasz tag (guid)
            if (Guid.TryParse(response.customTag, out var guid))
            {

                var guidFound = GuidTags.TryGetValue(guid, out string? command);//odczytujemy command

                if (guidFound)
                {
                    #endregion
                    //robimy cos tam, prztewarzamy dane
                    _ = GuidTags.Remove(guid, out _); //wywalamy ze slownika

                    #region Odczytywanie zawartosci odpowiedzi i w zaleznosci od komendy wykonujemy dzialania
                    switch (command)
                    {

                        case RequestCommands.Login:
                            if (_ws is null)
                                return;

                            RequestParametersDto getMarginRequest = new RequestParametersDto()
                            {
                                command = RequestCommands.GetMarginLevel
                            };

                            NadajZapytaniuCustomTagIdodajDoSlownika(getMarginRequest);

                            await SendRequestAsync(_ws, getMarginRequest);
                            break;

                        case RequestCommands.GetMarginLevel:
                            
                            if (response.returnData is null)
                                return;
                            
                                var returnData = response.returnData.ToString();
                                var margin = System.Text.Json.JsonSerializer.Deserialize<MarginDto>(returnData);


                            //RequestParametersDto getAllSymbol = new RequestParametersDto()
                            //{
                            //    command = RequestCommands.GetAllSymbols
                            //};
                            //NadajZapytaniuCustomTagIdodajDoSlownika(getAllSymbol);
                            //await SendRequestAsync(_ws, getAllSymbol);


                            //RequestParametersDto getCalendar = new RequestParametersDto()
                            //{
                            //    command = RequestCommands.GetCalendar
                            //};
                            //NadajZapytaniuCustomTagIdodajDoSlownika(getCalendar);
                            //await SendRequestAsync(_ws, getCalendar);


                            //RequestParametersDto getCurrentUserData = new RequestParametersDto()
                            //{
                            //    command = RequestCommands.GetCurrentUserData
                            //};
                            //NadajZapytaniuCustomTagIdodajDoSlownika(getCurrentUserData);
                            //await SendRequestAsync(_ws, getCurrentUserData);


                            //RequestParametersDto getServerTime = new RequestParametersDto()
                            //{
                            //    command = RequestCommands.GetServerTime
                            //};
                            //NadajZapytaniuCustomTagIdodajDoSlownika(getServerTime);
                            //await SendRequestAsync(_ws, getServerTime);


                            // KOMENDA NIE UZYwANA

                            //HistoryParameters hisParam = new HistoryParameters(1685009532, 1684836732); // unix time
                            //RequestParametersDto getIbsHistory = new RequestParametersDto()
                            //{
                            //    command = RequestCommands.GetIbsHistory,
                            //    arguments = hisParam
                            //    };
                            //    NadajZapytaniuCustomTagIdodajDoSlownika(getIbsHistory);
                            //    await SendRequestAsync(_ws, getIbsHistory);

                            MarginTradeArg argMarginTrade = new MarginTradeArg("EURPLN", 1); ;
                            RequestParametersDto getMarginTrade = new RequestParametersDto()
                            {
                                command = RequestCommands.GetMarginTrade,
                                arguments = argMarginTrade
                            };
                            NadajZapytaniuCustomTagIdodajDoSlownika(getMarginTrade);
                            await SendRequestAsync(_ws, getMarginTrade);

                            break;


                        case RequestCommands.GetAllSymbols:
                            ;
                            if (response.returnData is null)
                                return;

                            try
                            {
                                var returnData1 = response.returnData.ToString();
                                var symbol = System.Text.Json.JsonSerializer.Deserialize<SymbolRecord[]>(returnData1);
                            }
                            catch (Exception ex)
                            {

                            }
                            break;


                        case RequestCommands.GetCalendar:

                            if (response.returnData is null)
                                return;

                            try
                            {
                                var returnData2 = response.returnData.ToString();
                                var calendary = System.Text.Json.JsonSerializer.Deserialize<Calendary[]>(returnData2);
                            }
                            catch (Exception ex)
                            {

                            }
                            break;

                        case RequestCommands.GetCurrentUserData:
                            if (response.returnData is null) return;
                            try
                            {
                                var returnData3 = response.returnData.ToString();
                                var currentUserData = System.Text.Json.JsonSerializer.Deserialize<CurrentUserData>(returnData3);
                            }
                            catch (Exception ex)
                            {

                            }
                            break;

                        case RequestCommands.GetServerTime:
                            if (response.returnData is null) return;
                            try
                            {
                                var returnData4 = response.returnData.ToString();
                                var serverTime = System.Text.Json.JsonSerializer.Deserialize<ServerTime>(returnData4);
                            }
                            catch (Exception ex)
                            {

                            }
                            break;
                        //case RequestCommands.GetIbsHistory:
                        //    if (response.returnData is null) return;
                        //    try
                        //    {
                        //        var returnData5 = response.returnData.ToString();
                        //        var getIbsHistory = System.Text.Json.JsonSerializer.Deserialize<IB_record[]>(returnData5);
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //    }
                        //    break;
                        case RequestCommands.GetMarginTrade:
                            if (response.returnData is null) return;
                            try
                            {
                                var returnData6 = response.returnData.ToString();
                                var MarginTrade = System.Text.Json.JsonSerializer.Deserialize<MargineTradeParameters>(returnData6);
                            }
                            catch (Exception ex)
                            {

                            }
                            break;
                    }
                    #endregion

                }
                else
                {

                }
            }

        }
        public static Guid GetRandomGuid()
        {
            return Guid.NewGuid();
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