using System;
using System.Threading.Tasks;
using WeatherFeeds.Events;

namespace WeatherFeeds.Channel
{
    public class WeatherChannel : Channel<WeatherInfoEventArgs>, IWeatherChannel
    {
        public WeatherChannel() { }
        
        public async Task ProcessInputDataAsync(WeatherData data)
        {
            try
            {
                Console.WriteLine($"Received Temperature data of location '{data.Location}' at {DateTime.Now}");
                Console.WriteLine("Converting celsius to fahrenheit...");
                int fahren = (data.Celsius * 9) / 5 + 32;
                await Task.Delay(2000);
                Console.WriteLine("Data converted successfully.");
                OnProcessingCompleted(new WeatherInfoEventArgs(data.Location, data.Celsius, fahren));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while processing the request, {e.Message}");
            }
        }
    }


    public class WeatherData
    {
        public string Location { get; set; }
        public int Celsius { get; set; }
    }
}
