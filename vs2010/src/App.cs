﻿using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace hdd_status
{
    class App : Form
    {
        private const int USAGE_CHECK_PERIOD = 100; // ms

        private readonly NotifyIcon m_trayIcon;
        private readonly ContextMenu m_trayMenu;
        
        private App()
        {
            _collector = new DataCollector_DiskUsage();
            _sender = new DataSender_Console(); //new DataSender_ComPort(3);

            m_trayMenu = new ContextMenu();
            m_trayMenu.MenuItems.Add("Exit", OnExit);

            m_trayIcon = new NotifyIcon();
            m_trayIcon.Text = "HDD Status";
            m_trayIcon.Icon = new Icon(GetType(), "app.ico");
            m_trayIcon.ContextMenu = m_trayMenu;
            m_trayIcon.Visible = true;

            _timer = new PeriodicTimer(USAGE_CHECK_PERIOD, OnTick);
            _timer.Start();
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
                _collector.Dispose();
                _sender.Dispose();

                m_trayIcon.Dispose();
                _timer.Dispose();
            }
            base.Dispose(disposing);
        }

        private void OnTick()
        {
            _sender.Send(_collector.Collect());
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

        #region data

        private IDataCollector _collector;
        private IDataSender _sender;

        private PeriodicTimer _timer;

        #endregion
    }
}
