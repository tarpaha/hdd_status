using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace hdd_status
{
    class App : Form
    {
        #region creation

        private App()
        {
            _collector = new DataCollector_DiskUsage();
            _sender = new DataSender_ComPort(3);

            _trayIcon = new TrayIcon(OnClick, OnExit);

            _timer = new PeriodicTimer(USAGE_CHECK_PERIOD, OnTick);
            _timer.Start();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region Form

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

                _trayIcon.Dispose();

                _timer.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region events

        private void OnTick()
        {
            _sender.Send(_collector.Collect());
        }

        private void OnClick()
        {
            System.Console.WriteLine("click");
        }

        private void OnExit()
        {
            Application.Exit();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region main

        [STAThread]
        public static void Main()
        {
            Application.Run(new App());
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region constants

        private const int USAGE_CHECK_PERIOD = 100;

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region data

        private IDataCollector _collector;
        private IDataSender _sender;

        private PeriodicTimer _timer;

        private TrayIcon _trayIcon;

        #endregion
    }
}
