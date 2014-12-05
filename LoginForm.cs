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
            Comms = comms;
            ClosedByUser = true;
            this.FormClosing += LoginForm_FormClosing;
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            RegisterButton.Enabled = false;
            LoginButton.Enabled = false;
            AdvancedLoginButton.Enabled = false;
            LoginFailedLabel.Visible = false;
            Comms.Register(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessfulDelegate, LoginUnsuccessfulDelegate);
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            RegisterButton.Enabled = false;
            LoginButton.Enabled = false;
            AdvancedLoginButton.Enabled = false;
            LoginFailedLabel.Visible = false;
            Comms.Login(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessfulDelegate, LoginUnsuccessfulDelegate);
        }

        private void AdvancedLoginButton_Click(object sender, EventArgs e)
        {
            RegisterButton.Enabled = false;
            LoginButton.Enabled = false;
            AdvancedLoginButton.Enabled = false;
            LoginFailedLabel.Visible = false;
            Comms.AdvancedLogin(UsernameTextbox.Text, PasswordTextbox.Text, LoginSuccessfulDelegate, LoginUnsuccessfulDelegate);
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

        public delegate void LoginResult(PlayerInfo info);

        public void LoginSuccessfulDelegate(PlayerInfo info)
        {
            this.Invoke((MethodInvoker)delegate
            {
                MainForm newForm = new MainForm(info, Comms);
                ClosedByUser = false;
                this.Close();
                newForm.Show();
            });
        }

        public void LoginUnsuccessfulDelegate(PlayerInfo info)
        {
            this.Invoke((MethodInvoker)delegate
            {
                RegisterButton.Enabled = true;
                LoginButton.Enabled = true;
                AdvancedLoginButton.Enabled = true;
                LoginFailedLabel.Visible = true;
            });
        }
    }
}
