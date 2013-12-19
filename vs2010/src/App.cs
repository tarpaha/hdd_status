using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO.Ports;

namespace cs_hdd_status
{
    class App : Form
    {
        private const int USAGE_CHECK_PERIOD = 100; // ms

        private readonly PerformanceCounter m_diskUsage;
        private readonly SerialPort m_serialPort;
        private readonly System.Timers.Timer m_timer;

        private readonly NotifyIcon m_trayIcon;
        private readonly ContextMenu m_trayMenu;
        
        private float m_value;

        private App()
        {
            m_diskUsage = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            m_value = 0.0f;

            m_serialPort = new SerialPort("COM3");
            m_serialPort.Open();

            m_trayMenu = new ContextMenu();
            m_trayMenu.MenuItems.Add("Exit", OnExit);

            m_trayIcon = new NotifyIcon();
            m_trayIcon.Text = "HDD Status";
            m_trayIcon.Icon = new Icon(GetType(), "app.ico");
            m_trayIcon.ContextMenu = m_trayMenu;
            m_trayIcon.Visible = true;

            m_timer = new System.Timers.Timer();
            m_timer.Elapsed += new System.Timers.ElapsedEventHandler(Update);
            m_timer.Interval = USAGE_CHECK_PERIOD;
            m_timer.AutoReset = true;
            m_timer.Start();
        }

        private void UpdateValue()
        {
            // current disk usage value
            m_value = m_diskUsage.NextValue();
        }

        private void SendValue()
        {
            // send value to serail port
            m_serialPort.Write(new byte[] { (byte)(m_value) }, 0, 1);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            Visible = false;
            ShowInTaskbar = false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_serialPort.Close();
                m_trayIcon.Dispose();
                m_timer.Stop();
            }
            base.Dispose(disposing);
        }

        private void Update(object sender, System.Timers.ElapsedEventArgs e)
        {
            UpdateValue();
            SendValue();
        }

        private void OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        [STAThread]
        public static void Main()
        {
            Application.Run(new App());
        }
    }
}
