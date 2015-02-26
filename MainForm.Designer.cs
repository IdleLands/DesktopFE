namespace IdleLandsGUI
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.InfoTabControl = new System.Windows.Forms.TabControl();
            this.PlayerInfoTab = new System.Windows.Forms.TabPage();
            this.PlayerSettingsTab = new System.Windows.Forms.TabPage();
            this.StringMessageTextbox = new System.Windows.Forms.TextBox();
            this.StringTypeTextbox = new System.Windows.Forms.TextBox();
            this.SetStringButton = new System.Windows.Forms.Button();
            this.ResetPasswordTextbox = new System.Windows.Forms.TextBox();
            this.ResetPasswordButton = new System.Windows.Forms.Button();
            this.ApplyPlayerSettingsButton = new System.Windows.Forms.Button();
            this.GenderGroupBox = new System.Windows.Forms.GroupBox();
            this.OtherTextBox = new System.Windows.Forms.TextBox();
            this.OtherRadioButton = new System.Windows.Forms.RadioButton();
            this.FemaleRadioButton = new System.Windows.Forms.RadioButton();
            this.MaleRadioButton = new System.Windows.Forms.RadioButton();
            this.PriorityPointsGroupBox = new System.Windows.Forms.GroupBox();
            this.IntTrackBar = new System.Windows.Forms.TrackBar();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.WisTrackBar = new System.Windows.Forms.TrackBar();
            this.DexTrackBar = new System.Windows.Forms.TrackBar();
            this.AgiTrackBar = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.ConTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.StrTrackBar = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.EquipmentTab = new System.Windows.Forms.TabPage();
            this.InventoryTab = new System.Windows.Forms.TabPage();
            this.BuyPetTab = new System.Windows.Forms.TabPage();
            this.PetTab = new System.Windows.Forms.TabPage();
            this.EventsTab = new System.Windows.Forms.TabPage();
            this.GuildTab = new System.Windows.Forms.TabPage();
            this.MapTab = new System.Windows.Forms.TabPage();
            this.InfoTabControl.SuspendLayout();
            this.PlayerSettingsTab.SuspendLayout();
            this.GenderGroupBox.SuspendLayout();
            this.PriorityPointsGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WisTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DexTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgiTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // InfoTabControl
            // 
            this.InfoTabControl.Controls.Add(this.PlayerInfoTab);
            this.InfoTabControl.Controls.Add(this.PlayerSettingsTab);
            this.InfoTabControl.Controls.Add(this.EquipmentTab);
            this.InfoTabControl.Controls.Add(this.InventoryTab);
            this.InfoTabControl.Controls.Add(this.BuyPetTab);
            this.InfoTabControl.Controls.Add(this.PetTab);
            this.InfoTabControl.Controls.Add(this.EventsTab);
            this.InfoTabControl.Controls.Add(this.GuildTab);
            this.InfoTabControl.Controls.Add(this.MapTab);
            this.InfoTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.InfoTabControl.Location = new System.Drawing.Point(0, 0);
            this.InfoTabControl.Name = "InfoTabControl";
            this.InfoTabControl.SelectedIndex = 0;
            this.InfoTabControl.Size = new System.Drawing.Size(784, 562);
            this.InfoTabControl.TabIndex = 0;
            // 
            // PlayerInfoTab
            // 
            this.PlayerInfoTab.Location = new System.Drawing.Point(4, 22);
            this.PlayerInfoTab.Name = "PlayerInfoTab";
            this.PlayerInfoTab.Padding = new System.Windows.Forms.Padding(3);
            this.PlayerInfoTab.Size = new System.Drawing.Size(776, 536);
            this.PlayerInfoTab.TabIndex = 0;
            this.PlayerInfoTab.Text = "Player Info";
            this.PlayerInfoTab.UseVisualStyleBackColor = true;
            // 
            // PlayerSettingsTab
            // 
            this.PlayerSettingsTab.Controls.Add(this.StringMessageTextbox);
            this.PlayerSettingsTab.Controls.Add(this.StringTypeTextbox);
            this.PlayerSettingsTab.Controls.Add(this.SetStringButton);
            this.PlayerSettingsTab.Controls.Add(this.ResetPasswordTextbox);
            this.PlayerSettingsTab.Controls.Add(this.ResetPasswordButton);
            this.PlayerSettingsTab.Controls.Add(this.ApplyPlayerSettingsButton);
            this.PlayerSettingsTab.Controls.Add(this.GenderGroupBox);
            this.PlayerSettingsTab.Controls.Add(this.PriorityPointsGroupBox);
            this.PlayerSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.PlayerSettingsTab.Name = "PlayerSettingsTab";
            this.PlayerSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.PlayerSettingsTab.Size = new System.Drawing.Size(776, 536);
            this.PlayerSettingsTab.TabIndex = 3;
            this.PlayerSettingsTab.Text = "Player Settings";
            this.PlayerSettingsTab.UseVisualStyleBackColor = true;
            // 
            // StringMessageTextbox
            // 
            this.StringMessageTextbox.Location = new System.Drawing.Point(215, 377);
            this.StringMessageTextbox.Name = "StringMessageTextbox";
            this.StringMessageTextbox.Size = new System.Drawing.Size(100, 20);
            this.StringMessageTextbox.TabIndex = 13;
            // 
            // StringTypeTextbox
            // 
            this.StringTypeTextbox.Location = new System.Drawing.Point(109, 377);
            this.StringTypeTextbox.Name = "StringTypeTextbox";
            this.StringTypeTextbox.Size = new System.Drawing.Size(100, 20);
            this.StringTypeTextbox.TabIndex = 12;
            // 
            // SetStringButton
            // 
            this.SetStringButton.Location = new System.Drawing.Point(4, 375);
            this.SetStringButton.Name = "SetStringButton";
            this.SetStringButton.Size = new System.Drawing.Size(99, 23);
            this.SetStringButton.TabIndex = 11;
            this.SetStringButton.Text = "Set String";
            this.SetStringButton.UseVisualStyleBackColor = true;
            this.SetStringButton.Click += new System.EventHandler(this.SetStringButton_Click);
            // 
            // ResetPasswordTextbox
            // 
            this.ResetPasswordTextbox.Location = new System.Drawing.Point(109, 348);
            this.ResetPasswordTextbox.Name = "ResetPasswordTextbox";
            this.ResetPasswordTextbox.Size = new System.Drawing.Size(100, 20);
            this.ResetPasswordTextbox.TabIndex = 10;
            // 
            // ResetPasswordButton
            // 
            this.ResetPasswordButton.Location = new System.Drawing.Point(4, 346);
            this.ResetPasswordButton.Name = "ResetPasswordButton";
            this.ResetPasswordButton.Size = new System.Drawing.Size(99, 23);
            this.ResetPasswordButton.TabIndex = 9;
            this.ResetPasswordButton.Text = "Reset Password";
            this.ResetPasswordButton.UseVisualStyleBackColor = true;
            this.ResetPasswordButton.Click += new System.EventHandler(this.ResetPasswordButton_Click);
            // 
            // ApplyPlayerSettingsButton
            // 
            this.ApplyPlayerSettingsButton.Location = new System.Drawing.Point(695, 505);
            this.ApplyPlayerSettingsButton.Name = "ApplyPlayerSettingsButton";
            this.ApplyPlayerSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyPlayerSettingsButton.TabIndex = 7;
            this.ApplyPlayerSettingsButton.Text = "Apply";
            this.ApplyPlayerSettingsButton.UseVisualStyleBackColor = true;
            this.ApplyPlayerSettingsButton.Click += new System.EventHandler(this.ApplyPlayerSettingsButton_Click);
            // 
            // GenderGroupBox
            // 
            this.GenderGroupBox.Controls.Add(this.OtherTextBox);
            this.GenderGroupBox.Controls.Add(this.OtherRadioButton);
            this.GenderGroupBox.Controls.Add(this.FemaleRadioButton);
            this.GenderGroupBox.Controls.Add(this.MaleRadioButton);
            this.GenderGroupBox.Location = new System.Drawing.Point(161, 6);
            this.GenderGroupBox.Name = "GenderGroupBox";
            this.GenderGroupBox.Size = new System.Drawing.Size(182, 92);
            this.GenderGroupBox.TabIndex = 8;
            this.GenderGroupBox.TabStop = false;
            this.GenderGroupBox.Text = "Gender";
            // 
            // OtherTextBox
            // 
            this.OtherTextBox.Location = new System.Drawing.Point(75, 66);
            this.OtherTextBox.Name = "OtherTextBox";
            this.OtherTextBox.Size = new System.Drawing.Size(100, 20);
            this.OtherTextBox.TabIndex = 3;
            // 
            // OtherRadioButton
            // 
            this.OtherRadioButton.AutoSize = true;
            this.OtherRadioButton.Location = new System.Drawing.Point(7, 66);
            this.OtherRadioButton.Name = "OtherRadioButton";
            this.OtherRadioButton.Size = new System.Drawing.Size(51, 17);
            this.OtherRadioButton.TabIndex = 2;
            this.OtherRadioButton.TabStop = true;
            this.OtherRadioButton.Text = "Other";
            this.OtherRadioButton.UseVisualStyleBackColor = true;
            this.OtherRadioButton.CheckedChanged += new System.EventHandler(this.OtherRadioButton_CheckedChanged);
            // 
            // FemaleRadioButton
            // 
            this.FemaleRadioButton.AutoSize = true;
            this.FemaleRadioButton.Location = new System.Drawing.Point(7, 42);
            this.FemaleRadioButton.Name = "FemaleRadioButton";
            this.FemaleRadioButton.Size = new System.Drawing.Size(59, 17);
            this.FemaleRadioButton.TabIndex = 1;
            this.FemaleRadioButton.TabStop = true;
            this.FemaleRadioButton.Text = "Female";
            this.FemaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // MaleRadioButton
            // 
            this.MaleRadioButton.AutoSize = true;
            this.MaleRadioButton.Location = new System.Drawing.Point(7, 18);
            this.MaleRadioButton.Name = "MaleRadioButton";
            this.MaleRadioButton.Size = new System.Drawing.Size(48, 17);
            this.MaleRadioButton.TabIndex = 0;
            this.MaleRadioButton.TabStop = true;
            this.MaleRadioButton.Text = "Male";
            this.MaleRadioButton.UseVisualStyleBackColor = true;
            // 
            // PriorityPointsGroupBox
            // 
            this.PriorityPointsGroupBox.Controls.Add(this.IntTrackBar);
            this.PriorityPointsGroupBox.Controls.Add(this.label5);
            this.PriorityPointsGroupBox.Controls.Add(this.label6);
            this.PriorityPointsGroupBox.Controls.Add(this.label4);
            this.PriorityPointsGroupBox.Controls.Add(this.WisTrackBar);
            this.PriorityPointsGroupBox.Controls.Add(this.DexTrackBar);
            this.PriorityPointsGroupBox.Controls.Add(this.AgiTrackBar);
            this.PriorityPointsGroupBox.Controls.Add(this.label3);
            this.PriorityPointsGroupBox.Controls.Add(this.ConTrackBar);
            this.PriorityPointsGroupBox.Controls.Add(this.label2);
            this.PriorityPointsGroupBox.Controls.Add(this.StrTrackBar);
            this.PriorityPointsGroupBox.Controls.Add(this.label1);
            this.PriorityPointsGroupBox.Location = new System.Drawing.Point(3, 6);
            this.PriorityPointsGroupBox.Name = "PriorityPointsGroupBox";
            this.PriorityPointsGroupBox.Size = new System.Drawing.Size(152, 333);
            this.PriorityPointsGroupBox.TabIndex = 7;
            this.PriorityPointsGroupBox.TabStop = false;
            this.PriorityPointsGroupBox.Text = "Priority Points";
            // 
            // IntTrackBar
            // 
            this.IntTrackBar.Location = new System.Drawing.Point(35, 279);
            this.IntTrackBar.Maximum = 6;
            this.IntTrackBar.Name = "IntTrackBar";
            this.IntTrackBar.Size = new System.Drawing.Size(104, 45);
            this.IntTrackBar.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Int:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(0, 194);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Dex:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 247);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Wis:";
            // 
            // WisTrackBar
            // 
            this.WisTrackBar.Location = new System.Drawing.Point(35, 227);
            this.WisTrackBar.Maximum = 6;
            this.WisTrackBar.Name = "WisTrackBar";
            this.WisTrackBar.Size = new System.Drawing.Size(104, 45);
            this.WisTrackBar.TabIndex = 5;
            // 
            // DexTrackBar
            // 
            this.DexTrackBar.Location = new System.Drawing.Point(35, 174);
            this.DexTrackBar.Maximum = 6;
            this.DexTrackBar.Name = "DexTrackBar";
            this.DexTrackBar.Size = new System.Drawing.Size(104, 45);
            this.DexTrackBar.TabIndex = 4;
            // 
            // AgiTrackBar
            // 
            this.AgiTrackBar.Location = new System.Drawing.Point(35, 123);
            this.AgiTrackBar.Maximum = 6;
            this.AgiTrackBar.Name = "AgiTrackBar";
            this.AgiTrackBar.Size = new System.Drawing.Size(104, 45);
            this.AgiTrackBar.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Agi:";
            // 
            // ConTrackBar
            // 
            this.ConTrackBar.Location = new System.Drawing.Point(35, 71);
            this.ConTrackBar.Maximum = 6;
            this.ConTrackBar.Name = "ConTrackBar";
            this.ConTrackBar.Size = new System.Drawing.Size(104, 45);
            this.ConTrackBar.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Con:";
            // 
            // StrTrackBar
            // 
            this.StrTrackBar.Location = new System.Drawing.Point(35, 19);
            this.StrTrackBar.Maximum = 6;
            this.StrTrackBar.Name = "StrTrackBar";
            this.StrTrackBar.Size = new System.Drawing.Size(104, 45);
            this.StrTrackBar.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Str:";
            // 
            // EquipmentTab
            // 
            this.EquipmentTab.AutoScroll = true;
            this.EquipmentTab.Location = new System.Drawing.Point(4, 22);
            this.EquipmentTab.Name = "EquipmentTab";
            this.EquipmentTab.Padding = new System.Windows.Forms.Padding(3);
            this.EquipmentTab.Size = new System.Drawing.Size(776, 536);
            this.EquipmentTab.TabIndex = 1;
            this.EquipmentTab.Text = "Equipment";
            this.EquipmentTab.UseVisualStyleBackColor = true;
            // 
            // InventoryTab
            // 
            this.InventoryTab.Location = new System.Drawing.Point(4, 22);
            this.InventoryTab.Name = "InventoryTab";
            this.InventoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.InventoryTab.Size = new System.Drawing.Size(776, 536);
            this.InventoryTab.TabIndex = 4;
            this.InventoryTab.Text = "Inventory";
            this.InventoryTab.UseVisualStyleBackColor = true;
            // 
            // BuyPetTab
            // 
            this.BuyPetTab.Location = new System.Drawing.Point(4, 22);
            this.BuyPetTab.Name = "BuyPetTab";
            this.BuyPetTab.Padding = new System.Windows.Forms.Padding(3);
            this.BuyPetTab.Size = new System.Drawing.Size(776, 536);
            this.BuyPetTab.TabIndex = 5;
            this.BuyPetTab.Text = "BuyPetTab";
            this.BuyPetTab.UseVisualStyleBackColor = true;
            // 
            // PetTab
            // 
            this.PetTab.Location = new System.Drawing.Point(4, 22);
            this.PetTab.Name = "PetTab";
            this.PetTab.Size = new System.Drawing.Size(776, 536);
            this.PetTab.TabIndex = 6;
            this.PetTab.Text = "PetTab";
            this.PetTab.UseVisualStyleBackColor = true;
            // 
            // EventsTab
            // 
            this.EventsTab.Location = new System.Drawing.Point(4, 22);
            this.EventsTab.Name = "EventsTab";
            this.EventsTab.Size = new System.Drawing.Size(776, 536);
            this.EventsTab.TabIndex = 2;
            this.EventsTab.Text = "Events";
            this.EventsTab.UseVisualStyleBackColor = true;
            // 
            // GuildTab
            // 
            this.GuildTab.Location = new System.Drawing.Point(4, 22);
            this.GuildTab.Name = "GuildTab";
            this.GuildTab.Padding = new System.Windows.Forms.Padding(3);
            this.GuildTab.Size = new System.Drawing.Size(776, 536);
            this.GuildTab.TabIndex = 7;
            this.GuildTab.Text = "Guild";
            this.GuildTab.UseVisualStyleBackColor = true;
            // 
            // MapTab
            // 
            this.MapTab.Location = new System.Drawing.Point(4, 22);
            this.MapTab.Name = "MapTab";
            this.MapTab.Padding = new System.Windows.Forms.Padding(3);
            this.MapTab.Size = new System.Drawing.Size(776, 536);
            this.MapTab.TabIndex = 8;
            this.MapTab.Text = "Map";
            this.MapTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.InfoTabControl);
            this.Name = "MainForm";
            this.Text = "IdleLands DesktopFE";
            this.InfoTabControl.ResumeLayout(false);
            this.PlayerSettingsTab.ResumeLayout(false);
            this.PlayerSettingsTab.PerformLayout();
            this.GenderGroupBox.ResumeLayout(false);
            this.GenderGroupBox.PerformLayout();
            this.PriorityPointsGroupBox.ResumeLayout(false);
            this.PriorityPointsGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WisTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DexTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AgiTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.StrTrackBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl InfoTabControl;
        private System.Windows.Forms.TabPage PlayerInfoTab;
        private System.Windows.Forms.TabPage EventsTab;
        private System.Windows.Forms.TabPage PlayerSettingsTab;
        private System.Windows.Forms.GroupBox PriorityPointsGroupBox;
        private System.Windows.Forms.TrackBar IntTrackBar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar WisTrackBar;
        private System.Windows.Forms.TrackBar DexTrackBar;
        private System.Windows.Forms.TrackBar AgiTrackBar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar ConTrackBar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar StrTrackBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox GenderGroupBox;
        private System.Windows.Forms.RadioButton FemaleRadioButton;
        private System.Windows.Forms.RadioButton MaleRadioButton;
        private System.Windows.Forms.Button ApplyPlayerSettingsButton;
        private System.Windows.Forms.TabPage InventoryTab;
        private System.Windows.Forms.TabPage BuyPetTab;
        private System.Windows.Forms.TabPage PetTab;
        private System.Windows.Forms.TabPage GuildTab;
        private System.Windows.Forms.TabPage EquipmentTab;
        private System.Windows.Forms.TextBox OtherTextBox;
        private System.Windows.Forms.RadioButton OtherRadioButton;
        private System.Windows.Forms.TextBox ResetPasswordTextbox;
        private System.Windows.Forms.Button ResetPasswordButton;
        private System.Windows.Forms.TextBox StringMessageTextbox;
        private System.Windows.Forms.TextBox StringTypeTextbox;
        private System.Windows.Forms.Button SetStringButton;
        private System.Windows.Forms.TabPage MapTab;


    }
}