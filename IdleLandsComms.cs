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
        private const String programIdentifier = "IdleLandsGui#";
        private Stopwatch timeSinceLastTurn { get; set; }
        private RestClient Client { get; set; }
        private String Username { get; set; }
        //Bad O_o
        private String Password { get; set; }
        private String Token { get; set; }
        private bool LoggedIn { get; set; }
        private bool HasAdvancedLogin { get; set; }
        private string AdvancedIdentifier { get; set; }

        public IdleLandsComms()
        {
            playerUpdateDelegates = new List<PlayerUpdate>();
            timeSinceLastTurn = new Stopwatch();
            //Client = new RestClient("https://api.idle.land");
            Client = new RestClient("http://127.0.0.1");
            Client.Timeout = 20000;
            LoggedIn = false;
        }

        //Public functions, mostly Async

        public async void Register(String username, String password, IdleLandsGUI.LoginForm.LoginResultDelegate success, IdleLandsGUI.LoginForm.LoginFailedDelegate failure)
        {
            Username = username;
            Password = password;
            var request = new RestRequest("player/auth/register", Method.PUT);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("name", Username);
            request.AddParameter("password", Password);

            IRestResponse<LoginResponse> response = null;
            try
            {
                response = await Client.ExecuteTaskAsync<LoginResponse>(request);
            }
            catch (WebException we)
            {
                failure(we.Message);
                return;
            }

            if (response.Data != null)
            {
                Token = response.Data.token;
                LoggedIn = response.Data.Success();
                if (LoggedIn)
                    success(response.Data.player);
                else
                    failure(response.Data.code + ": " + response.Data.message);
            }
        }

        public async void Login(String username, String password, IdleLandsGUI.LoginForm.LoginResultDelegate success, IdleLandsGUI.LoginForm.LoginFailedDelegate failure)
        {
            Username = username;
            Password = password;
            HasAdvancedLogin = false;
            var request = new RestRequest("/player/auth/login", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("password", Password);
            request.AddParameter("name", username);

            IRestResponse<LoginResponse> response = null;
            try
            {
                response = await Client.ExecuteTaskAsync<LoginResponse>(request);
            }
            catch (WebException we)
            {
                failure(we.Message);
                return;
            }

            if (response.Data != null)
            {
                Token = response.Data.token;
                LoggedIn = response.Data.Success();
                if (LoggedIn)
                    success(response.Data.player);
                else
                    failure(response.Data.code + ": " + response.Data.message);
            }
        }

        public async void AdvancedLogin(String usernameWithIdent, String password, IdleLandsGUI.LoginForm.LoginResultDelegate success, IdleLandsGUI.LoginForm.LoginFailedDelegate failure)
        {
            Username = usernameWithIdent.Substring(usernameWithIdent.IndexOf('#'));
            Password = password;
            AdvancedIdentifier = usernameWithIdent;
            HasAdvancedLogin = true;
            var request = new RestRequest("/player/auth/login", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("password", Password);
            request.AddParameter("name", Username);

            IRestResponse<LoginResponse> response = null;
            try
            {
                response = await Client.ExecuteTaskAsync<LoginResponse>(request);
            }
            catch (WebException we)
            {
                failure(we.Message);
                return;
            }

            if (response.Data != null)
            {
                Token = response.Data.token;
                LoggedIn = response.Data.Success();
                if (LoggedIn && success != null)
                    success(response.Data.player);
                else if(!LoggedIn && failure != null)
                    failure(response.Data.code + ": " + response.Data.message);
            }
        }

        public async void Logout()
        {
            LoggedIn = false;

            var request = new RestRequest("/player/auth/logout", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", Token);

            var response = await Client.ExecuteTaskAsync<LoginResponse>(request);
        }

        public async void SendTurn()
        {
            var request = new RestRequest("/player/action/turn", Method.POST);
            request.AddParameter("identifier", GetToken());
            request.AddParameter("token", Token);

            var response = await Client.ExecuteTaskAsync<ActionResponse>(request);

            if (response.StatusCode == HttpStatusCode.OK && response.Data == null)
                return;

            if (!response.Data.Success())
            {
                if (response.Data.code == "-1" || response.Data.code == "10")
                    AdvancedLogin(GetToken(), Password, null, info => { MessageBox.Show("Fuck, crashing program with code " + response.Data.code +
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
            if (!LoggedIn)
                return;

            if (!timeSinceLastTurn.IsRunning)
                timeSinceLastTurn.Start();

            if(timeSinceLastTurn.ElapsedMilliseconds > 10100)
            {
                SendTurn();
                timeSinceLastTurn.Reset();
            }
        }

        public String GetToken()
        {
            if(!HasAdvancedLogin)
                return programIdentifier + Username;
            return AdvancedIdentifier;
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
        private List<PlayerUpdate> playerUpdateDelegates { get; set; }
        
        private void SendPlayerUpdate(PlayerInfo info)
        {
            foreach(var dele in playerUpdateDelegates)
            {
                dele(info);
            }
        }

        public void AddPlayerUpdateDelegate(PlayerUpdate updateDelegate)
        {
            playerUpdateDelegates.Add(updateDelegate);
        }

        public void RemovePlayerUpdateDelegate(PlayerUpdate updateDelegate)
        {
            playerUpdateDelegates.Remove(updateDelegate);
        }
    }
}
