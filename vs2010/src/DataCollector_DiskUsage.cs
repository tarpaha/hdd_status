using System.Diagnostics;

namespace hdd_status
{
    class DataCollector_DiskUsage : IDataCollector
    {
        public void Create()
        {
            m_diskUsage = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
        }

        public float Collect()
        {
            return m_diskUsage.NextValue();
        }

        public void Destroy()
        {
        }

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region private data

        private PerformanceCounter m_diskUsage;

        #endregion
    }
}
