namespace IdleLandsGUI.Tabs
{
    partial class GuildTabPage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.AcceptInviteGuildTextbox = new System.Windows.Forms.TextBox();
            this.AcceptInviteGuildButton = new System.Windows.Forms.Button();
            this.CreateGuildTextbox = new System.Windows.Forms.TextBox();
            this.InvitePlayerGuildTextbox = new System.Windows.Forms.TextBox();
            this.InvitePlayerGuildButton = new System.Windows.Forms.Button();
            this.LeaveGuildButton = new System.Windows.Forms.Button();
            this.DisbandGuildButton = new System.Windows.Forms.Button();
            this.CreateGuildButton = new System.Windows.Forms.Button();
            this.SetGuildTaxButton = new System.Windows.Forms.Button();
            this.SetPersonalTaxButton = new System.Windows.Forms.Button();
            this.GuildTaxNumeric = new System.Windows.Forms.NumericUpDown();
            this.PersonalTaxNumeric = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.GuildTaxNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PersonalTaxNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // AcceptInviteGuildTextbox
            // 
            this.AcceptInviteGuildTextbox.Location = new System.Drawing.Point(113, 124);
            this.AcceptInviteGuildTextbox.Name = "AcceptInviteGuildTextbox";
            this.AcceptInviteGuildTextbox.Size = new System.Drawing.Size(100, 20);
            this.AcceptInviteGuildTextbox.TabIndex = 15;
            // 
            // AcceptInviteGuildButton
            // 
            this.AcceptInviteGuildButton.Location = new System.Drawing.Point(3, 122);
            this.AcceptInviteGuildButton.Name = "AcceptInviteGuildButton";
            this.AcceptInviteGuildButton.Size = new System.Drawing.Size(104, 23);
            this.AcceptInviteGuildButton.TabIndex = 14;
            this.AcceptInviteGuildButton.Text = "Accept Invite";
            this.AcceptInviteGuildButton.UseVisualStyleBackColor = true;
            this.AcceptInviteGuildButton.Click += new System.EventHandler(this.AcceptInviteGuildButton_Click);
            // 
            // CreateGuildTextbox
            // 
            this.CreateGuildTextbox.Location = new System.Drawing.Point(113, 5);
            this.CreateGuildTextbox.Name = "CreateGuildTextbox";
            this.CreateGuildTextbox.Size = new System.Drawing.Size(100, 20);
            this.CreateGuildTextbox.TabIndex = 13;
            // 
            // InvitePlayerGuildTextbox
            // 
            this.InvitePlayerGuildTextbox.Location = new System.Drawing.Point(112, 94);
            this.InvitePlayerGuildTextbox.Name = "InvitePlayerGuildTextbox";
            this.InvitePlayerGuildTextbox.Size = new System.Drawing.Size(101, 20);
            this.InvitePlayerGuildTextbox.TabIndex = 12;
            // 
            // InvitePlayerGuildButton
            // 
            this.InvitePlayerGuildButton.Location = new System.Drawing.Point(3, 92);
            this.InvitePlayerGuildButton.Name = "InvitePlayerGuildButton";
            this.InvitePlayerGuildButton.Size = new System.Drawing.Size(104, 23);
            this.InvitePlayerGuildButton.TabIndex = 11;
            this.InvitePlayerGuildButton.Text = "Invite Player";
            this.InvitePlayerGuildButton.UseVisualStyleBackColor = true;
            this.InvitePlayerGuildButton.Click += new System.EventHandler(this.InvitePlayerGuildButton_Click);
            // 
            // LeaveGuildButton
            // 
            this.LeaveGuildButton.Location = new System.Drawing.Point(3, 62);
            this.LeaveGuildButton.Name = "LeaveGuildButton";
            this.LeaveGuildButton.Size = new System.Drawing.Size(104, 23);
            this.LeaveGuildButton.TabIndex = 10;
            this.LeaveGuildButton.Text = "Leave";
            this.LeaveGuildButton.UseVisualStyleBackColor = true;
            this.LeaveGuildButton.Click += new System.EventHandler(this.LeaveGuildButton_Click);
            // 
            // DisbandGuildButton
            // 
            this.DisbandGuildButton.Location = new System.Drawing.Point(3, 32);
            this.DisbandGuildButton.Name = "DisbandGuildButton";
            this.DisbandGuildButton.Size = new System.Drawing.Size(104, 23);
            this.DisbandGuildButton.TabIndex = 9;
            this.DisbandGuildButton.Text = "Disband";
            this.DisbandGuildButton.UseVisualStyleBackColor = true;
            this.DisbandGuildButton.Click += new System.EventHandler(this.DisbandGuildButton_Click);
            // 
            // CreateGuildButton
            // 
            this.CreateGuildButton.Location = new System.Drawing.Point(3, 3);
            this.CreateGuildButton.Name = "CreateGuildButton";
            this.CreateGuildButton.Size = new System.Drawing.Size(104, 23);
            this.CreateGuildButton.TabIndex = 8;
            this.CreateGuildButton.Text = "Create";
            this.CreateGuildButton.UseVisualStyleBackColor = true;
            this.CreateGuildButton.Click += new System.EventHandler(this.CreateGuildButton_Click);
            // 
            // SetGuildTaxButton
            // 
            this.SetGuildTaxButton.Location = new System.Drawing.Point(4, 152);
            this.SetGuildTaxButton.Name = "SetGuildTaxButton";
            this.SetGuildTaxButton.Size = new System.Drawing.Size(103, 23);
            this.SetGuildTaxButton.TabIndex = 16;
            this.SetGuildTaxButton.Text = "Set Guild Tax";
            this.SetGuildTaxButton.UseVisualStyleBackColor = true;
            this.SetGuildTaxButton.Click += new System.EventHandler(this.SetGuildTaxButton_Click);
            // 
            // SetPersonalTaxButton
            // 
            this.SetPersonalTaxButton.Location = new System.Drawing.Point(4, 182);
            this.SetPersonalTaxButton.Name = "SetPersonalTaxButton";
            this.SetPersonalTaxButton.Size = new System.Drawing.Size(103, 23);
            this.SetPersonalTaxButton.TabIndex = 17;
            this.SetPersonalTaxButton.Text = "Set Personal Tax";
            this.SetPersonalTaxButton.UseVisualStyleBackColor = true;
            this.SetPersonalTaxButton.Click += new System.EventHandler(this.SetPersonalTaxButton_Click);
            // 
            // GuildTaxNumeric
            // 
            this.GuildTaxNumeric.Location = new System.Drawing.Point(113, 155);
            this.GuildTaxNumeric.Name = "GuildTaxNumeric";
            this.GuildTaxNumeric.Size = new System.Drawing.Size(100, 20);
            this.GuildTaxNumeric.TabIndex = 18;
            // 
            // PersonalTaxNumeric
            // 
            this.PersonalTaxNumeric.Location = new System.Drawing.Point(113, 185);
            this.PersonalTaxNumeric.Name = "PersonalTaxNumeric";
            this.PersonalTaxNumeric.Size = new System.Drawing.Size(100, 20);
            this.PersonalTaxNumeric.TabIndex = 19;
            // 
            // GuildTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PersonalTaxNumeric);
            this.Controls.Add(this.GuildTaxNumeric);
            this.Controls.Add(this.SetPersonalTaxButton);
            this.Controls.Add(this.SetGuildTaxButton);
            this.Controls.Add(this.AcceptInviteGuildTextbox);
            this.Controls.Add(this.AcceptInviteGuildButton);
            this.Controls.Add(this.CreateGuildTextbox);
            this.Controls.Add(this.InvitePlayerGuildTextbox);
            this.Controls.Add(this.InvitePlayerGuildButton);
            this.Controls.Add(this.LeaveGuildButton);
            this.Controls.Add(this.DisbandGuildButton);
            this.Controls.Add(this.CreateGuildButton);
            this.Name = "GuildTabPage";
            this.Size = new System.Drawing.Size(776, 536);
            ((System.ComponentModel.ISupportInitialize)(this.GuildTaxNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PersonalTaxNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox AcceptInviteGuildTextbox;
        private System.Windows.Forms.Button AcceptInviteGuildButton;
        private System.Windows.Forms.TextBox CreateGuildTextbox;
        private System.Windows.Forms.TextBox InvitePlayerGuildTextbox;
        private System.Windows.Forms.Button InvitePlayerGuildButton;
        private System.Windows.Forms.Button LeaveGuildButton;
        private System.Windows.Forms.Button DisbandGuildButton;
        private System.Windows.Forms.Button CreateGuildButton;
        private System.Windows.Forms.Button SetGuildTaxButton;
        private System.Windows.Forms.Button SetPersonalTaxButton;
        private System.Windows.Forms.NumericUpDown GuildTaxNumeric;
        private System.Windows.Forms.NumericUpDown PersonalTaxNumeric;
    }
}
