using System;
using System.Collections.Generic;
using System.Text;

namespace BlayerUI.Shared
{
    public class BElement
    {
        public string Tag { get; set; }
        public string InnerText { get; set; }
        public List<BElement> InnerElements { get; set; }
    }

    public class BBText : BElement
    {
        public new string Tag => "p";
    }
    public class BButton : BElement
    {
        public new string Tag => "button";
    }


    public class JsonView
    {
        public List<BElement> Elements { get; set; }
    }
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    }
}
