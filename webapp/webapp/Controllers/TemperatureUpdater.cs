using System;
using webapp.Models;

using System.IO.Ports;

namespace webapp.Controllers
{
    public static class TemperatureUpdater
    {
        private static InOutTemperatureDbContext dbTemp = new InOutTemperatureDbContext();
        private static WindowDbContext dbWind = new WindowDbContext();

        public static void UpdateTemperatureToDb()
        {
            
            SerialPort arduinoPort = new SerialPort(CommonStrings.ComPort, 9600);
            arduinoPort.Open();
            if (arduinoPort.IsOpen)
            {
                arduinoPort.Write("TEMPERATURE_INDOOR");
                string temperatureString = arduinoPort.ReadLine();
                Double temperature = Double.Parse(temperatureString);

                arduinoPort.Write("TEMPERATURE_OUTDOOR");
                string outTemperatureString = arduinoPort.ReadLine();
                Double outTemperature = Double.Parse(outTemperatureString);

                arduinoPort.Write("WINDOW_STATUS");
                string windowStatusString = arduinoPort.ReadLine();
                string cleanWindowStatus = windowStatusString.Trim();
                bool windowOpen = (cleanWindowStatus == "WINDOW_OPEN");

                dbWind.Windows.Add(new Window(windowOpen, DateTime.Now));
                dbWind.SaveChanges();

                dbTemp.InOutTemperatures.Add(new InOutTemperature(temperature, outTemperature, DateTime.Now));
                dbTemp.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Arduino port not found");
            }
            arduinoPort.Close();
            
        }
        
    }
}