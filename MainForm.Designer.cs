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
            this.EquipmentTab = new System.Windows.Forms.TabPage();
            this.EventsTab = new System.Windows.Forms.TabPage();
            this.InfoTabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // InfoTabControl
            // 
            this.InfoTabControl.Controls.Add(this.PlayerInfoTab);
            this.InfoTabControl.Controls.Add(this.EquipmentTab);
            this.InfoTabControl.Controls.Add(this.EventsTab);
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
            // EventsTab
            // 
            this.EventsTab.Location = new System.Drawing.Point(4, 22);
            this.EventsTab.Name = "EventsTab";
            this.EventsTab.Size = new System.Drawing.Size(776, 536);
            this.EventsTab.TabIndex = 2;
            this.EventsTab.Text = "Events";
            this.EventsTab.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.InfoTabControl);
            this.Name = "MainForm";
            this.Text = "IdleLands GUI";
            this.InfoTabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl InfoTabControl;
        private System.Windows.Forms.TabPage PlayerInfoTab;
        private System.Windows.Forms.TabPage EquipmentTab;
        private System.Windows.Forms.TabPage EventsTab;


    }
}