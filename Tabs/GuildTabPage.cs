using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleLandsGUI.Model;
using IdleLandsGUI.Model.Guilds;

namespace IdleLandsGUI.Tabs
{
    public partial class GuildTabPage : UserControl
    {
        private IdleLandsComms _comms { get; set; }
        private string _selectedMember { get; set; }
        private string _selectedInvitee { get; set; }
        private Enums.GuildStatus _guildStatus { get; set; }

        public GuildTabPage(PlayerInfo playerInfo, GuildInfo guildInfo, List<string> guildInvites, IdleLandsComms comms)
        {
            InitializeComponent();
            Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            _comms = comms;
            _comms.AddPlayerUpdateDelegate(UpdatePlayerInfo);
            _comms.AddGuildUpdateDelegate(UpdateGuildInfo);
            _comms.AddGuildInvitesUpdateDelegate(UpdateGuildInvitesInfo);

            KickPlayerButton.Enabled = false;
            PromotePlayerButton.Enabled = false;
            DemotePlayerButton.Enabled = false;

            UpdatePlayerInfo(playerInfo);
            UpdateGuildInfo(guildInfo);
            UpdateGuildInvitesInfo(guildInvites);
        }

        private void UpdateGuildInvitesInfo(List<string> guildInvites)
        {
            if (guildInvites == null)
                return;

            List<ListboxRow> membersData = new List<ListboxRow>();

            foreach (var member in guildInvites)
            {
                string name = member;
                if (name.IndexOf('#') != -1)
                    name = name.Substring(name.IndexOf('#') + 1);
                membersData.Add(new ListboxRow { Text = name, Value = member });
            }

            PersonalInvitesListbox.DisplayMember = "Text";
            PersonalInvitesListbox.DataSource = membersData;
        }

        private void UpdateGuildInfo(GuildInfo info)
        {
            if (info == null)
                return;

            GuildTaxNumeric.Value = info.taxPercent;
            List<ListboxRow> membersData = new List<ListboxRow>();

            foreach(var member in info.members)
            {
                string name = member.name + (member.isAdmin ? " (admin)" : "");
                membersData.Add(new ListboxRow { Text = name, Value = member.name });
            }

            MembersListbox.DisplayMember = "Text";
            MembersListbox.DataSource = membersData;

            List<ListboxRow> invitesData = new List<ListboxRow>();

            foreach(var ident in info.invites)
            {
                string name = ident;
                if (name.IndexOf('#') != -1)
                    name = name.Substring(name.IndexOf('#') + 1);

                invitesData.Add(new ListboxRow { Text = name, Value = ident });
            }

            InvitesListbox.DisplayMember = "Text";
            InvitesListbox.DataSource = invitesData;

            GuildInfoNameLabel.Text = "Guild Name: " + info.name;
            if(info.gold != null)
                GuildInfoGoldLabel.Text = "Guild Gold: " + info.gold.__current;
            GuildInfoInvitesLabel.Text = "Guild Invites Remaining: " + info.invitesAvailable;
            GuildInfoLevelLabel.Text = "Guild Level: " + info.level;
        }

        private void UpdatePlayerInfo(PlayerInfo info)
        {
            //static stuff
            if (!info.guildStatus.HasValue || info.guildStatus == (int)Enums.GuildStatus.NotInAGuild)
            {
                CreateGuildButton.Enabled = true;
                DisbandGuildButton.Enabled = false;
                LeaveGuildButton.Enabled = false;
                InvitePlayerGuildButton.Enabled = false;
                AcceptInviteGuildButton.Enabled = true;
                SetGuildTaxButton.Enabled = false;
                SetPersonalTaxButton.Enabled = false;

                _guildStatus = Enums.GuildStatus.NotInAGuild;
            }
            else if (info.guildStatus == (int)Enums.GuildStatus.RegularMember)
            {
                CreateGuildButton.Enabled = false;
                DisbandGuildButton.Enabled = false;
                LeaveGuildButton.Enabled = true;
                InvitePlayerGuildButton.Enabled = false;
                AcceptInviteGuildButton.Enabled = false;
                SetGuildTaxButton.Enabled = false;
                SetPersonalTaxButton.Enabled = true;

                _guildStatus = Enums.GuildStatus.RegularMember;
            }
            else if (info.guildStatus == (int)Enums.GuildStatus.AdminMember)
            {
                CreateGuildButton.Enabled = false;
                DisbandGuildButton.Enabled = false;
                LeaveGuildButton.Enabled = true;
                InvitePlayerGuildButton.Enabled = true;
                AcceptInviteGuildButton.Enabled = false;
                SetGuildTaxButton.Enabled = false;
                SetPersonalTaxButton.Enabled = true;

                _guildStatus = Enums.GuildStatus.AdminMember;
            }
            else if (info.guildStatus == (int)Enums.GuildStatus.Leader)
            {
                CreateGuildButton.Enabled = false;
                DisbandGuildButton.Enabled = true;
                LeaveGuildButton.Enabled = true;
                InvitePlayerGuildButton.Enabled = true;
                AcceptInviteGuildButton.Enabled = false;
                SetGuildTaxButton.Enabled = true;
                SetPersonalTaxButton.Enabled = true;

                _guildStatus = Enums.GuildStatus.Leader;
            }

            PersonalTaxNumeric.Value = info.guildTax;
            DonateNumeric.Maximum = info.gold.__current;
        }

