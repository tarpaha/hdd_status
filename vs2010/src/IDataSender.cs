using System;

namespace hdd_status
{
    interface IDataSender : IDisposable
    {
        void Send(float value);
    }
}
