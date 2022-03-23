using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WeatherForcast.Models;


namespace WeatherForcast.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Weather()
        {

            return View();
        }

        [HttpGet]
      [CacheFilter(TimeDuration = 10)]
        public String WeatherDetail(string City)
        {

            //API KEY 
            string appId = "65f11e12c66d8e7c8eeff80a9c450a29";
            

           
            
            string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&units=imperial&cnt=1&APPID={1}", City, appId);

            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString(url);
  
                //object converting
                RootObject weather = (new JavaScriptSerializer()).Deserialize<RootObject>(json);

               
                ResultViewModel results = new ResultViewModel();

                results.Country = weather.sys.country;
                results.City = weather.name;
                results.Lat = Convert.ToString(weather.coord.lat);
                results.Lon = Convert.ToString(weather.coord.lon);
                results.Description = weather.weather[0].description;
                results.Humidity = Convert.ToString(weather.main.humidity);
                results.Temp = Convert.ToString(weather.main.temp);
                results.TempFeelsLike = Convert.ToString(weather.main.feels_like);
                results.TempMax = Convert.ToString(weather.main.temp_max);
                results.TempMin = Convert.ToString(weather.main.temp_min);
                results.WeatherIcon = weather.weather[0].icon;

                //obj to json strng 
                var jsonstring = new JavaScriptSerializer().Serialize(results);

                return jsonstring;
            }
        }
    }
}