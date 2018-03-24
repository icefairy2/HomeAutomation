using System;

namespace webapp.Models
{
    public class TemperatureRecord
    {
        public TemperatureRecord()
        {
        }

        public TemperatureRecord(double temperature, DateTime dateRecorded)
        {
            Temperature = temperature;
            DateRecorded = dateRecorded;
        }

        public int ID { get; set; }
        public double Temperature { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}