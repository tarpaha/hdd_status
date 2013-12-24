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
            float value = _collector.Collect();
            byte byteValue = (byte)(value * PERCENT_TO_BYTE);

            if (_flag)
            {
                byteValue |= 1;
            }
            else
            {
                byteValue &= 0xFE;
            }

            _sender.Send(byteValue);
        }

        private void OnClick()
        {
            _flag = !_flag;
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
        private const float PERCENT_TO_BYTE = 255.0f / 100.0f;

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region data

        private IDataCollector _collector;
        private IDataSender _sender;

        private PeriodicTimer _timer;

        private TrayIcon _trayIcon;

        private bool _flag;

        #endregion
    }
}
