using System;
using WeatherFeeds.Channel;
using WeatherFeeds.Events;

namespace WeatherFeeds.Subscriber
{
    public class WeatherSubscriber
    {
        readonly IWeatherChannel _weatherChannel;
        public string Name { get; set; }

        public WeatherSubscriber(string name, IWeatherChannel weatherChannel)
        {
            Name = name;
            _weatherChannel = weatherChannel;
            _weatherChannel.Subscribe(name, OnProcessedDataReceived);
        }

        public void UnSubscribe()
        {
            _weatherChannel.UnSubscribe(Name, OnProcessedDataReceived);
        }

        protected virtual void OnProcessedDataReceived(object sender, WeatherInfoEventArgs e)
        {
            Console.WriteLine($"Hey {Name}, Temperature at {e.Location} is {e.Celsius}°C | {e.Fahrenheit}°F. Updated on {e.UpdatedOn}");
        }
    }
}
