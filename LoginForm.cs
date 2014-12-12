using IdleLandsGUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleLandsGUI
{
    public partial class LoginForm : Form
    {
        private IdleLandsComms Comms { get; set; }
        private bool ClosedByUser { get; set; }
        public LoginForm(IdleLandsComms comms)
        {
            InitializeComponent();
            ServerComboBox.SelectedIndex = 0;
            Comms = comms;
            Comms.SetServer(ServerComboBox.Text);
            ClosedByUser = true;
            this.FormClosing += LoginForm_FormClosing;
            this.ServerComboBox.TextChanged += ServerComboBox_OnChange;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            RegisterButton.Enabled = false;
            LoginButton.Enabled = false;
            AdvancedLoginButton.Enabled = false;
            LoginFailedLabel.Visible = false;
            Comms.Register(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessful, LoginUnsuccessful);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            RegisterButton.Enabled = false;
            LoginButton.Enabled = false;
            AdvancedLoginButton.Enabled = false;
            LoginFailedLabel.Visible = false;
            Comms.Login(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessful, LoginUnsuccessful);
        }

        private void AdvancedLoginButton_Click(object sender, EventArgs e)
        {
            RegisterButton.Enabled = false;
            LoginButton.Enabled = false;
            AdvancedLoginButton.Enabled = false;
            LoginFailedLabel.Visible = false;
            Comms.AdvancedLogin(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessful, LoginUnsuccessful);
        }

        private void ServerComboBox_OnChange(object sender, EventArgs e)
        {
            if(ServerComboBox.Text.Contains('('))
                Comms.SetServer(ServerComboBox.Text.Substring(0, ServerComboBox.Text.IndexOf('(') - 1));
            else
                Comms.SetServer(ServerComboBox.Text);
        }

        private void PasswordTextbox_KeyDown(object send, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                LoginButton.PerformClick();
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing && ClosedByUser)
            {
                if (MessageBox.Show(this, "Really?", "Closing...",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel) e.Cancel = true;
                else
                    Application.Exit();
            }
        }

        public delegate void LoginResultDelegate(PlayerInfo info);

        public void LoginSuccessful(PlayerInfo info)
        {
            this.Invoke((MethodInvoker)delegate
            {
                MainForm newForm = new MainForm(info, Comms);
                ClosedByUser = false;
                this.Close();
                newForm.Show();
            });
        }

        public delegate void LoginFailedDelegate(string message);

        public void LoginUnsuccessful(string message)
        {
            this.Invoke((MethodInvoker)delegate
            {
                RegisterButton.Enabled = true;
                LoginButton.Enabled = true;
                AdvancedLoginButton.Enabled = true;
                LoginFailedLabel.Text = "Login failed: " + message;
                LoginFailedLabel.Visible = true;
            });
        }
    }
}
