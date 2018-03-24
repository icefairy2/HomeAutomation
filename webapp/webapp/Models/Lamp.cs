using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Lamp
    {
        public int Id { get; set; }
        public bool IsTurnedOn { get; set; }
        public DateTime DateRecorded { get; set; }

        public Lamp()
        {
        }

        public Lamp(bool isTurnedOn)
        {
            IsTurnedOn = isTurnedOn;
        }
    }
}