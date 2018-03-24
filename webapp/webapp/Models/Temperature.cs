using System;

namespace webapp.Models
{
    public class TemperatureRecord
    {
        public int ID { get; set; }
        public double Temperature { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}