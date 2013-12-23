using System.IO.Ports;

namespace hdd_status
{
    class DataSender_ComPort : IDataSender
    {
        #region creation

        public DataSender_ComPort(int portNumber)
        {
            string portName = string.Format("COM{0}", portNumber);

            _serialPort = new SerialPort(portName);
            _serialPort.Open();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region IDataSender

        public void Send(float value)
        {
            _serialPort.Write(new byte[] { (byte)(value) }, 0, 1);
        }

        public void Dispose()
        {
            _serialPort.Close();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region data

        private SerialPort _serialPort;

        #endregion
    }
}
