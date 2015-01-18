using IdleLandsGUI.Model;
using IdleLandsGUI.Properties;
using IdleLandsGUI.SystemTray;
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
        private IdleLandsComms _comms { get; set; }
        private bool _closedByUser { get; set; }
        private IdlelandContextMenu _menu { get; set; }
        public LoginForm(IdleLandsComms comms, IdlelandContextMenu menu)
        {
            InitializeComponent();
            this.Icon = Icon.FromHandle(Resources.IdleLandsIcon.GetHicon());
            ServerComboBox.SelectedIndex = 1;
            _comms = comms;
            _menu = menu;
            _menu.SetForm(this);
            if (ServerComboBox.Text.IndexOf('(') >= 0)
                _comms.SetServer(ServerComboBox.Text.Substring(0, ServerComboBox.Text.IndexOf('(')).Trim());
            else
                _comms.SetServer(ServerComboBox.Text.Trim());
            _closedByUser = true;
            this.FormClosing += LoginForm_FormClosing;
            this.ServerComboBox.TextChanged += ServerComboBox_OnChange;
        }

        private void DisableControls()
        {
            RegisterButton.Enabled = false;
            LoginButton.Enabled = false;
            AdvancedLoginButton.Enabled = false;
            LoginFailedLabel.Visible = false;
            ServerComboBox.Enabled = false;
            UsernameTextbox.Enabled = false;
            PasswordTextbox.Enabled = false;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            DisableControls();
            _comms.Register(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessful, LoginUnsuccessful);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            DisableControls();
            _comms.Login(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessful, LoginUnsuccessful);
        }

        private void AdvancedLoginButton_Click(object sender, EventArgs e)
        {
            DisableControls();
            _comms.AdvancedLogin(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessful, LoginUnsuccessful);
        }

        private void ServerComboBox_OnChange(object sender, EventArgs e)
        {
            if(ServerComboBox.Text.Contains('('))
                _comms.SetServer(ServerComboBox.Text.Substring(0, ServerComboBox.Text.IndexOf('(')).Trim());
            else
                _comms.SetServer(ServerComboBox.Text.Trim());
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
            if (e.CloseReason == CloseReason.UserClosing && _closedByUser)
            {
                if (MessageBox.Show(this, "Really?", "Closing...",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel) e.Cancel = true;
                else
                    Application.Exit();
            }
        }

        public delegate void LoginResultDelegate(IdleLandsComms.ActionResponse info);

        public void LoginSuccessful(IdleLandsComms.ActionResponse info)
        {
            this.Invoke((MethodInvoker)delegate
            {
                MainForm newForm = new MainForm(info, _comms);
                _closedByUser = false;
                _menu.SetForm(newForm);
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
                ServerComboBox.Enabled = true;
                UsernameTextbox.Enabled = true;
                PasswordTextbox.Enabled = true;
                LoginFailedLabel.Text = "Login failed: " + message;
                LoginFailedLabel.Visible = true;
                
            });
        }
    }
}
