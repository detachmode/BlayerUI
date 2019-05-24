using BlayerUI.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlayerUI.Server.Controllers
{



    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }

        [HttpGet("[action]")]
        public string View()
        {
            var res = System.IO.File.ReadAllText("./viewjson.json");
            return res;

            var title = DateTime.Now.ToString();
            // return new JsonView
            // {
            //     Elements = new List<BElement>
            //     {
            //         // new BBText{InnerText = "HI"},
            //         // new BBText{InnerText = "HI"},
            //         // new BBText{InnerText = "HI"},
            //         // new BBText{InnerText = "HI"},
            //         new BBText {InnerText = title},
            //         new BBText {InnerText = title},
            //         new BBText {InnerText = title},
            //         new BBText {InnerText = title},
            //         new BBText {InnerText = title},
            //         new BButton {InnerText = "click me!"},
            //         new BButton {InnerText = "click me!"},
            //         new BButton {InnerText = "click me!"},
            //         new BButton {InnerText = "click me!"},
            //     }
            // };
        }
    }
}
