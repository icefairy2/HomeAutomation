using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class TurnHeatController : Controller
    {
        private HeatDbContext db = new HeatDbContext();
        // GET: TurnHeat
        public ActionResult Index()
        {
            SerialPort arduinoPort = new SerialPort(CommonStrings.ComPort, 9600);
            arduinoPort.Open();
            if (arduinoPort.IsOpen)
            {
                string turn = Request.QueryString["turn"];
                String command = (turn == "ON") ? "LAMP_ON" : "LAMP_OFF";
                bool heatOn = (turn == "ON");
                arduinoPort.Write(command);
                string temperatureString = arduinoPort.ReadLine();

                db.Heats.Add(new Heat(heatOn, DateTime.Now));
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Arduino port not found");
            }
            arduinoPort.Close();
            return View();
        }
    }
}