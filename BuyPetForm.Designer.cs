namespace IdleLandsGUI
{
    partial class BuyPetForm
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
            this.PetTypeLabel = new System.Windows.Forms.Label();
            this.PetCostLabel = new System.Windows.Forms.Label();
            this.PetNameInputLabel = new System.Windows.Forms.Label();
            this.FirstAttributeLabel = new System.Windows.Forms.Label();
            this.PetNameTextbox = new System.Windows.Forms.TextBox();
            this.PetFirstAttributeTextbox = new System.Windows.Forms.TextBox();
            this.PetSecondAttributeTextbox = new System.Windows.Forms.TextBox();
            this.SecondAttributeLabel = new System.Windows.Forms.Label();
            this.CancelBuyButton = new System.Windows.Forms.Button();
            this.BuyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PetTypeLabel
            // 
            this.PetTypeLabel.AutoSize = true;
            this.PetTypeLabel.Location = new System.Drawing.Point(13, 13);
            this.PetTypeLabel.Name = "PetTypeLabel";
            this.PetTypeLabel.Size = new System.Drawing.Size(50, 13);
            this.PetTypeLabel.TabIndex = 0;
            this.PetTypeLabel.Text = "Pet Type";
            // 
            // PetCostLabel
            // 
            this.PetCostLabel.AutoSize = true;
            this.PetCostLabel.Location = new System.Drawing.Point(13, 26);
            this.PetCostLabel.Name = "PetCostLabel";
            this.PetCostLabel.Size = new System.Drawing.Size(47, 13);
            this.PetCostLabel.TabIndex = 1;
            this.PetCostLabel.Text = "Pet Cost";
            // 
            // PetNameInputLabel
            // 
            this.PetNameInputLabel.AutoSize = true;
            this.PetNameInputLabel.Location = new System.Drawing.Point(13, 58);
            this.PetNameInputLabel.Name = "PetNameInputLabel";
            this.PetNameInputLabel.Size = new System.Drawing.Size(38, 13);
            this.PetNameInputLabel.TabIndex = 2;
            this.PetNameInputLabel.Text = "Name:";
            // 
            // FirstAttributeLabel
            // 
            this.FirstAttributeLabel.AutoSize = true;
            this.FirstAttributeLabel.Location = new System.Drawing.Point(13, 84);
            this.FirstAttributeLabel.Name = "FirstAttributeLabel";
            this.FirstAttributeLabel.Size = new System.Drawing.Size(32, 13);
            this.FirstAttributeLabel.TabIndex = 3;
            this.FirstAttributeLabel.Text = "Attr1:";
            // 
            // PetNameTextbox
            // 
            this.PetNameTextbox.Location = new System.Drawing.Point(57, 55);
            this.PetNameTextbox.Name = "PetNameTextbox";
            this.PetNameTextbox.Size = new System.Drawing.Size(100, 20);
            this.PetNameTextbox.TabIndex = 4;
            // 
            // PetFirstAttributeTextbox
            // 
            this.PetFirstAttributeTextbox.Location = new System.Drawing.Point(57, 81);
            this.PetFirstAttributeTextbox.Name = "PetFirstAttributeTextbox";
            this.PetFirstAttributeTextbox.Size = new System.Drawing.Size(100, 20);
            this.PetFirstAttributeTextbox.TabIndex = 5;
            // 
            // PetSecondAttributeTextbox
            // 
            this.PetSecondAttributeTextbox.Location = new System.Drawing.Point(57, 107);
            this.PetSecondAttributeTextbox.Name = "PetSecondAttributeTextbox";
            this.PetSecondAttributeTextbox.Size = new System.Drawing.Size(100, 20);
            this.PetSecondAttributeTextbox.TabIndex = 6;
            // 
            // SecondAttributeLabel
            // 
            this.SecondAttributeLabel.AutoSize = true;
            this.SecondAttributeLabel.Location = new System.Drawing.Point(13, 110);
            this.SecondAttributeLabel.Name = "SecondAttributeLabel";
            this.SecondAttributeLabel.Size = new System.Drawing.Size(32, 13);
            this.SecondAttributeLabel.TabIndex = 7;
            this.SecondAttributeLabel.Text = "Attr2:";
            // 
            // CancelBuyButton
            // 
            this.CancelBuyButton.Location = new System.Drawing.Point(94, 145);
            this.CancelBuyButton.Name = "CancelBuyButton";
            this.CancelBuyButton.Size = new System.Drawing.Size(75, 23);
            this.CancelBuyButton.TabIndex = 8;
            this.CancelBuyButton.Text = "Cancel";
            this.CancelBuyButton.UseVisualStyleBackColor = true;
            this.CancelBuyButton.Click += new System.EventHandler(this.CancelBuyButton_Click);
            // 
            // BuyButton
            // 
            this.BuyButton.Location = new System.Drawing.Point(13, 145);
            this.BuyButton.Name = "BuyButton";
            this.BuyButton.Size = new System.Drawing.Size(75, 23);
            this.BuyButton.TabIndex = 9;
            this.BuyButton.Text = "Buy";
            this.BuyButton.UseVisualStyleBackColor = true;
            this.BuyButton.Click += new System.EventHandler(this.BuyButton_Click);
            // 
            // BuyPetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(191, 193);
            this.Controls.Add(this.BuyButton);
            this.Controls.Add(this.CancelBuyButton);
            this.Controls.Add(this.SecondAttributeLabel);
            this.Controls.Add(this.PetSecondAttributeTextbox);
            this.Controls.Add(this.PetFirstAttributeTextbox);
            this.Controls.Add(this.PetNameTextbox);
            this.Controls.Add(this.FirstAttributeLabel);
            this.Controls.Add(this.PetNameInputLabel);
            this.Controls.Add(this.PetCostLabel);
            this.Controls.Add(this.PetTypeLabel);
            this.Name = "BuyPetForm";
            this.Text = "BuyPetForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label PetTypeLabel;
        private System.Windows.Forms.Label PetCostLabel;
        private System.Windows.Forms.Label PetNameInputLabel;
        private System.Windows.Forms.Label FirstAttributeLabel;
        private System.Windows.Forms.TextBox PetNameTextbox;
        private System.Windows.Forms.TextBox PetFirstAttributeTextbox;
        private System.Windows.Forms.TextBox PetSecondAttributeTextbox;
        private System.Windows.Forms.Label SecondAttributeLabel;
        private System.Windows.Forms.Button CancelBuyButton;
        private System.Windows.Forms.Button BuyButton;
    }
}