        private void CreateGuildButton_Click(object sender, EventArgs e)
        {
            CreateGuildButton.Enabled = false;
            _comms.SendCreateGuild(CreateGuildTextbox.Text, () =>
            {
                CreateGuildButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void DisbandGuildButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Really disband?", "Disband guild?",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            DisbandGuildButton.Enabled = false;
            _comms.SendDisbandGuild(() =>
            {
                DisbandGuildButton.Enabled = true;
                MembersListbox.DataSource = new List<ListboxRow>();
                InvitesListbox.DataSource = new List<ListboxRow>();
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void LeaveGuildButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Really leave?", "Leave guild?",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            LeaveGuildButton.Enabled = false;
            _comms.SendLeaveGuild(() =>
            {
                LeaveGuildButton.Enabled = true;
                MembersListbox.DataSource = new List<ListboxRow>();
                InvitesListbox.DataSource = new List<ListboxRow>();
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void InvitePlayerGuildButton_Click(object sender, EventArgs e)
        {
            InvitePlayerGuildButton.Enabled = false;
            _comms.SendInvitePlayerGuild(InvitePlayerGuildTextbox.Text,
            () =>
            {
                InvitePlayerGuildButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void AcceptInviteGuildButton_Click(object sender, EventArgs e)
        {
            AcceptInviteGuildButton.Enabled = false;
            _comms.SendInviteManageGuild(true, _selectedInvitee,
            () =>
            {
                AcceptInviteGuildButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void DeclineInviteGuildButton_Click(object sender, EventArgs e)
        {
            DeclineInviteGuildButton.Enabled = false;
            _comms.SendInviteManageGuild(false, _selectedInvitee,
            () =>
            {
                DeclineInviteGuildButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void SetGuildTaxButton_Click(object sender, EventArgs e)
        {
            SetGuildTaxButton.Enabled = false;
            _comms.SendSetTaxGuild((int)GuildTaxNumeric.Value,
            () =>
            {
                SetGuildTaxButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void SetPersonalTaxButton_Click(object sender, EventArgs e)
        {
            SetPersonalTaxButton.Enabled = false;
            _comms.SendSetTaxPlayer((int)PersonalTaxNumeric.Value,
            () =>
            {
                SetPersonalTaxButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void KickPlayerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMember))
                return;

            if (MessageBox.Show(this, "Really kick " + _selectedMember + "?", "Kick player?",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
            {
                return;
            }

            KickPlayerButton.Enabled = false;
            _comms.SendKickGuild(_selectedMember,
            () =>
            {
                KickPlayerButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void PromotePlayerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMember))
                return;

            PromotePlayerButton.Enabled = false;
            _comms.SendPromoteGuild(_selectedMember,
            () =>
            {
                PromotePlayerButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void DemotePlayerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMember))
                return;

            DemotePlayerButton.Enabled = false;
            _comms.SendDemoteGuild(_selectedMember,
            () =>
            {
                DemotePlayerButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void DonateButton_Click(object sender, EventArgs e)
        {
            DonateButton.Enabled = false;
            _comms.SendDonateGuild((int)DonateNumeric.Value,
            () =>
            {
                DonateButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void ConstructBuildingButton_Click(object sender, EventArgs e)
        {
            ConstructBuildingTextbox.Enabled = false;
            _comms.SendConstructBuildingGuild(ConstructBuildingTextbox.Text, (int)ConstructBuildingSlotNumeric.Value,
            () =>
            {
                ConstructBuildingTextbox.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void UpgradeBuildingButton_Click(object sender, EventArgs e)
        {
            UpgradeBuildingButton.Enabled = false;
            _comms.SendUpgradeBuildingGuild(UpgradeBuildingTextbox.Text,
            () =>
            {
                UpgradeBuildingButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void MoveGuildButton_Click(object sender, EventArgs e)
        {
            MoveGuildButton.Enabled = false;
            _comms.SendMoveGuild(MoveGuildTextbox.Text,
            () =>
            {
                MoveGuildButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void BuyBuffButton_Click(object sender, EventArgs e)
        {
            BuyBuffButton.Enabled = false;
            _comms.SendBuyBuffGuild(BuyBuffTextbox.Text, (int)BuyBuffNumeric.Value,
            () =>
            {
                BuyBuffButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void PersonalInvitesListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedInvitee = (PersonalInvitesListbox.SelectedItem as ListboxRow).Value;
        }

        private void MembersListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedMember = (MembersListbox.SelectedItem as ListboxRow).Value;

            if(_guildStatus == Enums.GuildStatus.AdminMember || _guildStatus == Enums.GuildStatus.Leader)
            {
                KickPlayerButton.Enabled = true;
                PromotePlayerButton.Enabled = true;
                DemotePlayerButton.Enabled = true;
            }
        }

        private class ListboxRow
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
    }
}
