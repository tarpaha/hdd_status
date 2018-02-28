using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;

namespace hdd_status
{
    public static class SerialPortUtils
    {
        /// <summary>
        /// Returns list of serial ports as they look in device manager
        /// Uses snippet from http://stackoverflow.com/a/2871873/2220552
        /// </summary>
        public static IEnumerable<string> GetSerialPortCaptions()
        {
            var captions = new List<string>();
            var searcher = new ManagementObjectSearcher(
                "root\\CIMV2",
                "SELECT * FROM Win32_PnPEntity");
            foreach (var queryObj in searcher.Get())
            {
                if(queryObj != null)
                {
                    var caption = queryObj["Caption"];
                    if(caption != null)
                    {
                        string captionStr = caption.ToString();
                        if (captionStr.Contains("(COM"))
                        {
                            captions.Add(captionStr);
                        }
                    }
                }
            }
            return captions;
        }

        /// <summary>
        /// Return serial port name that caption (from device manager) contains passed string
        /// </summary>
        public static string GetSerialPortThatContains(string str)
        {
            var portCaptions = GetSerialPortCaptions();
            var portCaptionWithStr = portCaptions.FirstOrDefault(caption => caption.Contains(str));

            var portNames = SerialPort.GetPortNames();
            var portNameForCaptionWithStr = portNames.FirstOrDefault(name => portCaptionWithStr.Contains(name));

            return portNameForCaptionWithStr;
        }
    }
}