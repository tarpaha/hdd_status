using System.Diagnostics;

namespace hdd_status
{
    class DataCollector_DiskUsage : IDataCollector
    {
        #region creation

        public DataCollector_DiskUsage()
        {
            _diskUsage = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
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

        #region data

        private PerformanceCounter _diskUsage;

        #endregion
    }
}
