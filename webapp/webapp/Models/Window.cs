using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class Window
    {
        public int Id { get; set; }
        public bool IsOpen { get; set; }
        public DateTime DateRecorded { get; set; }

        public Window()
        {
        }

        public Window(bool isOpen)
        {
            IsOpen = isOpen;
        }
    }
}