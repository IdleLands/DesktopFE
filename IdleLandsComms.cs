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
        private RestClient _client { get; set; }
        private String _username { get; set; }
        //Bad O_o
        private String _password { get; set; }
        private String _token { get; set; }
        private bool _loggedIn { get; set; }
        private bool _hasAdvancedLogin { get; set; }
        private string _advancedIdentifier { get; set; }

        public IdleLandsComms()
        {
            _playerUpdateDelegates = new List<PlayerUpdate>();
            _timeSinceLastTurn = new Stopwatch();
            _loggedIn = false;
        }

        //Public functions, mostly Async

        public void SetServer(string server)
        {
            Uri uri = new Uri(server);
            _client = new RestClient(uri);
            
            _client.Timeout = 20000;
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
                response = await _client.ExecuteTaskAsync<LoginResponse>(request);
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
                response = await _client.ExecuteTaskAsync<LoginResponse>(request);
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
            _username = usernameWithIdent.Substring(usernameWithIdent.IndexOf('#'));
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
                response = await _client.ExecuteTaskAsync<LoginResponse>(request);
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
        }

        public async void Logout(Func<bool> doOnComplete)
        {
            _loggedIn = false;

            var request = new RestRequest("/player/auth/logout", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", _token);

            var response = await _client.ExecuteTaskAsync<LoginResponse>(request);

            doOnComplete();
        }

        public async void SendTurn()
        {
            var request = new RestRequest("/player/action/turn", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", _token);

            var response = await _client.ExecuteTaskAsync<ActionResponse>(request);

            if (response.StatusCode == HttpStatusCode.OK && response.Data == null)
                return;

            if (!response.Data.Success())
            {
                if (response.Data.code == "-1" || response.Data.code == "10")
                    AdvancedLogin(GetToken(), _password, null, info => { MessageBox.Show("Fuck, crashing program with code " + response.Data.code +
                        ": " + response.Data.message); Application.Exit(); });
                else if (response.Data.code != "100")
                {
                    MessageBox.Show("Problem taking turn. Code: " + response.Data.code + " message: " + response.Data.message);
                    throw new Exception("This is bad?");
                }

                return;
            }
            if (response.Data != null && response.Data.player != null)
            {
                SendPlayerUpdate(response.Data.player);
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
