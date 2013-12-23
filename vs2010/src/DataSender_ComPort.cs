using System.IO.Ports;

namespace hdd_status
{
    class DataSender_ComPort : IDataSender
    {
        #region creation

        public DataSender_ComPort(int portNumber)
        {
            _comPortName = string.Format("COM{0}", portNumber);
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region IDataSender

        public void Create()
        {
            _serialPort = new SerialPort("COM3");
            _serialPort.Open();
        }

        public void Send(float value)
        {
            _serialPort.Write(new byte[] { (byte)(value) }, 0, 1);
        }

        public void Destroy()
        {
            _serialPort.Close();
        }

        #endregion

        /////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////

        #region private data

        private readonly string _comPortName;
        private SerialPort _serialPort;

        #endregion
    }
}
