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
using System.Reflection;
using IdleLandsGUI.Model.Pets;

namespace IdleLandsGUI.Tabs
{
    public partial class PetTabPage : UserControl
    {
        private IdleLandsComms _comms { get; set; }
        private PetInfo _activePet { get; set; }
        private PetInfo _selectedPet { get; set; }
        private List<PetInfo> _pets { get; set; }
        private string _selectedSkill { get; set; }
        private int _selectedInventory { get; set; }
        private ulong? _selectedEquipment { get; set; }
        public PetTabPage(IdleLandsComms.PetResponse response, IdleLandsComms comms)
        {
            InitializeComponent();
            _comms = comms;
            comms.AddPetUpdateDelegate(UpdatePetInfo);
            Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);

            PetInventoryListbox.DisplayMember = "Text";
            PetEquipmentListbox.DisplayMember = "Text";

            UpgradeSkillButton.Enabled = false;
            SellItemButton.Enabled = false;
            TakeItemButton.Enabled = false;
            EquipItemButton.Enabled = false;
            UnequipItemButton.Enabled = false;
            ChangeClassButton.Enabled = false;

            UpdatePetInfo(response);
            if(response.pets != null && response.pets.Any())
                PetListbox.SetSelected(0, true);
        }

        //TODO Refactor into updatePet and updatePets
        public void UpdatePetInfo(IdleLandsComms.PetResponse response)
        {
            //Some of the responses can be null, even though they're normally filled.
            if (response.pet == null && response.pets == null)
                return;

            if (response.pet != null)
            {
                _activePet = response.pet;
                SmartSelfButton.Text = "Smart Self: " + (_activePet.smartSelf == "on" ? "ON" : "OFF");
                SmartEquipButton.Text = "Smart Equip: " + (_activePet.smartEquip == "on" ? "ON" : "OFF");
                SmartSellButton.Text = "Smart Sell: " + (_activePet.smartSell == "on" ? "ON" : "OFF");
                if (_selectedPet != null && _activePet.createdAt == _selectedPet.createdAt)
                {
                    _selectedPet = _activePet;
                    UpdateSelectedPetStats(_selectedPet);
                }
                ChangeClassButton.Enabled = true;
            }

            if (response.pets != null)
            {
                PetListbox.Items.Clear();
                _pets = response.pets;

                foreach (var pet in response.pets)
                {
                    if (response.pet != null && pet.name == response.pet.name)
                    {
                        PetListbox.Items.Add(pet.name + " (Active)");
                    }
                    else
                    {
                        PetListbox.Items.Add(pet.name);
                    }
                }
            }
                
            PetSkillsListbox_SelectedIndexChanged(null, null);
        }

        private PetInfo GetSelectedPet()
        {
            if (_pets == null || !_pets.Any())
                return null;

            if (PetListbox.SelectedItem == null)
                return null;

            string name = (string)PetListbox.SelectedItem;
            if (name.LastIndexOf("(Active)") != -1)
                name = name.Substring(0, name.LastIndexOf("(Active)") - 1);

            return _pets.SingleOrDefault(p => p.name == name);
        }

        private void UpdateSelectedPetStats(PetInfo pet)
        {
            PetNameLabel.Text = "Name: " + pet.name;
            if (pet.attrs != null && pet.attrs.Any())
            {
                PetNameLabel.Text += " with " + pet.attrs[0];
                if (pet.attrs.Count == 2)
                    PetNameLabel.Text += " and " + pet.attrs[1];
            }
            PetTypeLabel.Text = "Type: " + pet.type;
            PetHpLabel.Text = "HP: " + pet.hp.__current + "/" + pet.hp.maximum;
            PetMpLabel.Text = "MP: " + pet.mp.__current + "/" + pet.mp.maximum;
            PetXpLabel.Text = "XP: " + pet.xp.__current + "/" + pet.xp.maximum;
            PetGoldLabel.Text = "Gold: " + pet.gold.__current + "/" + pet.gold.maximum;
            PetNextItemLabel.Text = "Next Item: " + pet.nextItemFind;

            List<ListboxData> data = new List<ListboxData>();
            foreach (var item in pet.inventory)
            {
                data.Add(new ListboxData { Text = item.name, Uid = item.uid });
            }
            PetInventoryListbox.DataSource = data;

            if(!pet.inventory.Any())
            {
                TakeItemButton.Enabled = false;
                SellItemButton.Enabled = false;
                EquipItemButton.Enabled = false;
            }

            data = new List<ListboxData>();
            foreach (var item in pet.equipment)
            {
                data.Add(new ListboxData { Text = item.name, Uid = item.uid });
            }
            PetEquipmentListbox.DataSource = data;

            if(!pet.equipment.Any())
            {
                UnequipItemButton.Enabled = false;
            }

            Type petInfoType = pet.scaleLevel.GetType();
            PetSkillsListbox.Items.Clear();
            foreach (PropertyInfo propertyInfo in petInfoType.GetProperties())
            {
                PetSkillsListbox.Items.Add(propertyInfo.Name);
            }

            if (_activePet != null && pet.createdAt == _activePet.createdAt)
            {
                ActivatePetButton.Enabled = false;
            }
            else
            {
                ActivatePetButton.Enabled = true;
            }
        }

