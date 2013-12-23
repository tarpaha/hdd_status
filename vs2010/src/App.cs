using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace hdd_status
{
    class App : Form
    {
        private const int USAGE_CHECK_PERIOD = 100;

        private App()
        {
            _collector = new DataCollector_DiskUsage();
            _sender = new DataSender_Console(); //new DataSender_ComPort(3);

            _trayIcon = new TrayIcon(OnClick, OnExit);

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

                _trayIcon.Dispose();

                _timer.Dispose();
            }
            base.Dispose(disposing);
        }

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

        [STAThread]
        public static void Main()
        {
            Application.Run(new App());
        }

        #region data

        private IDataCollector _collector;
        private IDataSender _sender;

        private PeriodicTimer _timer;

        private TrayIcon _trayIcon;

        #endregion
    }
}
