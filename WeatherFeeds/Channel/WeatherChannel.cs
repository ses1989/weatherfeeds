using System;
using System.Linq;
using System.Threading.Tasks;
using WeatherFeeds.Events;

namespace WeatherFeeds.Channel
{
    public class WeatherChannel: IWeatherChannel
    {
        public WeatherChannel()
        {
        }

        public event EventHandler<WeatherInfoEventArgs> ProcessingCompleted;

        public async Task ProcessWeatherDataAsync(string location, int celsius)
        {
            try
            {
                Console.WriteLine($"Received Temperature data of location '{location}' at {DateTime.Now}");
                Console.WriteLine("Converting celsius to fahrenheit...");
                int fahren = (celsius * 9) / 5 + 32;
                await Task.Delay(2000);
                Console.WriteLine("Data converted successfully.");
                OnProcessingCompleted(new WeatherInfoEventArgs(location, celsius, fahren));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while processing the request, {e.Message}");
            }
        }

        public void Subscribe(string subscriber, EventHandler<WeatherInfoEventArgs> action)
        {
            if (ProcessingCompleted == null || !ProcessingCompleted.GetInvocationList().Contains(action))
            {
                ProcessingCompleted += action;
                Console.WriteLine($"'{subscriber}' subscribed");
            }
        }

        public void UnSubscribe(string subscriber, EventHandler<WeatherInfoEventArgs> action)
        {
            ProcessingCompleted -= action;
            Console.WriteLine($"'{subscriber}' unsubscribed");
        }

        protected virtual void OnProcessingCompleted(WeatherInfoEventArgs e)
        {
            ProcessingCompleted?.Invoke(this, e);
        }
    }
}