        private void PetListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            PetInfo pet = GetSelectedPet();

            if (pet == null)
                return;

            _selectedPet = pet;

            UpdateSelectedPetStats(pet);
        }

        private void ActivatePetButton_Click(object sender, EventArgs e)
        {
            ActivatePetButton.Enabled = false;
            PetInfo pet = GetSelectedPet();
            if (pet == null)
                return;
            _comms.SendSwapPet(pet.createdAt, null, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void PetSkillsListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PetSkillsListbox.SelectedItem == null && _selectedPet == null)
            {
                UpgradeSkillButton.Enabled = false;
            }
            else
            {
                if (PetSkillsListbox.SelectedItem != null)
                {
                    _selectedSkill = (string)PetSkillsListbox.SelectedItem;
                    UpgradeSkillButton.Enabled = true;
                }
                //
                Type petScaleLevelInfoType = _selectedPet.scaleLevel.GetType();
                foreach (PropertyInfo propertyInfo in petScaleLevelInfoType.GetProperties())
                {
                    if(propertyInfo.Name == _selectedSkill)
                    {
                        int level = (int)propertyInfo.GetValue(_selectedPet.scaleLevel);

                        Type petScaleInfoType = _selectedPet._configCache.scale.GetType();
                        foreach (PropertyInfo propertyInfo2 in petScaleInfoType.GetProperties())
                        {
                            if (propertyInfo2.Name == _selectedSkill)
                            {
                                PetSkillCurrentNameLabel.Text = "Current Name: " + _selectedSkill;
                                IEnumerable<int> intListVal = propertyInfo2.GetValue(_selectedPet._configCache.scale) as List<int>;
                                IEnumerable<float> floatListVal = propertyInfo2.GetValue(_selectedPet._configCache.scale) as List<float>;

                                if (intListVal != null)
                                {
                                    intListVal = intListVal.OrderBy(x => x);
                                    PetSkillCurrentValueLabel.Text = "Current Value: " + intListVal.ElementAt(level);
                                    PetSkillPossibleValuesLabel.Text = "Possible Values: [" + String.Join(",", intListVal) + "]";
                                }
                                else
                                {
                                    floatListVal = floatListVal.OrderBy(x => x);
                                    PetSkillCurrentValueLabel.Text = "Current Value: " + floatListVal.ElementAt(level);
                                    PetSkillPossibleValuesLabel.Text = "Possible Values: [" + String.Join(",", floatListVal) + "]";
                                }
                            }
                        }


                        Type petScaleCostInfoType = _selectedPet._configCache.scaleCost.GetType();
                        foreach (PropertyInfo propertyInfo3 in petScaleCostInfoType.GetProperties())
                        {
                            if (propertyInfo3.Name == _selectedSkill)
                            {
                                List<int> upgradeVals = propertyInfo3.GetValue(_selectedPet._configCache.scaleCost) as List<int>;
                                if(level + 1 >= upgradeVals.Count())
                                    PetSkillUpgradeCostLabel.Text = "Upgrade Cost: Max Level Reached";
                                else
                                    PetSkillUpgradeCostLabel.Text = "Upgrade Cost: " + upgradeVals.OrderBy(x => x).ElementAt(level+1);
                            }
                        }

                        break;
                    }
                }
            }
        }

        private void UpgradeSkillButton_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(_selectedSkill))
            {
                UpgradeSkillButton.Enabled = false;
                _comms.SendUpgradePet(_selectedSkill, () =>
                {
                    UpgradeSkillButton.Enabled = true;
                    return true;
                }, (string msg, int code) =>
                {
                    MessageBox.Show(code + ": " + msg);
                    return true;
                });
            }
        }

        private void FeedPetButton_Click(object sender, EventArgs e)
        {
            FeedPetButton.Enabled = false;
            _comms.SendFeedPet(() =>
            {
                FeedPetButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void TakeGoldButton_Click(object sender, EventArgs e)
        {
            TakeGoldButton.Enabled = false;
            _comms.SendTakeGoldPet(() =>
            {
                TakeGoldButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void PetInventoryListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedInventory = PetInventoryListbox.SelectedIndex;
            if (_selectedInventory >= 0)
            {
                SellItemButton.Enabled = true;
                TakeItemButton.Enabled = true;
                EquipItemButton.Enabled = true;
            }
            else
            {
                SellItemButton.Enabled = false;
                TakeItemButton.Enabled = false;
                EquipItemButton.Enabled = false;
            }
        }

        private void PetEquipmentListbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(PetEquipmentListbox.SelectedItem == null)
            {
                _selectedEquipment = null;
                UnequipItemButton.Enabled = false;
                return;
            }
            UnequipItemButton.Enabled = true;
            ListboxData data = PetEquipmentListbox.SelectedItem as ListboxData;
            _selectedEquipment = data.Uid;
        }

        private void SellItemButton_Click(object sender, EventArgs e)
        {
            if(_selectedInventory >= 0)
            {
                SellItemButton.Enabled = false;
                _comms.SendInventorySellPet(_selectedInventory, () =>
                {
                    SellItemButton.Enabled = true;
                    return true;
                }, (string msg, int code) =>
                {
                    MessageBox.Show(code + ": " + msg);
                    return true;
                });
            }
        }

        private void TakeItemButton_Click(object sender, EventArgs e)
        {
            if (_selectedInventory >= 0)
            {
                TakeItemButton.Enabled = false;
                _comms.SendInventoryTakePet(_selectedInventory, () =>
                {
                    TakeItemButton.Enabled = true;
                    return true;
                }, (string msg, int code) =>
                {
                    MessageBox.Show(code + ": " + msg);
                    return true;
                });
            }
        }

        private void EquipItemButton_Click(object sender, EventArgs e)
        {
            if (_selectedInventory >= 0)
            {
                EquipItemButton.Enabled = false;
                _comms.SendInventoryEquipPet(_selectedInventory, () =>
                {
                    EquipItemButton.Enabled = true;
                    return true;
                }, (string msg, int code) =>
                {
                    MessageBox.Show(code + ": " + msg);
                    return true;
                });
            }
        }

        private void UnequipItemButton_Click(object sender, EventArgs e)
        {
            if (_selectedEquipment.HasValue)
            {
                UnequipItemButton.Enabled = false;
                _comms.SendInventoryUnequipPet(_selectedEquipment.Value, () =>
                {
                    UnequipItemButton.Enabled = true;
                    return true;
                }, (string msg, int code) =>
                {
                    MessageBox.Show(code + ": " + msg);
                    return true;
                });
            }
        }

        private void ChangeClassButton_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(ChangeClassTextbox.Text))
            {
                MessageBox.Show("Can't change class to empty!");
                return;
            }

            ChangeClassButton.Enabled = false;
            _comms.SendChangeClassPet(ChangeClassTextbox.Text, () =>
            {
                ChangeClassButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void SmartSelfButton_Click(object sender, EventArgs e)
        {
            if (_activePet == null)
                return;

            SmartSelfButton.Enabled = false;
            _comms.SendSmartPet("smartSelf", _activePet.smartSelf == "on" ? "off" : "on", () =>
            {
                SmartSelfButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void SmartEquipButton_Click(object sender, EventArgs e)
        {
            if (_activePet == null)
                return;

            SmartEquipButton.Enabled = false;
            _comms.SendSmartPet("smartEquip", _activePet.smartEquip == "on" ? "off" : "on", () =>
            {
                SmartEquipButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private void SmartSellButton_Click(object sender, EventArgs e)
        {
            if (_activePet == null)
                return;

            SmartSellButton.Enabled = false;
            _comms.SendSmartPet("smartSell", _activePet.smartSell == "on" ? "off" : "on", () =>
            {
                SmartSellButton.Enabled = true;
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
        }

        private class ListboxData
        {
            public string Text { get; set; }
            public ulong Uid { get; set; }
        }
    }
}
