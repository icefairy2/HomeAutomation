using System;
using webapp.Models;

using System.IO.Ports;

namespace webapp.Controllers
{
    public static class TemperatureUpdater
    {
        private static InOutTemperatureDbContext dbTemp = new InOutTemperatureDbContext();
        private static WindowDbContext dbWind = new WindowDbContext();
        private static HeatDbContext dbHeat = new HeatDbContext();

        public static void UpdateTemperatureToDb()
        {
            /*
            SerialPort arduinoPort = PortController.GetArduinoPort();
            if (arduinoPort.IsOpen)
            {
                arduinoPort.Write("TEMPERATURE_INDOOR");
                string temperatureString = arduinoPort.ReadLine();
                Double temperature = Double.Parse(temperatureString);

                arduinoPort.Write("TEMPERATURE_OUTDOOR");
                string outTemperatureString = arduinoPort.ReadLine();
                Double outTemperature = Double.Parse(outTemperatureString);

                dbTemp.InOutTemperatures.Add(new InOutTemperature(temperature, outTemperature, DateTime.Now));
                dbTemp.SaveChanges();

                arduinoPort.Write("WINDOW_STATUS");
                string windowStatusString = arduinoPort.ReadLine();
                string cleanWindowStatus = windowStatusString.Trim();
                bool windowOpen = (cleanWindowStatus == "WINDOW_OPEN");

                dbWind.Windows.Add(new Window(windowOpen, DateTime.Now));
                dbWind.SaveChanges();
                
                arduinoPort.Write("HEATER_STATUS");
                string heaterStatusString = arduinoPort.ReadLine();
                string cleanHeaterStatus = heaterStatusString.Trim();
                bool heaterOn = (cleanHeaterStatus == "HEATER_ON");

                dbHeat.Heats.Add(new Heat(heaterOn, DateTime.Now));
                dbHeat.SaveChanges();

            }
            else
            {
                throw new InvalidOperationException("Arduino port not found");
            }
            */
            
        }
        
    }
}