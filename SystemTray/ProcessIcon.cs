using System;
using System.Diagnostics;
using System.Windows.Forms;
using IdleLandsGUI.Properties;
using System.Drawing;

// Taken from http://www.codeproject.com/Articles/290013/Formless-System-Tray-Application

namespace IdleLandsGUI.SystemTray
{
    /// <summary>
    /// 
    /// </summary>
    class ProcessIcon : IDisposable
    {
        /// <summary>
        /// The NotifyIcon object.
        /// </summary>
        private NotifyIcon _NotifyIcon;
        private IdleLandsComms _Comms;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessIcon"/> class.
        /// </summary>
        public ProcessIcon(IdleLandsComms comms)
        {
            _Comms = comms;
            _NotifyIcon = new NotifyIcon();
        }

        /// <summary>
        /// Displays the icon in the system tray.
        /// </summary>
        public IdlelandContextMenu Display()
        {
            // Put the icon in the system tray and allow it react to mouse clicks.			
            _NotifyIcon.MouseClick += new MouseEventHandler(ni_MouseClick);
            _NotifyIcon.Icon = Icon.FromHandle(Resources.IdleLandsIcon.GetHicon());
            _NotifyIcon.Text = "DesktopFE";
            _NotifyIcon.Visible = true;

            // Attach a context menu.
            var menu = new IdlelandContextMenu(_Comms);
            _NotifyIcon.ContextMenuStrip = menu.Create();
            return menu;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            // When the application closes, this will remove the icon from the system tray immediately.
            _NotifyIcon.Dispose();
        }

        /// <summary>
        /// Handles the MouseClick event of the ni control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs"/> instance containing the event data.</param>
        void ni_MouseClick(object sender, MouseEventArgs e)
        {
        }
    }
}
