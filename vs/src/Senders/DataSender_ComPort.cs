﻿using System.IO.Ports;

namespace hdd_status
{
    class DataSender_ComPort : IDataSender
    {
        #region creation

        public DataSender_ComPort(string portName)
        {
            _serialPort = new SerialPort(portName);
            _serialPort.Open();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region IDataSender

        public void Send(byte value)
        {
            _serialPort.Write(new byte[] { value }, 0, 1);
        }

        public void Dispose()
        {
            _serialPort.Close();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region data

        private readonly SerialPort _serialPort;

        #endregion
    }
}
