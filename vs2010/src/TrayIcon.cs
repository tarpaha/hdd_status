using System;
using System.Drawing;
using System.Windows.Forms;

namespace hdd_status
{
    class TrayIcon : IDisposable
    {
        public TrayIcon(Action onClick, Action onExit)
        {
            ContextMenu trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add(
                "Exit",
                (object sender, EventArgs args) =>
                {
                    onExit();
                });

            _trayIcon = new NotifyIcon();
            _trayIcon.Text = "HDD Status";
            _trayIcon.Icon = new Icon(GetType(), "app.ico");
            _trayIcon.ContextMenu = trayMenu;
            _trayIcon.Visible = true;
            _trayIcon.Click +=
                (object sender, EventArgs args) =>
                {
                    onClick();
                };
        }

        public void Dispose()
        {
            _trayIcon.Dispose();
        }

        private readonly NotifyIcon _trayIcon;
    }
}
