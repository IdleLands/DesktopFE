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
                    Comms.Logout();
                    Application.Exit();
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
                    if (statVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages[0];
                        AddLabel(tab, x, y, propertyInfo.Name, propertyInfo.Name + ": " + statVal.__current + "/" + statVal.maximum);
                        y += 15;
                        if (y + 50 > tab.Size.Height)
                        {
                            y = 0;
                            x += 200;
                        }
                    }
                    else if (equipmentVal != null)
                    {
                        TabPage tab = InfoTabControl.TabPages[1];
                        int x2 = 15, y2 = 0;
                        foreach (EquipmentInfo item in equipmentVal)
                        {
                            AddLabel(tab, x2, y2, item.type, item.type + ": " + item.name);
                            y2 += 15;
                            if (y2 + 50 > tab.Size.Height)
                            {
                                y2 = 0;
                                x2 += 200;
                            }
                        }
                    }
                    else if (eventVal != null)
                    {
                        eventVal.Reverse();
                        TabPage tab = InfoTabControl.TabPages[2];
                        int x2 = 15, y2 = 0;
                        foreach (IdleLandsGUI.Model.EventInfo item in eventVal)
                        {
                            var labelSize = AddAutoSizeLabel(tab, x2, y2, "event_" + item._id, item.message);
                            y2 += labelSize.Height;
                        }
                    }
                    else if (val.GetType() == typeof(string))
                    {
                        TabPage tab = InfoTabControl.TabPages[0];
                        AddLabel(tab, x, y, propertyInfo.Name, propertyInfo.Name + ": " + ((String)val));
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
                    if (statVal != null)
                    {
                        if (_playerControls.TryGetValue(propertyInfo.Name, out tempControl))
                        {
                            tempControl.Text = propertyInfo.Name + ": " + statVal.__current + "/" + statVal.maximum;
                        }
                    }
                    else if (equipmentVal != null)
                    {
                        foreach (EquipmentInfo item in equipmentVal)
                        {
                            if (_playerControls.TryGetValue(item.type, out tempControl))
                            {
                                tempControl.Text = item.type + ": " + item.name;
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
                    else if (val.GetType() == typeof(string))
                    {
                        if (_playerControls.TryGetValue(propertyInfo.Name, out tempControl))
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
            tempLabel.MaximumSize = new System.Drawing.Size(400, 0);
            tempLabel.AutoSize = true;
            tempLabel.Text = Text;
            _playerControls.Add(Name, tempLabel);
            tab.Controls.Add(tempLabel);
            return tempLabel.Size;
        }
    }
}
