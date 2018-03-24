using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Heat
    {
        public int Id { get; set; }
        public bool IsTurnedOn { get; set; }
        public DateTime DateRecorded { get; set; }

        public Heat()
        {
        }

        public Heat(bool isTurnedOn, DateTime dateTime)
        {
            IsTurnedOn = isTurnedOn;
            DateRecorded = dateTime;
        }
    }
}