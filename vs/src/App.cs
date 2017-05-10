using System;
using System.Windows.Forms;
using Liensberger;

namespace hdd_status
{
    class App : Form
    {
        #region Form

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Init();

            Visible = false;
            ShowInTaskbar = false;

            _hook = new KeyboardHook();
            _hook.KeyPressed += KeyPressed;
            _hook.RegisterHotKey(KeyboardHook.ModifierKeys.Control | KeyboardHook.ModifierKeys.Alt, Keys.F12);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Release();
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
            byte byteValue = (byte)(value * PercentToByte);

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
            SwitchFlag();
        }

        private void KeyPressed(object sender, KeyboardHook.KeyPressedEventArgs e)
        {
            SwitchFlag();
        }

        private void OnExit()
        {
            Application.Exit();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region private functions
        
        private void Init()
        {
            try
            {
                _collector = new DataCollector_DiskUsage();
                _sender = new DataSender_ComPort(SerialPortUtils.GetSerialPortThatContains(SerialPortCaptionStr));

                _trayIcon = new TrayIcon(OnClick, OnExit);

                _timer = new PeriodicTimer(UsageCheckPeriod, OnTick);
                _timer.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                OnExit();
            }
        }

        private void Release()
        {
            _collector.Dispose();
            _sender.Dispose();

            _trayIcon.Dispose();

            _timer.Dispose();

            _hook.Dispose();
        }

        private void SwitchFlag()
        {
            _flag = !_flag;
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

        private const string SerialPortCaptionStr = "Arduino";
        private const int UsageCheckPeriod = 100;
        private const float PercentToByte = 255.0f / 100.0f;

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region data

        private IDataCollector _collector;
        private IDataSender _sender;

        private PeriodicTimer _timer;

        private TrayIcon _trayIcon;

        private bool _flag;

        private KeyboardHook _hook;

        #endregion
    }
}
