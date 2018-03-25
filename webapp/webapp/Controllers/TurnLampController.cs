using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO.Ports;
using webapp.Models;

namespace webapp.Controllers
{
    public class TurnLampController : Controller
    {
        private LampDbContext db = new LampDbContext();

        // GET: TurnLamp
        public ActionResult Index()
        {
            SerialPort arduinoPort = PortController.GetArduinoPort();
            if (arduinoPort.IsOpen)
            {
                string turn = Request.QueryString["turn"];
                String command = (turn == "ON") ? "LAMP_ON" : "LAMP_OFF";
                bool lampOn = (turn == "ON");
                arduinoPort.Write(command);
                string temperatureString = arduinoPort.ReadLine();

                db.Lamps.Add(new Lamp(lampOn, DateTime.Now));
                db.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Arduino port not found");
            }
            return View();
        }
    }
}