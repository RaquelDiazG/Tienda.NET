using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace TiendaOnline.Controllers
{
    public class IndexController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contacto()
        {
            ServiceReference1.GlobalWeatherSoapClient client = new ServiceReference1.GlobalWeatherSoapClient("GlobalWeatherSoap");
            string result = client.GetWeather("Madrid", "Spain");
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            XmlNodeList xnList = xml.SelectNodes("/CurrentWeather");
            string temperatura = "";
            foreach (XmlNode xn in xnList)
            {
                temperatura = xn["Temperature"].InnerText;
            }
            ViewBag.Temperatura = temperatura;
            return View();
        }
    }
}