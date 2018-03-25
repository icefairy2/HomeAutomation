﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webapp.Models;

namespace webapp.Controllers
{
    public class TurnHeatController : Controller
    {
        // GET: TurnHeat?temperature=33
        public ActionResult Index()
        {
            SerialPort arduinoPort = PortController.GetArduinoPort();
            if (arduinoPort.IsOpen)
            {
                string temperatureString = Request.QueryString["temperature"];
                int temperature = Int32.Parse(temperatureString);
                arduinoPort.Write("SET_THERMOSTAT");
                arduinoPort.WriteLine(temperature.ToString(CultureInfo.InvariantCulture));
            }
            else
            {
                throw new InvalidOperationException("Arduino port not found");
            }
            return View();
        }
    }
}