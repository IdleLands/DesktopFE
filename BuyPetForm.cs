using IdleLandsGUI.Properties;
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
    public partial class BuyPetForm : Form
    {
        private IdleLandsComms _comms { get; set; }
        private string _petType { get; set; }
        public BuyPetForm(IdleLandsComms comms, string petType, string petCost)
        {
            _comms = comms;
            _petType = petType;
            InitializeComponent();
            this.Icon = Icon.FromHandle(Resources.IdleLandsIcon.GetHicon());

            PetTypeLabel.Text = "Pet Type: " + petType;
            PetCostLabel.Text = "Pet Cost: " + petCost;
        }

        private void CancelBuyButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BuyButton_Click(object sender, EventArgs e)
        {
            if(!PetNameTextbox.Text.Any())
            {
                MessageBox.Show("Name cannot be empty.");
                return;
            }

            PetNameTextbox.Enabled = false;
            PetFirstAttributeTextbox.Enabled = false;
            PetSecondAttributeTextbox.Enabled = false;
            BuyButton.Enabled = false;
            CancelBuyButton.Enabled = false;

            _comms.SendBuyPet(_petType, PetNameTextbox.Text,
                new List<string> { PetFirstAttributeTextbox.Text, PetSecondAttributeTextbox.Text }, () =>
            {
                this.Close();
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }
    }
}
