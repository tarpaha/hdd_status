using System;
using System.Timers;

namespace hdd_status
{
    class PeriodicTimer : IDisposable
    {
        #region creation

        public PeriodicTimer(int period, Action onUpdate)
        {
            _onUpdate = onUpdate;

            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(Update);
            _timer.Interval = period;
            _timer.AutoReset = true;
            _timer.Start();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region public functions

        public void Start()
        {
            _timer.Start();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region IDisposable

        public void Dispose()
        {
            _timer.Dispose();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region events

        private void Update(object sender, ElapsedEventArgs args)
        {
            _onUpdate();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region data

        private readonly System.Timers.Timer _timer;
        private readonly Action _onUpdate;

        #endregion
    }
}
