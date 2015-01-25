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

        public GuildTabPage(PlayerInfo playerInfo, GuildInfo guildInfo, IdleLandsComms comms)
        {
            InitializeComponent();
            Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            _comms = comms;
            _comms.AddPlayerUpdateDelegate(UpdatePlayerInfo);
            _comms.AddGuildUpdateDelegate(UpdateGuildInfo);
            UpdatePlayerInfo(playerInfo);
            UpdateGuildInfo(guildInfo);
        }

        private void UpdateGuildInfo(GuildInfo info)
        {
            GuildTaxNumeric.Value = info.taxPercent;
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
            }

            PersonalTaxNumeric.Value = info.guildTax;
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
            //AcceptInviteGuildButton.Name
            AcceptInviteGuildButton.Enabled = false;
            _comms.SendInviteManageGuild(true, AcceptInviteGuildTextbox.Text,
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
    }
}
