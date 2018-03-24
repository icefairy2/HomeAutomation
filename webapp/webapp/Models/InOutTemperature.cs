using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class InOutTemperature
    {
        public InOutTemperature()
        {
        }

        public InOutTemperature(double indoorTemperature, double outdoorTemperature, DateTime dateRecorded)
        {
            IndoorTemperature = indoorTemperature;
            OutdoorTemperature = outdoorTemperature;
            DateRecorded = dateRecorded;
        }

        public int ID { get; set; }
        public double IndoorTemperature { get; set; }
        public double OutdoorTemperature { get; set; }
        public DateTime DateRecorded { get; set; }
    }
}