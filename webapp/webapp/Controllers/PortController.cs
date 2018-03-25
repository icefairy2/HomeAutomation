using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO.Ports;

namespace webapp.Controllers
{
    public class PortController
    {
        private static SerialPort initPort()
        {
            SerialPort arduPort = new SerialPort(CommonStrings.ComPort, 9600);
            arduPort.Open();
            return arduPort;
        }

        private static SerialPort arduinoPort = initPort();

        public static SerialPort GetArduinoPort()
        {
            return arduinoPort;
        }
    }
}