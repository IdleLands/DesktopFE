using IdleLandsGUI.CustomAttributes;
using IdleLandsGUI.Model;
using IdleLandsGUI.Model.Pets;
using IdleLandsGUI.Properties;
using IdleLandsGUI.Tabs;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdleLandsGUI
{
    public partial class MainForm : Form
    {
        private PlayerInfo _player;
        private Dictionary<String, Control> _playerControls;
        private IdleLandsComms _comms { get; set; }
        public MainForm(IdleLandsComms.ActionResponse response, IdleLandsComms comms)
        {
            InitializeComponent();
            this.Icon = Icon.FromHandle(Resources.IdleLandsIcon.GetHicon());

            _playerControls = new Dictionary<string, Control>();

            CreateGui(response.player);
            UpdatePlayer(response.player);

            this.FormClosing += MainForm_FormClosing;
            _comms = comms;
            comms.AddPlayerUpdateDelegate(UpdatePlayer);

            IdleLandsComms.PetResponse petResponse = new IdleLandsComms.PetResponse
            {
                pet = response.pet,
                pets = response.pets
            };

            PetTab.Controls.Add(new PetTabPage(petResponse, comms));
            GuildTab.Controls.Add(new GuildTabPage(response.player, response.guild, comms));
        }

        public void UpdatePlayer(PlayerInfo info)
        {
            if (info.foundPets != null)
            {
                var dict = JsonConvert.DeserializeObject<Dictionary<string, FoundPetInfo>>(info.foundPets);
                info.workaroundPets = dict.Select(k => new FoundPetInfo
                {
                    name = k.Key,
                    cost = k.Value.cost,
                    purchaseDate = k.Value.purchaseDate,
                    unlockDate = k.Value.unlockDate
                }).ToList();
            } 
            else
            {
                info.workaroundPets = new List<FoundPetInfo>();
            }
            _player = info;
            UpdateGui(info);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show(this, "Really?", "Closing...",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    e.Cancel = true;
                else
                {
                    _comms.Logout(() => {Application.Exit(); return true;});
                }
            }
        }

        private void CreateGui(PlayerInfo info)
        {
            Type playerInfoType = info.GetType();

            int x = 15;
            int y = 0;

            OtherTextBox.Enabled = false;
            if (info.gender.ToLower() == "male")
            {
                MaleRadioButton.Select();
            }
            else if (info.gender.ToLower() == "female")
            {
                FemaleRadioButton.Select();
            }
            else
            {
                OtherRadioButton.Select();
                OtherTextBox.Text = info.gender;
                OtherTextBox.Enabled = true;
            }

            //Dynamic stuff
            foreach (PropertyInfo propertyInfo in playerInfoType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var labelAttrib = GetAttribute<NotAGuiElementAttribute>(propertyInfo);
                    if (labelAttrib != null)
                        continue;

                    object val = propertyInfo.GetValue(info);

                    if (val == null)
                        continue;

                    StatInfo statVal = val as StatInfo;
                    List<EquipmentInfo> equipmentVal = val as List<EquipmentInfo>;
                    List<IdleLandsGUI.Model.EventInfo> eventVal = val as List<IdleLandsGUI.Model.EventInfo>;
                    StatCacheInfo statCacheVal = val as StatCacheInfo;
                    PriorityPointsInfo priorityPointsVal = val as PriorityPointsInfo;
                    if (statVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "PlayerInfoTab");
                        var iconAttrib = GetAttribute<IconElementAttribute>(propertyInfo);

                        if (iconAttrib != null)
                        {
                            int tempX = x;
                            if (!string.IsNullOrEmpty(iconAttrib.Name))
                            {
                                var control = AddIcon(tab, x, y, propertyInfo.Name + "_stat_icon", iconAttrib.Name, Color.FromName(iconAttrib.Colour));
                                new ToolTip().SetToolTip(control, propertyInfo.Name);
                                tempX += 20;
                            }

                            if (iconAttrib.HideMax)
                            {
                                AddLabel(tab, tempX, y, propertyInfo.Name + "_stat", statVal.__current.ToString());
                            }
                            else
                            {
                                AddLabel(tab, tempX, y, propertyInfo.Name + "_stat", statVal.__current + "/" + statVal.maximum);
                            }
                            y += 15;
                            if (y + 50 > tab.Size.Height)
                            {
                                y = 0;
                                x += 200;
                            }
                        }
                        else
                        {
                            AddLabel(tab, x, y, propertyInfo.Name + "_stat", propertyInfo.Name + ": " + statVal.__current + "/" + statVal.maximum);
                            y += 15;
                            if (y + 50 > tab.Size.Height)
                            {
                                y = 0;
                                x += 200;
                            }
                        }
                    }
                    else if (equipmentVal != null)
                    {
                        TabPage tab = null;
                        string controlNameType = "_equipment";
                        if (propertyInfo.Name == "overflow")
                        {
                            tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "InventoryTab");
                            controlNameType = "_overflow";
                            continue;
                        }
                        else
                        {
                            tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "EquipmentTab");
                        }
                        int x2 = 15, y2 = 0;
                        foreach (EquipmentInfo item in equipmentVal)
                        {
                            AddLabel(tab, x2, y2, item.type + controlNameType + "_label", item.type + ": " + item.name);

                            AddButton(tab, 600, y2, item.type + controlNameType + "_button", "Unequip", 0, (Button button, int slotNo) =>
                            {
                                button.Enabled = false;
                                _comms.InventoryAdd(item.type, () =>
                                {
                                    button.Enabled = true;
                                    return true;
                                }, (string msg, int code) =>
                                {
                                    MessageBox.Show(code + ": " + msg);
                                    return true;
                                });
                                return true;
                            });

                            int x3 = x2;

                            foreach (PropertyInfo itemStatPropertyInfo in item.GetType().GetProperties())
                            {
                                var iconAttrib = GetAttribute<IconElementAttribute>(itemStatPropertyInfo);
                                var equipAttrib = GetAttribute<EquipmentStatAttribute>(itemStatPropertyInfo);

                                if (iconAttrib != null)
                                {
                                    CreateItemGui(iconAttrib, tab, x3, y2, item.type + controlNameType + "_" + itemStatPropertyInfo.Name + controlNameType,
                                        itemStatPropertyInfo.GetValue(item, null).ToString(), itemStatPropertyInfo.Name);

                                    x3 += 100;
                                    if (x3 > 500)
                                    {
                                        y2 += 20;
                                        x3 = x2;
                                    }
                                }
                                else if (equipAttrib != null)
                                {
                                    CreateItemGui(iconAttrib, tab, x3, y2, item.type + controlNameType + "_" + equipAttrib.BelongsTo + controlNameType,
                                        itemStatPropertyInfo.GetValue(item, null).ToString(), "");
                                }
                            }

                            y2 += 40;
                        }
                    }
                    else if (statCacheVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "PlayerInfoTab");
                        int x2 = x + 250, y2 = 0;
                        foreach (PropertyInfo statCachePropertyInfo in statCacheVal.GetType().GetProperties())
                        {
                            var iconAttrib = GetAttribute<IconElementAttribute>(statCachePropertyInfo);

                            if (iconAttrib != null)
                            {
                                var control = AddIcon(tab, x2, y2, statCachePropertyInfo.Name + "_statcache_icon", iconAttrib.Name, Color.FromName(iconAttrib.Colour));
                                AddLabel(tab, x2 + 20, y2, statCachePropertyInfo.Name + "_statcache", statCachePropertyInfo.GetValue(statCacheVal, null).ToString());

                                new ToolTip().SetToolTip(control, statCachePropertyInfo.Name);
                            }

                            y2 += 15;
                            if (y2 + 50 > tab.Size.Height)
                            {
                                y2 = 0;
                                x2 += 200;
                            }
                        }
                    }
                    else if(priorityPointsVal != null)
                    {
                        StrTrackBar.Value = priorityPointsVal.str;
                        ConTrackBar.Value = priorityPointsVal.con;
                        DexTrackBar.Value = priorityPointsVal.dex;
                        AgiTrackBar.Value = priorityPointsVal.agi;
                        WisTrackBar.Value = priorityPointsVal.wis;
                        IntTrackBar.Value = priorityPointsVal._int;
                    }
                    else if (val.GetType() == typeof(string))
                    {
                        TabPage tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "PlayerInfoTab");
                        AddLabel(tab, x, y, propertyInfo.Name + "_val", propertyInfo.Name + ": " + ((String)val));
                        y += 15;
                        if (y + 50 > tab.Size.Height)
                        {
                            y = 0;
                            x += 200;
                        }
                    }
                }
            }
        }

        private void UpdateGui(PlayerInfo info)
        {
            Type playerInfoType = info.GetType();

            //dynamic stuff
            foreach (PropertyInfo propertyInfo in playerInfoType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var labelAttrib = GetAttribute<NotAGuiElementAttribute>(propertyInfo);
                    if (labelAttrib != null)
                        continue;

                    Control tempControl = null;
                    object val = propertyInfo.GetValue(info);

                    if (val == null)
                        continue;

                    StatInfo statVal = val as StatInfo;
                    List<EquipmentInfo> equipmentVal = val as List<EquipmentInfo>;
                    List<IdleLandsGUI.Model.EventInfo> eventVal = val as List<IdleLandsGUI.Model.EventInfo>;
                    StatCacheInfo statCacheVal = val as StatCacheInfo;
                    List<FoundPetInfo> workaroundPetsVal = val as List<FoundPetInfo>;
                    if (statVal != null)
                    {
                        if (_playerControls.TryGetValue(propertyInfo.Name + "_stat", out tempControl))
                        {
                            var iconAttrib = GetAttribute<IconElementAttribute>(propertyInfo);
                            if (iconAttrib == null || string.IsNullOrEmpty(iconAttrib.Name))
                            {
                                string text = propertyInfo.Name + ": " + statVal.__current;
                                if (iconAttrib == null || !iconAttrib.HideMax)
                                    text += "/" + statVal.maximum;
                                tempControl.Text = text;
                            }
                            else if(iconAttrib.HideMax)
                            {
                                tempControl.Text = statVal.__current.ToString();
                            }
                            else
                            {
                                tempControl.Text = statVal.__current + "/" + statVal.maximum;
                            }
                        }
                    }
                    else if (equipmentVal != null)
                    {
                        string controlNameType = "_equipment";
                        TabPage tab = null;
                        if (propertyInfo.Name == "overflow")
                        {
                            controlNameType = "_overflow";
                            List<Control> controls = _playerControls.RemoveAllKeys(x => x.Contains("_overflow"));
                            tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "InventoryTab");
                            foreach(Control control in controls)
                            {
                                tab.Controls.Remove(control);
                            }
                        }

                        if (!equipmentVal.Any() || equipmentVal.All(x => x == null))
                            continue;

                        int slotNo = 0, x2 = 15, y = 0;
                        foreach (EquipmentInfo item in equipmentVal)
                        {
                            if (propertyInfo.Name == "overflow")
                            {
                                if (item == null || item.uid == 0)
                                    continue;

                                AddLabel(tab, x2, y, item.uid + controlNameType + "_label", item.type + ": " + item.name);

                                AddButton(tab, 600, y, slotNo + controlNameType + "_button1", "Equip", slotNo, (Button button, int buttonSlot) =>
                                {
                                    _comms.InventorySwap(buttonSlot.ToString(), () =>
                                    {
                                        button.Enabled = true;
                                        return true;
                                    }, (string msg, int code) =>
                                    {
                                        MessageBox.Show(code + ": " + msg);
                                        return true;
                                    });
                                    button.Enabled = false;
                                    return true;
                                });
                                AddButton(tab, 600, y + 35, slotNo + controlNameType + "_button2", "Sell", slotNo, (Button button, int buttonSlot) =>
                                {
                                    if (MessageBox.Show(this, "Sell " + item.name + "?", "Really sell?",
                                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                                    {
                                        return false;
                                    }
                                    _comms.InventorySell(buttonSlot.ToString(), () =>
                                    {
                                        button.Enabled = true;
                                        return true;
                                    }, (string msg, int code) =>
                                    {
                                        MessageBox.Show(code + ": " + msg);
                                        return true;
                                    });
                                    button.Enabled = false;
                                    return true;
                                });

                                foreach (PropertyInfo itemStatPropertyInfo in item.GetType().GetProperties())
                                {
                                    var iconAttrib = GetAttribute<IconElementAttribute>(itemStatPropertyInfo);
                                    var equipAttrib = GetAttribute<EquipmentStatAttribute>(itemStatPropertyInfo);

                                    if (iconAttrib != null)
                                    {
                                        CreateItemGui(iconAttrib, tab, x2, y, slotNo + controlNameType + "_" + itemStatPropertyInfo.Name + controlNameType,
                                            itemStatPropertyInfo.GetValue(item, null).ToString(), itemStatPropertyInfo.Name);

                                        x2 += 100;
                                        if (x2 > 500)
                                        {
                                            y += 20;
                                            x2 = 15;
                                        }
                                    }
                                    else if (equipAttrib != null)
                                    {
                                        CreateItemGui(iconAttrib, tab, x2, y, slotNo + controlNameType + "_" + equipAttrib.BelongsTo + controlNameType,
                                            itemStatPropertyInfo.GetValue(item, null).ToString(), "");
                                    }
                                }

                                slotNo++;
                                y += 40;
                                x2 = 15;
                            }
                            else
                            {
                                if (_playerControls.TryGetValue(item.type + controlNameType + "_label", out tempControl))
                                {
                                    tempControl.Text = item.type + ": " + item.name;
                                }

                                foreach (PropertyInfo itemStatPropertyInfo in item.GetType().GetProperties())
                                {
                                    var iconAttrib = GetAttribute<IconElementAttribute>(itemStatPropertyInfo);
                                    var equipAttrib = GetAttribute<EquipmentStatAttribute>(itemStatPropertyInfo);

                                    if (iconAttrib != null)
                                    {
                                        if (_playerControls.TryGetValue(item.type + "_" + itemStatPropertyInfo.Name + controlNameType, out tempControl))
                                        {
                                            long itemStat = Convert.ToInt64(itemStatPropertyInfo.GetValue(item, null));
                                            tempControl.Text = itemStat.ToString();
                                        }
                                    }
                                    else if (equipAttrib != null)
                                    {
                                        if (_playerControls.TryGetValue(item.type + "_" + equipAttrib.BelongsTo + controlNameType, out tempControl))
                                        {
                                            long itemStat = Convert.ToInt64(itemStatPropertyInfo.GetValue(item, null));
                                            tempControl.Text += " (" + ((itemStat < 0) ? "-" : "+") + itemStat + "%)";
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (workaroundPetsVal != null)
                    {
                        //controlNameType = "_overflow";
                        List<Control> controls = _playerControls.RemoveAllKeys(x => x.Contains("_foundpets"));
                        TabPage tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "BuyPetTab");
                        foreach (Control control in controls)
                        {
                            tab.Controls.Remove(control);
                        }

                        int x2 = 15, y = 0;
                        foreach (FoundPetInfo pet in workaroundPetsVal)
                        {
                            AddLabel(tab, x2, y, pet.name + "_foundpets_name", pet.name);
                            AddLabel(tab, x2, y + 20, pet.name + "_foundpets_price", pet.cost.ToString());

                            AddButton(tab, 600, y, pet.name + "_foundpets_button1", "Buy", pet.name, (Button button, string petName) =>
                            {
                                BuyPetForm newForm = new BuyPetForm(_comms, pet.name, pet.cost.ToString());
                                newForm.Show();
                                return true;
                            });

                            y += 60;
                        }
                    }
                    else if (eventVal != null)
                    {
                        eventVal.Reverse();
                        TabPage tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "EventsTab");
                        tab.Controls.Clear();

                        foreach(var keyval in _playerControls.Where(k => k.Key.StartsWith("event_")).ToList())
                        {
                            _playerControls.Remove(keyval.Key);
                        }

                        int x2 = 15, y2 = 0;
                        foreach (IdleLandsGUI.Model.EventInfo item in eventVal)
                        {
                            if (string.IsNullOrEmpty(item._id))
                                continue;

                            var labelSize = AddAutoSizeLabel(tab, x2, y2, "event_" + item._id, item.message);
                            y2 += labelSize.Height + 10;
                        }
                    }
                    else if (statCacheVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages.Cast<TabPage>().Single(page => page.Name == "PlayerInfoTab");
                        foreach (PropertyInfo statCachePropertyInfo in statCacheVal.GetType().GetProperties())
                        {
                            if (_playerControls.TryGetValue(statCachePropertyInfo.Name + "_statcache", out tempControl))
                            {
                                tempControl.Text = statCachePropertyInfo.GetValue(statCacheVal, null).ToString();
                            }
                        }
                    }
                    else if (val.GetType() == typeof(string))
                    {
                        if (_playerControls.TryGetValue(propertyInfo.Name + "_val", out tempControl))
                        {
                            tempControl.Text = propertyInfo.Name + ": " + ((String)val);
                        }
                    }
                }
            }
        }

        private void AddLabel(TabPage tab, int x, int y, string Name, string Text, int width = 190)
        {
            Label tempLabel = new Label();
            tempLabel.Location = new Point(x, y);
            tempLabel.Size = new System.Drawing.Size(width, 15);
            tempLabel.Text = Text;
            _playerControls.Add(Name, tempLabel);
            tab.Controls.Add(tempLabel);
        }

        private Size AddAutoSizeLabel(TabPage tab, int x, int y, string Name, string Text)
        {
            Label tempLabel = new Label();

            tempLabel.Location = new Point(x, y);
            tempLabel.MaximumSize = new System.Drawing.Size(600, 0);
            tempLabel.AutoSize = true;
            /* This doesn't work :(
             * if (Text.Contains("http://"))
            {
                string url = Text.Substring(Text.IndexOf("http://"));
                Text = Text.Substring(0, Text.IndexOf("http://"));
                Text += "<a href=\"" + url + "\">here</a>";
            }*/
            tempLabel.Text = Text;
            _playerControls.Add(Name, tempLabel);
            tab.Controls.Add(tempLabel);
            return tempLabel.Size;
        }

        private PictureBox AddIcon(TabPage tab, int x, int y, string Name, string IconName, Color? color = null)
        {
            if (color == null)
                color = Color.Red;

            PictureBox tempBox = new PictureBox();
            tempBox.Location = new Point(x, y);
            Bitmap img =  (Bitmap)Image.FromFile("Assets/fa-icons/" + IconName);
            for (int i = 0; i < img.Height; i++)
            {
                for (int j = 0; j < img.Width; j++)
                {
                    Color orig = img.GetPixel(j, i);
                    img.SetPixel(j, i, Color.FromArgb(orig.A, color.Value));
                }
            }
            tempBox.Image = img;
            tempBox.Size = new System.Drawing.Size(16, 16);

            _playerControls.Add(Name, tempBox);
            tab.Controls.Add(tempBox);
            return tempBox;
        }

        private Button AddButton<T>(TabPage tab, int x, int y, string Name, string Text, T comparisonValue, Func<Button, T, bool> onClickHandler)
        {
            Button tempButton = new Button();
            tempButton.Location = new Point(600, y);
            tempButton.Text = Text;
            tempButton.Click += (Object o, EventArgs e) =>
            {
                onClickHandler(tempButton, comparisonValue);
            };
            _playerControls.Add(Name, tempButton);
            tab.Controls.Add(tempButton);

            return tempButton;
        }

        private void ApplyPlayerSettingsButton_Click(object sender, EventArgs e)
        {
            PriorityPointsInfo info = new PriorityPointsInfo();
            info.str = StrTrackBar.Value;
            info.dex = DexTrackBar.Value;
            info.con = ConTrackBar.Value;
            info.agi = AgiTrackBar.Value;
            info.wis = WisTrackBar.Value;
            info._int = IntTrackBar.Value;

            if(info.str + info.dex + info.con + info.agi + info.wis + info._int != 6)
            {
                MessageBox.Show("Combined value of priority points should be 6.");
                return;
            }

            string gender = "female";
            if (MaleRadioButton.Checked)
                gender = "male";
            if (OtherRadioButton.Checked)
                gender = OtherTextBox.Text;

            _comms.SendGender(gender, () =>
            {
                _comms.SendPriorityPoints(info, null, (string msg, int code) =>
                {
                    MessageBox.Show(code + ": " + msg);
                    return true;
                });
                return true;
            }, (string msg, int code) =>
            {
                MessageBox.Show(code + ": " + msg);
                return true;
            });
            
        }

        private T GetAttribute<T>(PropertyInfo info)
        {
            return (T)info.GetCustomAttributes(typeof(T), false).FirstOrDefault();
        }

        private void CreateItemGui(IconElementAttribute iconAttrib, TabPage tab, int x, int y,
            string name, string labelText, string tooltipText)
        {
            if (iconAttrib != null)
            {
                var control = AddIcon(tab, x, y + 20, name + "_icon", iconAttrib.Name, Color.FromName(iconAttrib.Colour));
                AddLabel(tab, x + 18, y + 20, name, labelText, 80);

                new ToolTip().SetToolTip(control, tooltipText);
            }
            else
            {
                Control tempControl = null;
                if (_playerControls.TryGetValue(name, out tempControl))
                {
                    long itemStat = Convert.ToInt64(labelText);
                    tempControl.Text += " (" + ((itemStat < 0) ? "-" : "+") + itemStat + "%)";
                }
            }
        }

        private void OtherRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (OtherRadioButton.Checked)
                OtherTextBox.Enabled = true;
            else
                OtherTextBox.Enabled = false;
        }
    }
}
