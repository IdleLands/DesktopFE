using IdleLandsGUI.Model;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleLandsGUI
{
    public class IdleLandsComms
    {
        private const String _programIdentifier = "IdleLandsGui#";
        private Stopwatch _timeSinceLastTurn { get; set; }
        private String _username { get; set; }
        //Bad O_o
        private String _password { get; set; }
        private String _token { get; set; }
        private bool _loggedIn { get; set; }
        private bool _hasAdvancedLogin { get; set; }
        private string _advancedIdentifier { get; set; }
        private string _serverAddress { get; set; }

        public IdleLandsComms()
        {
            _playerUpdateDelegates = new List<PlayerUpdate>();
            _timeSinceLastTurn = new Stopwatch();
            _loggedIn = false;
        }

        //Public functions, mostly Async

        public void SetServer(string server)
        {
            _serverAddress = server;
        }

        public async void Register(String username, String password, IdleLandsGUI.LoginForm.LoginResultDelegate success, IdleLandsGUI.LoginForm.LoginFailedDelegate failure)
        {
            _username = username;
            _password = password;
            var request = new RestRequest("player/auth/register", Method.PUT);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("name", _username);
            request.AddParameter("password", _password);

            IRestResponse<LoginResponse> response = null;
            try
            {
                response = await GetClient().ExecuteTaskAsync<LoginResponse>(request);
            }
            catch (WebException we)
            {
                failure(we.Message);
                return;
            }
            catch(Exception e)
            {
                failure("");
                return;
            }

            if (response.Data != null)
            {
                _token = response.Data.token;
                _loggedIn = response.Data.Success();
                if (_loggedIn)
                    success(response.Data.player);
                else
                    failure(response.Data.code + ": " + response.Data.message);
            }
        }

        public async void Login(String username, String password, IdleLandsGUI.LoginForm.LoginResultDelegate success, IdleLandsGUI.LoginForm.LoginFailedDelegate failure)
        {
            _username = username;
            _password = password;
            _hasAdvancedLogin = false;
            var request = new RestRequest("/player/auth/login", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("password", _password);
            request.AddParameter("name", username);

            IRestResponse<LoginResponse> response = null;
            try
            {
                response = await GetClient().ExecuteTaskAsync<LoginResponse>(request);
            }
            catch (WebException we)
            {
                failure(we.Message);
                return;
            }

            if (response.Data != null)
            {
                _token = response.Data.token;
                _loggedIn = response.Data.Success();
                if (_loggedIn)
                    success(response.Data.player);
                else
                    failure(response.Data.code + ": " + response.Data.message);
            }
        }

        public async void AdvancedLogin(String usernameWithIdent, String password, IdleLandsGUI.LoginForm.LoginResultDelegate success, IdleLandsGUI.LoginForm.LoginFailedDelegate failure)
        {
            if(usernameWithIdent.IndexOf('#') == -1)
            {
                failure("Username has to contain the '#' sign.");
                return;
            }

            _username = usernameWithIdent.Substring(usernameWithIdent.IndexOf('#')-1);
            _password = password;
            _advancedIdentifier = usernameWithIdent;
            _hasAdvancedLogin = true;
            var request = new RestRequest("/player/auth/login", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("password", _password);
            request.AddParameter("name", _username);

            IRestResponse<LoginResponse> response = null;
            try
            {
                response = await GetClient().ExecuteTaskAsync<LoginResponse>(request);
            }
            catch (WebException we)
            {
                failure(we.Message);
                return;
            }

            if (response.Data != null)
            {
                _token = response.Data.token;
                _loggedIn = response.Data.Success();
                if (_loggedIn && success != null)
                    success(response.Data.player);
                else if(!_loggedIn && failure != null)
                    failure(response.Data.code + ": " + response.Data.message);
            }
            else
            {
                failure("Couldn't login due to an unknown reason.");
            }
        }

        public async void Logout(Func<bool> doOnComplete)
        {
            _loggedIn = false;

            var request = new RestRequest("/player/auth/logout", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", _token);

            var response = await GetClient().ExecuteTaskAsync<LoginResponse>(request);

            doOnComplete();
        }

        public async void InventoryAdd(string slot, Func<bool> doOnComplete, Func<string, string, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/inventory/add", Method.PUT);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", _token);
            request.AddParameter("itemSlot", slot);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            if (EnsureLoggedIn(response, () =>
                {
                    InventoryAdd(slot, doOnComplete, doOnFailure);
                    return true;
                }))
            {
                return;
            }

            if (response.Data != null && response.Data.player != null)
            {
                SendPlayerUpdate(response.Data.player);
            }
            else if(response.Data != null && !response.Data.Success())
            {
                doOnFailure(response.Data.message, response.Data.code);
            }

            if (doOnComplete != null)
                doOnComplete();
        }

        public async void InventorySell(string slot, Func<bool> doOnComplete, Func<string, string, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/inventory/sell", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", _token);
            request.AddParameter("invSlot", slot);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            System.IO.File.AppendAllText(@"responses.txt", "\r\n\r\n!!!SELL!!!\r\n\r\n" + response.Content);

            if (EnsureLoggedIn(response, () =>
            {
                InventorySell(slot, doOnComplete, doOnFailure);
                return true;
            }))
            {
                return;
            }

            if (response.Data != null && response.Data.player != null)
            {
                SendPlayerUpdate(response.Data.player);
            }
            else if (response.Data != null && !response.Data.Success())
            {
                doOnFailure(response.Data.message, response.Data.code);
            }

            if (doOnComplete != null)
                doOnComplete();
        }

        public async void InventorySwap(string slot, Func<bool> doOnComplete, Func<string, string, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/inventory/swap", Method.PATCH);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", _token);
            request.AddParameter("invSlot", slot);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            System.IO.File.AppendAllText(@"responses.txt", "\r\n\r\n!!!SWAP!!!\r\n\r\n" + response.Content);

            if (EnsureLoggedIn(response, () =>
            {
                InventorySwap(slot, doOnComplete, doOnFailure);
                return true;
            }))
            {
                return;
            }

            if (response.Data != null && response.Data.player != null)
            {
                SendPlayerUpdate(response.Data.player);
            }
            else if (response.Data != null && !response.Data.Success())
            {
                doOnFailure(response.Data.message, response.Data.code);
            }

            if (doOnComplete != null)
                doOnComplete();
        }

        public async void SendTurn()
        {
            var request = new RestRequest("/player/action/turn", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", _token);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            System.IO.File.AppendAllText(@"responses.txt", "\r\n\r\n!!!ACTION!!!\r\n\r\n" + response.Content);

            if (response.StatusCode == HttpStatusCode.OK && response.Data == null)
                return;

            if (EnsureLoggedIn(response, null))
            {
                return;
            }
            
            if (response.Data != null && response.Data.player != null)
            {
                SendPlayerUpdate(response.Data.player);
            }
        }

        public async void SendGender(string gender, Func<bool> doOnComplete)
        {
            var request = new RestRequest("/player/manage/gender/set", Method.PUT);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("gender", gender);
            request.AddParameter("token", _token);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            if (EnsureLoggedIn(response, () =>
            {
                SendGender(gender, doOnComplete);
                return true;
            }))
            {
                return;
            }

            doOnComplete();
        }

        public async void SendPriorityPoints(PriorityPointsInfo priorityPoints)
        {
            var request = new RestRequest("/player/manage/priority/set", Method.PUT);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                stats = new
                {
                    priorityPoints.str,
                    priorityPoints.dex,
                    priorityPoints.con,
                    priorityPoints.agi,
                    @int = priorityPoints._int,
                    priorityPoints.wis
                },
                identifier = GetToken(),
                token = _token
            });

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            if (EnsureLoggedIn(response, () =>
            {
                SendPriorityPoints(priorityPoints);
                return true;
            }))
            {
                return;
            }
        }

        public void DoTick(object sender, EventArgs e)
        {
            if (!_loggedIn)
                return;

            if (!_timeSinceLastTurn.IsRunning)
                _timeSinceLastTurn.Start();

            if(_timeSinceLastTurn.ElapsedMilliseconds > 10100)
            {
                SendTurn();
                _timeSinceLastTurn.Reset();
            }
        }

        private bool EnsureLoggedIn<T>(IRestResponse<T> response, Func<bool> onSuccess) where T : BaseResponse
        {
            if (response.Data == null)
            {
                throw new Exception("This is bad?");
            }
            if (response.Data.code == "-1" || response.Data.code == "10")
            {
                AdvancedLogin(GetToken(), _password, info =>
                {
                    if(onSuccess != null)
                        onSuccess();
                }, info =>
                {
                    MessageBox.Show("Fuck, crashing program with code " + response.Data.code +
                        ": " + response.Data.message + " - " + info);
                    Application.Exit();
                });
                return true;
            }
            return false;
        }

        private RestClient GetClient()
        {
            var client = new RestClient(_serverAddress);

            client.Timeout = 20000;
            //client.AddDefaultHeader("Connection", "close");

            return client;
        }

        public String GetToken()
        {
            if(!_hasAdvancedLogin)
                return _programIdentifier + _username;
            return _advancedIdentifier;
        }

        //Response definitions

        public class BaseResponse
        {
            public string isSuccess { get; set; }
            public string code { get; set; }
            public string message { get; set; }

            public bool Success()
            {
                return isSuccess == "True";
            }
        }

        public class LoginResponse : BaseResponse
        {
            public PlayerInfo player { get; set; }
            public string token { get; set; }
        }

        public class ActionResponse : BaseResponse
        {
            public PlayerInfo player { get; set; }
        }

        
        //Delegate definitions
        public delegate void PlayerUpdate(PlayerInfo player);

        //Actual Delegates
        private List<PlayerUpdate> _playerUpdateDelegates { get; set; }
        
        private void SendPlayerUpdate(PlayerInfo info)
        {
            foreach(var dele in _playerUpdateDelegates)
            {
                dele(info);
            }
        }

        public void AddPlayerUpdateDelegate(PlayerUpdate updateDelegate)
        {
            _playerUpdateDelegates.Add(updateDelegate);
        }

        public void RemovePlayerUpdateDelegate(PlayerUpdate updateDelegate)
        {
            _playerUpdateDelegates.Remove(updateDelegate);
        }
    }
}
