﻿namespace hdd_status
{
    class DataSender_Console : IDataSender
    {
        #region IDataSender

        public void Send(byte value)
        {
            System.Console.WriteLine(string.Format(
                "sending value = {0}",
                value));
        }

        public void Dispose()
        {
        }

        #endregion
    }
}
