using System;
using webapp.Models;

using System.IO.Ports;

namespace webapp.Controllers
{
    public static class TemperatureUpdater
    {
        private static TemperatureDbContext db = new TemperatureDbContext();
        
        public static void UpdateTemperatureToDb()
        {
            SerialPort arduinoPort = new SerialPort(CommonStrings.ComPort, 9600);
            arduinoPort.Open();
            if (arduinoPort.IsOpen)
            {
                arduinoPort.Write("TEMPERATURE");
                string temperatureString = arduinoPort.ReadLine();
                Double temperature = Double.Parse(temperatureString);
                db.TemperatureRecords.Add(new TemperatureRecord(temperature, DateTime.Now));
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