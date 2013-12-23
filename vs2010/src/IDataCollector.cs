using System;

namespace hdd_status
{
    interface IDataCollector : IDisposable
    {
        float Collect();
    }
}
