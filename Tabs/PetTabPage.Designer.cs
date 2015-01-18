namespace IdleLandsGUI.Tabs
{
    partial class PetTabPage
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
            this.PetListbox = new System.Windows.Forms.ListBox();
            this.PetNameLabel = new System.Windows.Forms.Label();
            this.PetTypeLabel = new System.Windows.Forms.Label();
            this.PetHpLabel = new System.Windows.Forms.Label();
            this.PetMpLabel = new System.Windows.Forms.Label();
            this.PetXpLabel = new System.Windows.Forms.Label();
            this.PetGoldLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.PetInventoryListbox = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.PetEquipmentListbox = new System.Windows.Forms.ListBox();
            this.PetNextItemLabel = new System.Windows.Forms.Label();
            this.ActivatePetButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.PetSkillsListbox = new System.Windows.Forms.ListBox();
            this.PetSkillCurrentValueLabel = new System.Windows.Forms.Label();
            this.PetSkillPossibleValuesLabel = new System.Windows.Forms.Label();
            this.PetSkillUpgradeCostLabel = new System.Windows.Forms.Label();
            this.UpgradeSkillButton = new System.Windows.Forms.Button();
            this.FeedPetButton = new System.Windows.Forms.Button();
            this.TakeGoldButton = new System.Windows.Forms.Button();
            this.SellItemButton = new System.Windows.Forms.Button();
            this.TakeItemButton = new System.Windows.Forms.Button();
            this.EquipItemButton = new System.Windows.Forms.Button();
            this.UnequipItemButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PetListbox
            // 
            this.PetListbox.FormattingEnabled = true;
            this.PetListbox.Location = new System.Drawing.Point(3, 22);
            this.PetListbox.Name = "PetListbox";
            this.PetListbox.Size = new System.Drawing.Size(120, 147);
            this.PetListbox.TabIndex = 0;
            this.PetListbox.SelectedIndexChanged += new System.EventHandler(this.PetListbox_SelectedIndexChanged);
            // 
            // PetNameLabel
            // 
            this.PetNameLabel.AutoSize = true;
            this.PetNameLabel.Location = new System.Drawing.Point(129, 22);
            this.PetNameLabel.Name = "PetNameLabel";
            this.PetNameLabel.Size = new System.Drawing.Size(54, 13);
            this.PetNameLabel.TabIndex = 1;
            this.PetNameLabel.Text = "Pet Name";
            // 
            // PetTypeLabel
            // 
            this.PetTypeLabel.AutoSize = true;
            this.PetTypeLabel.Location = new System.Drawing.Point(129, 35);
            this.PetTypeLabel.Name = "PetTypeLabel";
            this.PetTypeLabel.Size = new System.Drawing.Size(50, 13);
            this.PetTypeLabel.TabIndex = 2;
            this.PetTypeLabel.Text = "Pet Type";
            // 
            // PetHpLabel
            // 
            this.PetHpLabel.AutoSize = true;
            this.PetHpLabel.Location = new System.Drawing.Point(129, 48);
            this.PetHpLabel.Name = "PetHpLabel";
            this.PetHpLabel.Size = new System.Drawing.Size(41, 13);
            this.PetHpLabel.TabIndex = 3;
            this.PetHpLabel.Text = "Pet HP";
            // 
            // PetMpLabel
            // 
            this.PetMpLabel.AutoSize = true;
            this.PetMpLabel.Location = new System.Drawing.Point(129, 61);
            this.PetMpLabel.Name = "PetMpLabel";
            this.PetMpLabel.Size = new System.Drawing.Size(42, 13);
            this.PetMpLabel.TabIndex = 4;
            this.PetMpLabel.Text = "Pet MP";
            // 
            // PetXpLabel
            // 
            this.PetXpLabel.AutoSize = true;
            this.PetXpLabel.Location = new System.Drawing.Point(129, 74);
            this.PetXpLabel.Name = "PetXpLabel";
            this.PetXpLabel.Size = new System.Drawing.Size(40, 13);
            this.PetXpLabel.TabIndex = 5;
            this.PetXpLabel.Text = "Pet XP";
            // 
            // PetGoldLabel
            // 
            this.PetGoldLabel.AutoSize = true;
            this.PetGoldLabel.Location = new System.Drawing.Point(129, 87);
            this.PetGoldLabel.Name = "PetGoldLabel";
            this.PetGoldLabel.Size = new System.Drawing.Size(48, 13);
            this.PetGoldLabel.TabIndex = 6;
            this.PetGoldLabel.Text = "Pet Gold";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Pets:";
            // 
            // PetInventoryListbox
            // 
            this.PetInventoryListbox.FormattingEnabled = true;
            this.PetInventoryListbox.Location = new System.Drawing.Point(3, 192);
            this.PetInventoryListbox.Name = "PetInventoryListbox";
            this.PetInventoryListbox.Size = new System.Drawing.Size(120, 147);
            this.PetInventoryListbox.TabIndex = 8;
            this.PetInventoryListbox.SelectedIndexChanged += new System.EventHandler(this.PetInventoryListbox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 176);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Pet Inventory:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 341);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Pet Equipment";
            // 
            // PetEquipmentListbox
            // 
            this.PetEquipmentListbox.FormattingEnabled = true;
            this.PetEquipmentListbox.Location = new System.Drawing.Point(3, 357);
            this.PetEquipmentListbox.Name = "PetEquipmentListbox";
            this.PetEquipmentListbox.Size = new System.Drawing.Size(120, 147);
            this.PetEquipmentListbox.TabIndex = 10;
            this.PetEquipmentListbox.SelectedIndexChanged += new System.EventHandler(this.PetEquipmentListbox_SelectedIndexChanged);
            // 
            // PetNextItemLabel
            // 
            this.PetNextItemLabel.AutoSize = true;
            this.PetNextItemLabel.Location = new System.Drawing.Point(129, 100);
            this.PetNextItemLabel.Name = "PetNextItemLabel";
            this.PetNextItemLabel.Size = new System.Drawing.Size(71, 13);
            this.PetNextItemLabel.TabIndex = 14;
            this.PetNextItemLabel.Text = "Pet Next Item";
            // 
            // ActivatePetButton
            // 
            this.ActivatePetButton.Location = new System.Drawing.Point(652, 11);
            this.ActivatePetButton.Name = "ActivatePetButton";
            this.ActivatePetButton.Size = new System.Drawing.Size(85, 23);
            this.ActivatePetButton.TabIndex = 15;
            this.ActivatePetButton.Text = "Activate Pet";
            this.ActivatePetButton.UseVisualStyleBackColor = true;
            this.ActivatePetButton.Click += new System.EventHandler(this.ActivatePetButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 176);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Pet Skills";
            // 
            // PetSkillsListbox
            // 
            this.PetSkillsListbox.FormattingEnabled = true;
            this.PetSkillsListbox.Location = new System.Drawing.Point(132, 192);
            this.PetSkillsListbox.Name = "PetSkillsListbox";
            this.PetSkillsListbox.Size = new System.Drawing.Size(120, 147);
            this.PetSkillsListbox.TabIndex = 16;
            this.PetSkillsListbox.SelectedIndexChanged += new System.EventHandler(this.PetSkillsListbox_SelectedIndexChanged);
            // 
            // PetSkillCurrentValueLabel
            // 
            this.PetSkillCurrentValueLabel.AutoSize = true;
            this.PetSkillCurrentValueLabel.Location = new System.Drawing.Point(258, 192);
            this.PetSkillCurrentValueLabel.Name = "PetSkillCurrentValueLabel";
            this.PetSkillCurrentValueLabel.Size = new System.Drawing.Size(71, 13);
            this.PetSkillCurrentValueLabel.TabIndex = 18;
            this.PetSkillCurrentValueLabel.Text = "Current Value";
            // 
            // PetSkillPossibleValuesLabel
            // 
            this.PetSkillPossibleValuesLabel.AutoSize = true;
            this.PetSkillPossibleValuesLabel.Location = new System.Drawing.Point(258, 205);
            this.PetSkillPossibleValuesLabel.Name = "PetSkillPossibleValuesLabel";
            this.PetSkillPossibleValuesLabel.Size = new System.Drawing.Size(81, 13);
            this.PetSkillPossibleValuesLabel.TabIndex = 19;
            this.PetSkillPossibleValuesLabel.Text = "Possible Values";
            // 
            // PetSkillUpgradeCostLabel
            // 
            this.PetSkillUpgradeCostLabel.AutoSize = true;
            this.PetSkillUpgradeCostLabel.Location = new System.Drawing.Point(258, 218);
            this.PetSkillUpgradeCostLabel.Name = "PetSkillUpgradeCostLabel";
            this.PetSkillUpgradeCostLabel.Size = new System.Drawing.Size(72, 13);
            this.PetSkillUpgradeCostLabel.TabIndex = 20;
            this.PetSkillUpgradeCostLabel.Text = "Upgrade Cost";
            // 
            // UpgradeSkillButton
            // 
            this.UpgradeSkillButton.Location = new System.Drawing.Point(652, 40);
            this.UpgradeSkillButton.Name = "UpgradeSkillButton";
            this.UpgradeSkillButton.Size = new System.Drawing.Size(85, 23);
            this.UpgradeSkillButton.TabIndex = 21;
            this.UpgradeSkillButton.Text = "Upgrade Skill";
            this.UpgradeSkillButton.UseVisualStyleBackColor = true;
            this.UpgradeSkillButton.Click += new System.EventHandler(this.UpgradeSkillButton_Click);
            // 
            // FeedPetButton
            // 
            this.FeedPetButton.Location = new System.Drawing.Point(652, 69);
            this.FeedPetButton.Name = "FeedPetButton";
            this.FeedPetButton.Size = new System.Drawing.Size(85, 23);
            this.FeedPetButton.TabIndex = 22;
            this.FeedPetButton.Text = "Feed Pet";
            this.FeedPetButton.UseVisualStyleBackColor = true;
            this.FeedPetButton.Click += new System.EventHandler(this.FeedPetButton_Click);
            // 
            // TakeGoldButton
            // 
            this.TakeGoldButton.Location = new System.Drawing.Point(652, 98);
            this.TakeGoldButton.Name = "TakeGoldButton";
            this.TakeGoldButton.Size = new System.Drawing.Size(85, 23);
            this.TakeGoldButton.TabIndex = 23;
            this.TakeGoldButton.Text = "Take Gold";
            this.TakeGoldButton.UseVisualStyleBackColor = true;
            this.TakeGoldButton.Click += new System.EventHandler(this.TakeGoldButton_Click);
            // 
            // SellItemButton
            // 
            this.SellItemButton.Location = new System.Drawing.Point(652, 156);
            this.SellItemButton.Name = "SellItemButton";
            this.SellItemButton.Size = new System.Drawing.Size(85, 23);
            this.SellItemButton.TabIndex = 24;
            this.SellItemButton.Text = "Sell Item";
            this.SellItemButton.UseVisualStyleBackColor = true;
            this.SellItemButton.Click += new System.EventHandler(this.SellItemButton_Click);
            // 
            // TakeItemButton
            // 
            this.TakeItemButton.Location = new System.Drawing.Point(652, 127);
            this.TakeItemButton.Name = "TakeItemButton";
            this.TakeItemButton.Size = new System.Drawing.Size(85, 23);
            this.TakeItemButton.TabIndex = 25;
            this.TakeItemButton.Text = "Take Item";
            this.TakeItemButton.UseVisualStyleBackColor = true;
            this.TakeItemButton.Click += new System.EventHandler(this.TakeItemButton_Click);
            // 
            // EquipItemButton
            // 
            this.EquipItemButton.Location = new System.Drawing.Point(652, 185);
            this.EquipItemButton.Name = "EquipItemButton";
            this.EquipItemButton.Size = new System.Drawing.Size(85, 23);
            this.EquipItemButton.TabIndex = 26;
            this.EquipItemButton.Text = "Equip Item";
            this.EquipItemButton.UseVisualStyleBackColor = true;
            this.EquipItemButton.Click += new System.EventHandler(this.EquipItemButton_Click);
            // 
            // UnequipItemButton
            // 
            this.UnequipItemButton.Location = new System.Drawing.Point(652, 214);
            this.UnequipItemButton.Name = "UnequipItemButton";
            this.UnequipItemButton.Size = new System.Drawing.Size(85, 23);
            this.UnequipItemButton.TabIndex = 27;
            this.UnequipItemButton.Text = "Unequip Item";
            this.UnequipItemButton.UseVisualStyleBackColor = true;
            this.UnequipItemButton.Click += new System.EventHandler(this.UnequipItemButton_Click);
            // 
            // PetTabPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.UnequipItemButton);
            this.Controls.Add(this.EquipItemButton);
            this.Controls.Add(this.TakeItemButton);
            this.Controls.Add(this.SellItemButton);
            this.Controls.Add(this.TakeGoldButton);
            this.Controls.Add(this.FeedPetButton);
            this.Controls.Add(this.UpgradeSkillButton);
            this.Controls.Add(this.PetSkillUpgradeCostLabel);
            this.Controls.Add(this.PetSkillPossibleValuesLabel);
            this.Controls.Add(this.PetSkillCurrentValueLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PetSkillsListbox);
            this.Controls.Add(this.ActivatePetButton);
            this.Controls.Add(this.PetNextItemLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PetEquipmentListbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PetInventoryListbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PetGoldLabel);
            this.Controls.Add(this.PetXpLabel);
            this.Controls.Add(this.PetMpLabel);
            this.Controls.Add(this.PetHpLabel);
            this.Controls.Add(this.PetTypeLabel);
            this.Controls.Add(this.PetNameLabel);
            this.Controls.Add(this.PetListbox);
            this.Name = "PetTabPage";
            this.Size = new System.Drawing.Size(776, 536);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox PetListbox;
        private System.Windows.Forms.Label PetNameLabel;
        private System.Windows.Forms.Label PetTypeLabel;
        private System.Windows.Forms.Label PetHpLabel;
        private System.Windows.Forms.Label PetMpLabel;
        private System.Windows.Forms.Label PetXpLabel;
        private System.Windows.Forms.Label PetGoldLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox PetInventoryListbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox PetEquipmentListbox;
        private System.Windows.Forms.Label PetNextItemLabel;
        private System.Windows.Forms.Button ActivatePetButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox PetSkillsListbox;
        private System.Windows.Forms.Label PetSkillCurrentValueLabel;
        private System.Windows.Forms.Label PetSkillPossibleValuesLabel;
        private System.Windows.Forms.Label PetSkillUpgradeCostLabel;
        private System.Windows.Forms.Button UpgradeSkillButton;
        private System.Windows.Forms.Button FeedPetButton;
        private System.Windows.Forms.Button TakeGoldButton;
        private System.Windows.Forms.Button SellItemButton;
        private System.Windows.Forms.Button TakeItemButton;
        private System.Windows.Forms.Button EquipItemButton;
        private System.Windows.Forms.Button UnequipItemButton;
    }
}
