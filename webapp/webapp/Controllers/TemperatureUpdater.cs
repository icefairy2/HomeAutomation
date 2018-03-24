using System;
using webapp.Models;

using System.IO.Ports;

namespace webapp.Controllers
{
    public static class TemperatureUpdater
    {
        private static InOutTemperatureDbContext db = new InOutTemperatureDbContext();
        
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

                db.InOutTemperatures.Add(new InOutTemperature(temperature, outTemperature, DateTime.Now));
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Arduino port not found");
            }
            arduinoPort.Close();

        }
        
    }
}