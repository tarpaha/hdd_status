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
            return _diskUsage.NextValue();
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
