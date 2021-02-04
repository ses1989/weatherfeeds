using System;
namespace WeatherFeeds.Events
{
    public class WeatherInfoEventArgs : EventArgs
    {
        public string Location { get; private set; }
        public int Celsius { get; private set; }
        public int Fahrenheit { get; private set; }
        public DateTime UpdatedOn { get; set; }

        public WeatherInfoEventArgs(string location, int celsius, int fahrenheit)
        {
            Location = location;
            Celsius = celsius;
            Fahrenheit = fahrenheit;
            UpdatedOn = DateTime.Now;
        }
    }
}
