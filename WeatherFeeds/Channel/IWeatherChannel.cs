using System;
using System.Threading.Tasks;
using WeatherFeeds.Events;

namespace WeatherFeeds.Channel
{
    public interface IWeatherChannel
    {
        Task ProcessInputDataAsync(WeatherData data);
        void Subscribe(string subscriber, EventHandler<WeatherInfoEventArgs> action);
        void UnSubscribe(string subscriber, EventHandler<WeatherInfoEventArgs> action);
    }
}
