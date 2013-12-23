using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hdd_status
{
    interface IDataSender
    {
        void Create();
        void Send(float value);
        void Destroy();
    }
}
