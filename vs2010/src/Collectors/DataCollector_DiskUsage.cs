using System;
using System.Diagnostics;

namespace hdd_status
{
    class DataCollector_DiskUsage : IDataCollector
    {
        #region creation

        public DataCollector_DiskUsage()
        {
            try
            {
                // System performance counters must contain requested counter and category
                // In some cases counters are broken, use "lodctr /r" (console under administrator) to restore them
                // "lodctr /q" can be used to show all counters with their statuses (enabled or disabled)
                _diskUsage = new PerformanceCounter(COUNTER_NAME, CATEGORY_NAME, INSTANCE_NAME);
            }
            catch (InvalidOperationException)
            {
                throw new Exception(string.Format(
                    "There is no counter \"{0}\" or it category \"{1}\" in performance counters",
                    COUNTER_NAME,
                    CATEGORY_NAME));
            }
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region IDataCollector

        public float Collect()
        {
            float value = _diskUsage.NextValue();
            return value < 100.0f ? value: 100.0f;
        }

        public void Dispose()
        {
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region consts

        private const string COUNTER_NAME = "PhysicalDisk";
        private const string CATEGORY_NAME = "% Disk Time";
        private const string INSTANCE_NAME = "_Total";

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region data

        private PerformanceCounter _diskUsage;

        #endregion
    }
}
