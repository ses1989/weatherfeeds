using System;
using System.Threading.Tasks;
using WeatherFeeds.Channel;

namespace WeatherFeeds.Publisher
{
    public class WeatherPublisher
    {
        readonly IWeatherChannel _weatherChannel;
        public WeatherPublisher(IWeatherChannel weatherChannel)
        {
            _weatherChannel = weatherChannel;
        }

        public async Task PublishDataAsync()
        {
            Console.Write("Enter the location : ");
            var location = Console.ReadLine();
            Console.Write("Enter the Temperature in Celsius(°C) : ");
            int.TryParse(Console.ReadLine(), out int celsius);

            await _weatherChannel.ProcessWeatherDataAsync(location, celsius);
        }
    }
}
