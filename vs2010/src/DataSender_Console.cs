using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hdd_status
{
    class DataSender_Console : IDataSender
    {
        public void Create()
        {
        }

        public void Send(float value)
        {
            System.Console.WriteLine(string.Format(
                "sending value = {0}",
                value));
        }

        public void Destroy()
        {
        }
    }
}
