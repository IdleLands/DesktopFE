using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

using IdleLandsGUI.Properties;

// Taken from http://www.codeproject.com/Articles/290013/Formless-System-Tray-Application

namespace IdleLandsGUI.SystemTray
{
    /// <summary>
    /// 
    /// </summary>
    public class IdlelandContextMenu
    {
        private IdleLandsComms _Comms { get; set; }
        private Form _Form { get; set; }

        public IdlelandContextMenu(IdleLandsComms comms)
        {
            _Comms = comms;
        }

        public void SetForm(Form form)
        {
            _Form = form;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>ContextMenuStrip</returns>
        public ContextMenuStrip Create()
        {
            // Add the default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            // Windows Explorer.
            item = new ToolStripMenuItem();
            item.Text = "Show/Hide";
            item.Click += new EventHandler(Show_Click);
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler(Exit_Click);
            menu.Items.Add(item);

            return menu;
        }

        void Show_Click(object sender, EventArgs e)
        {
            if(_Form != null)
            {
                _Form.Visible = !_Form.Visible;
            }
        }

        /// <summary>
        /// Processes a menu item.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        void Exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(_Form, "Really?", "Closing...",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Question)
                    != DialogResult.Cancel)
            {
                _Comms.Logout(() => { Application.Exit(); return true; });
            }
        }
    }
}
