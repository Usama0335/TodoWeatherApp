using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1
{
    public class WeatherModel
    {
        public Main main { get; set; }
        public List<Weather> weather { get; set; }
        public Wind wind { get; set; }
    }

    public class Main
    {
        public double temp { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
    }

    public class Weather
    {
        public string main { get; set; }
        public string description { get; set; }
    }
    public class Wind
    {
        public double speed { get; set; }
    }
}
