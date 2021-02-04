using System;
using System.Threading.Tasks;
using WeatherFeeds.Events;

namespace WeatherFeeds.Channel
{
    public interface IWeatherChannel
    {
        event EventHandler<WeatherInfoEventArgs> ProcessingCompleted;

        Task ProcessWeatherDataAsync(string location, int celsius);
        void Subscribe(string subscriber, EventHandler<WeatherInfoEventArgs> action);
        void UnSubscribe(string subscriber, EventHandler<WeatherInfoEventArgs> action);
    }
}
