using IdleLandsGUI.CustomAttributes;
using IdleLandsGUI.Model;
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
        private PlayerInfo Player
        {
            get
            {
                return _player;
            }
            set
            {
                _player = value;
                UpdateGui(value);
            }
        }
        private IdleLandsComms Comms { get; set; }
        public MainForm(PlayerInfo player, IdleLandsComms comms)
        {
            InitializeComponent();

            _playerControls = new Dictionary<string, Control>();

            CreateGui(player);

            Player = player;
            this.FormClosing += MainForm_FormClosing;
            Comms = comms;
            comms.AddPlayerUpdateDelegate(UpdatePlayer);
        }

        public void UpdatePlayer(PlayerInfo info)
        {
            Player = info;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show(this, "Really?", "Closing...",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    == DialogResult.Cancel) e.Cancel = true;
                else
                {
                    Comms.Logout(() => {Application.Exit(); return true;});
                }
            }
        }

        private void CreateGui(PlayerInfo info)
        {
            Type playerInfoType = info.GetType();

            int x = 15;
            int y = 0;
            foreach (PropertyInfo propertyInfo in playerInfoType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var labelAttrib = (NotAGuiElementAttribute)propertyInfo.GetCustomAttributes(typeof(NotAGuiElementAttribute), false).FirstOrDefault();
                    if (labelAttrib != null)
                        continue;

                    object val = propertyInfo.GetValue(info);

                    if (val == null)
                        continue;

                    StatInfo statVal = val as StatInfo;
                    List<EquipmentInfo> equipmentVal = val as List<EquipmentInfo>;
                    List<IdleLandsGUI.Model.EventInfo> eventVal = val as List<IdleLandsGUI.Model.EventInfo>;
                    StatCacheInfo statCacheVal = val as StatCacheInfo;
                    if (statVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages[0];
                        var iconAttrib = (IconElementAttribute)propertyInfo.GetCustomAttributes(typeof(IconElementAttribute), false).FirstOrDefault();

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
                        TabPage tab = InfoTabControl.TabPages[1];
                        int x2 = 15, y2 = 0;
                        foreach (EquipmentInfo item in equipmentVal)
                        {
                            AddLabel(tab, x2, y2, item.type + "_equipment_label", item.type + ": " + item.name);

                            int x3 = x2;

                            foreach (PropertyInfo itemStatPropertyInfo in item.GetType().GetProperties())
                            {
                                var iconAttrib = (IconElementAttribute)itemStatPropertyInfo.GetCustomAttributes(typeof(IconElementAttribute), false).FirstOrDefault();
                                var equipAttrib = (EquipmentStatAttribute)itemStatPropertyInfo.GetCustomAttributes(typeof(EquipmentStatAttribute), false).FirstOrDefault();

                                if (iconAttrib != null)
                                {
                                    var control = AddIcon(tab, x3, y2 + 20, item.type + "_" + itemStatPropertyInfo.Name + "_equipment_icon",
                                        iconAttrib.Name, Color.FromName(iconAttrib.Colour));
                                    AddLabel(tab, x3 + 18, y2 + 20, item.type + "_" + itemStatPropertyInfo.Name + "_equipment",
                                        itemStatPropertyInfo.GetValue(item, null).ToString(), 80);

                                    new ToolTip().SetToolTip(control, itemStatPropertyInfo.Name);

                                    x3 += 100;
                                    if(x3 > 500)
                                    {
                                        y2 += 20;
                                        x3 = x2;
                                    }
                                }
                                else if(equipAttrib != null)
                                {
                                    Control tempControl = null;
                                    if (_playerControls.TryGetValue(item.type + "_" + equipAttrib.BelongsTo + "_equipment", out tempControl))
                                    {
                                        long itemStat = Convert.ToInt64(itemStatPropertyInfo.GetValue(item, null));
                                        tempControl.Text += " (" + ((itemStat < 0) ? "-" : "+") + itemStat + "%)";
                                    }
                                }
                            }

                            y2 += 40;
                        }
                    }
                    else if (statCacheVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages[0];
                        int x2 = x + 250, y2 = 0;
                        foreach (PropertyInfo statCachePropertyInfo in statCacheVal.GetType().GetProperties())
                        {
                            var iconAttrib = (IconElementAttribute)statCachePropertyInfo.GetCustomAttributes(typeof(IconElementAttribute), false).FirstOrDefault();

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
                    else if (val.GetType() == typeof(string))
                    {
                        TabPage tab = InfoTabControl.TabPages[0];
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

            foreach (PropertyInfo propertyInfo in playerInfoType.GetProperties())
            {
                if (propertyInfo.CanRead)
                {
                    var labelAttrib = (NotAGuiElementAttribute)propertyInfo.GetCustomAttributes(typeof(NotAGuiElementAttribute), false).FirstOrDefault();
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
                    if (statVal != null)
                    {
                        if (_playerControls.TryGetValue(propertyInfo.Name + "_stat", out tempControl))
                        {
                            var iconAttrib = (IconElementAttribute)propertyInfo.GetCustomAttributes(typeof(IconElementAttribute), false).FirstOrDefault();
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
                        foreach (EquipmentInfo item in equipmentVal)
                        {
                            if (_playerControls.TryGetValue(item.type + "_equipment_label", out tempControl))
                            {
                                tempControl.Text = item.type + ": " + item.name;
                            }

                            foreach (PropertyInfo itemStatPropertyInfo in item.GetType().GetProperties())
                            {
                                var iconAttrib = (IconElementAttribute)itemStatPropertyInfo.GetCustomAttributes(typeof(IconElementAttribute), false).FirstOrDefault();
                                var equipAttrib = (EquipmentStatAttribute)itemStatPropertyInfo.GetCustomAttributes(typeof(EquipmentStatAttribute), false).FirstOrDefault();

                                if(iconAttrib != null)
                                {
                                    if (_playerControls.TryGetValue(item.type + "_" + itemStatPropertyInfo.Name + "_equipment", out tempControl))
                                    {
                                        long itemStat = Convert.ToInt64(itemStatPropertyInfo.GetValue(item, null));
                                        tempControl.Text = itemStat.ToString();
                                    }
                                }
                                else if (equipAttrib != null)
                                {
                                    if (_playerControls.TryGetValue(item.type + "_" + equipAttrib.BelongsTo + "_equipment", out tempControl))
                                    {
                                        long itemStat = Convert.ToInt64(itemStatPropertyInfo.GetValue(item, null));
                                        tempControl.Text += " (" + ((itemStat < 0) ? "-" : "+") + itemStat + "%)";
                                    }
                                }
                            }
                        }
                    }
                    else if (eventVal != null)
                    {
                        eventVal.Reverse();
                        TabPage tab = InfoTabControl.TabPages[2];
                        tab.Controls.Clear();

                        foreach(var keyval in _playerControls.Where(k => k.Key.StartsWith("event_")).ToList())
                        {
                            _playerControls.Remove(keyval.Key);
                        }

                        int x2 = 15, y2 = 0;
                        foreach (IdleLandsGUI.Model.EventInfo item in eventVal)
                        {
                            var labelSize = AddAutoSizeLabel(tab, x2, y2, "event_" + item._id, item.message);
                            y2 += labelSize.Height + 10;
                        }
                    }
                    else if (statCacheVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages[0];
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
    }
}
