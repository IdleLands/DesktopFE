using IdleLandsGUI.Model;
using IdleLandsGUI.Model.Guilds;
using IdleLandsGUI.Model.Pets;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
        private bool _logResponses { get; set; }
        private bool _logRequests { get; set; }

        public IdleLandsComms()
        {
            _playerUpdateDelegates = new List<PlayerUpdate>();
            _petUpdateDelegates = new List<PetUpdate>();
            _guildUpdateDelegates = new List<GuildUpdate>();
            _timeSinceLastTurn = new Stopwatch();
            _loggedIn = false;
            AppSettings apps = new AppSettings();
            _logResponses = apps.LogResponses;
            _logRequests = apps.LogRequests;
        }

        //Public functions, mostly Async

        public void SetServer(string server)
        {
            _serverAddress = server;
        }

        public void SetAppSettings(AppSettings apps)
        {
            _logResponses = apps.LogResponses;
            _logRequests = apps.LogRequests;
        }

        // Main actions, exempt from CompleteRequest() except for SendTurn()

        public async void Register(String username, String password, IdleLandsGUI.LoginForm.LoginResultDelegate success, IdleLandsGUI.LoginForm.LoginFailedDelegate failure)
        {
            _username = username;
            _password = password;
            var request = new RestRequest("player/auth/register", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("name", _username);
            request.AddParameter("password", _password);

            LogRequest(request);

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

            LogResponse(response);

            if (response.Data != null)
            {
                _token = response.Data.token;
                _loggedIn = response.Data.Success();
                if (_loggedIn)
                    success(response.Data);
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
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("password", _password);
            request.AddParameter("name", username);

            LogRequest(request);

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

            LogResponse(response);

            if (response.Data != null)
            {
                _token = response.Data.token;
                _loggedIn = response.Data.Success();
                if (_loggedIn)
                    success(response.Data);
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
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("password", _password);
            request.AddParameter("name", _username);

            LogRequest(request);

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

            LogResponse(response);

            if (response.Data != null)
            {
                _token = response.Data.token;
                _loggedIn = response.Data.Success();
                if (_loggedIn && success != null)
                    success(response.Data);
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
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<LoginResponse>(request);

            LogResponse(response);

            doOnComplete();
        }

        public async void SendTurn()
        {
            var request = new RestRequest("/player/action/turn", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            LogResponse(response);

            if (response.StatusCode == HttpStatusCode.OK && response.Data == null)
                return;

            CompleteRequest(response, null, null, null);
        }

        //Inventory

        public async void InventoryAdd(string slot, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/inventory/add", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);
            request.AddParameter("itemSlot", slot);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                InventoryAdd(slot, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void InventorySell(string slot, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/inventory/sell", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);
            request.AddParameter("invSlot", slot);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                InventorySell(slot, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void InventorySwap(string slot, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/inventory/swap", Method.PATCH);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);
            request.AddParameter("invSlot", slot);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<ActionResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                InventorySwap(slot, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        //Options

        public async void SendGender(string gender, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/gender/set", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("gender", gender);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendGender(gender, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendPriorityPoints(PriorityPointsInfo priorityPoints, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
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
                identifier = GetIdentifier(),
                token = _token
            });

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendPriorityPoints(priorityPoints, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        //Guild stuff

        public async void SendCreateGuild(string guildName, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/create", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("guildName", guildName);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendCreateGuild(guildName, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendDisbandGuild(Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/disband", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendDisbandGuild(doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendLeaveGuild(Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/leave", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendLeaveGuild(doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendInvitePlayerGuild(string invName, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/invite/player", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("invName", invName);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendInvitePlayerGuild(invName, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendInviteManageGuild(bool accept, string guildName, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/invite/manage", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("accepted", accept);
            request.AddParameter("guildName", guildName);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendInviteManageGuild(accept, guildName, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendPromoteGuild(string memberName, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/invite/promote", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("memberName", memberName);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendPromoteGuild(memberName, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendDemoteGuild(string memberName, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/invite/demote", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("memberName", memberName);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendDemoteGuild(memberName, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendKickGuild(string memberName, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/invite/kick", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("memberName", memberName);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendKickGuild(memberName, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendSetTaxGuild(int taxPercent, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/guild/manage/tax", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("taxPercent", taxPercent);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendSetTaxGuild(taxPercent, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendSetTaxPlayer(int taxPercent, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/player/manage/tax", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("taxPercent", taxPercent);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<BaseResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendSetTaxPlayer(taxPercent, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendBuyPet(string type, string name, List<String> attrs, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/buy", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(new
            {
                identifier = GetIdentifier(),
                token = _token,
                name = name,
                type = type,
                attrs = attrs
            });

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendBuyPet(type, name, attrs, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendUpgradePet(string stat, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/upgrade", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("stat", stat);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
                {
                    SendUpgradePet(stat, doOnComplete, doOnFailure);
                    return true;
                }, doOnComplete, doOnFailure);
        }

        public async void SendFeedPet(Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/upgrade", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendFeedPet(doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendTakeGoldPet(Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/takeGold", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendTakeGoldPet(doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendSmartPet(string option, string value, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/smart", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("option", option);
            request.AddParameter("value", value);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendSmartPet(option, value, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendSwapPet(ulong petId, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/swap", Method.PATCH);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                identifier = GetIdentifier(),
                token = _token,
                petId = petId
            });

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendSwapPet(petId, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendChangeClassPet(string petClass, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/class", Method.PATCH);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("petClass", petClass);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendChangeClassPet(petClass, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendInventoryGivePet(int itemSlot, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/inventory/give", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("itemSlot", itemSlot);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendInventoryGivePet(itemSlot, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendInventoryTakePet(int itemSlot, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/inventory/take", Method.POST);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("itemSlot", itemSlot);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendInventoryTakePet(itemSlot, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendInventorySellPet(int itemSlot, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/inventory/sell", Method.PATCH);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("itemSlot", itemSlot);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendInventorySellPet(itemSlot, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendInventoryEquipPet(int itemSlot, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/inventory/equip", Method.PUT);
            request.AddParameter("identifier", GetIdentifier());
            request.AddParameter("itemSlot", itemSlot);
            request.AddParameter("token", _token);

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendInventoryEquipPet(itemSlot, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public async void SendInventoryUnequipPet(ulong itemUid, Func<bool> doOnComplete, Func<string, int, bool> doOnFailure)
        {
            var request = new RestRequest("/pet/inventory/unequip", Method.POST);
            request.RequestFormat = DataFormat.Json;

            request.AddBody(new
            {
                identifier = GetIdentifier(),
                token = _token,
                itemUid = itemUid
            });

            LogRequest(request);

            var response = await GetClient().ExecuteTaskAsync<PetResponse>(request);

            LogResponse(response);

            CompleteRequest(response, () =>
            {
                SendInventoryUnequipPet(itemUid, doOnComplete, doOnFailure);
                return true;
            }, doOnComplete, doOnFailure);
        }

        public void DoTick(object sender, EventArgs e)
        {
            if (!_loggedIn)
                return;

            if (!_timeSinceLastTurn.IsRunning)
                _timeSinceLastTurn.Start();

            if(_timeSinceLastTurn.ElapsedMilliseconds > 10200)
            {
                _timeSinceLastTurn.Reset();
                SendTurn();
            }
        }

        private void CompleteRequest<T>(IRestResponse<T> response, Func<bool> doOnRetryLogin,
            Func<bool> doOnComplete, Func<string, int, bool> doOnFailure) where T : BaseResponse
        {
            if (EnsureLoggedIn(response, doOnRetryLogin))
            {
                return;
            }

            if (response.Data != null && !response.Data.Success() && doOnFailure != null)
            {
                doOnFailure(response.Data.message, response.Data.code);
            }
            else if (response.Data == null && doOnFailure != null)
            {
                doOnFailure("Unknown error", -1);
            }

            IRestResponse<ActionResponse> actionResponse = response as IRestResponse<ActionResponse>;
            if (actionResponse != null && actionResponse.Data != null && actionResponse.Data.player != null)
            {
                SendPlayerUpdate(actionResponse.Data.player);
                PetResponse tempPetResponse = new PetResponse
                {
                    pet = actionResponse.Data.pet,
                    pets = actionResponse.Data.pets
                };
                SendPetUpdate(tempPetResponse);
                if(actionResponse.Data.guild != null)
                {
                    SendGuildUpdate(actionResponse.Data.guild);
                }
            }

            IRestResponse<PetResponse> petResponse = response as IRestResponse<PetResponse>;
            if (petResponse != null && petResponse.Data != null && petResponse.Data.pet != null)
            {
                SendPetUpdate(petResponse.Data);
            }

            if (doOnComplete != null)
                doOnComplete();
        }

        private void LogRequest(RestRequest request, [CallerMemberName]string memberName = "")
        {
            if (_logRequests)
                System.IO.File.AppendAllText("requests.txt", "\r\n\r\n!!!" + memberName + " @ " + DateTime.Now.ToString() + "!!!\r\n\r\n" + JsonConvert.SerializeObject(request));
        }

        private void LogResponse(IRestResponse response, [CallerMemberName]string memberName = "")
        {
            if (_logResponses)
                System.IO.File.AppendAllText("responses.txt", "\r\n\r\n!!!" + memberName + " @ " + DateTime.Now.ToString() + "!!!\r\n\r\n" + response.Content);
        }

        private bool EnsureLoggedIn<T>(IRestResponse<T> response, Func<bool> onSuccess) where T : BaseResponse
        {
            if (response.Data == null)
            {
                throw new Exception("This is bad?");
            }
            if (response.Data.code == -1 || response.Data.code == 10)
            {
                AdvancedLogin(GetIdentifier(), _password, info =>
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

        public String GetIdentifier()
        {
            if(!_hasAdvancedLogin)
                return _programIdentifier + _username;
            return _advancedIdentifier;
        }

        //Response definitions

        public class BaseResponse
        {
            public string isSuccess { get; set; }
            public int code { get; set; }
            public string message { get; set; }

            public bool Success()
            {
                return isSuccess == "True";
            }
        }

        public class LoginResponse : ActionResponse
        {
            public string token { get; set; }
        }

        public class ActionResponse : BaseResponse
        {
            public PlayerInfo player { get; set; }
            public PetInfo pet { get; set; }
            public List<PetInfo> pets { get; set; }
            public GuildInfo guild { get; set; }
        }

        public class PetResponse : BaseResponse
        {
            public PetInfo pet { get; set; }
            public List<PetInfo> pets { get; set; }
        }

        
        //Delegate definitions
        public delegate void PlayerUpdate(PlayerInfo player);
        public delegate void PetUpdate(PetResponse player);
        public delegate void GuildUpdate(GuildInfo guild);

        //Actual Delegates
        private List<PlayerUpdate> _playerUpdateDelegates { get; set; }
        private List<PetUpdate> _petUpdateDelegates { get; set; }
        private List<GuildUpdate> _guildUpdateDelegates { get; set; }
        
        private void SendPlayerUpdate(PlayerInfo info)
        {
            foreach(var dele in _playerUpdateDelegates)
            {
                dele(info);
            }
        }

        private void SendPetUpdate(PetResponse info)
        {
            foreach (var dele in _petUpdateDelegates)
            {
                dele(info);
            }
        }

        private void SendGuildUpdate(GuildInfo info)
        {
            foreach (var dele in _guildUpdateDelegates)
            {
                dele(info);
            }
        }

        public void AddPlayerUpdateDelegate(PlayerUpdate updateDelegate)
        {
            _playerUpdateDelegates.Add(updateDelegate);
        }

        public void AddPetUpdateDelegate(PetUpdate updateDelegate)
        {
            _petUpdateDelegates.Add(updateDelegate);
        }

        public void AddGuildUpdateDelegate(GuildUpdate updateDelegate)
        {
            _guildUpdateDelegates.Add(updateDelegate);
        }

        public void RemovePlayerUpdateDelegate(PlayerUpdate updateDelegate)
        {
            _playerUpdateDelegates.Remove(updateDelegate);
        }

        public void RemovePetUpdateDelegate(PetUpdate updateDelegate)
        {
            _petUpdateDelegates.Remove(updateDelegate);
        }

        public void RemoveGuildUpdateDelegate(GuildUpdate updateDelegate)
        {
            _guildUpdateDelegates.Remove(updateDelegate);
        }
    }
}
