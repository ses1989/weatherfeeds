using System;
using System.Threading.Tasks;
using WeatherFeeds.Channel;
using WeatherFeeds.Publisher;
using WeatherFeeds.Subscriber;

namespace WeatherFeeds
{
    class Program
    {
        static async Task Main(string[] args)
        {
            do
            {
                IWeatherChannel weatherChannel = new WeatherChannel();
                var subscriber1 = new WeatherSubscriber("Subscriber 1", weatherChannel);
                var subscriber2 = new WeatherSubscriber("Subscriber 2", weatherChannel);
                WeatherPublisher publisher = new WeatherPublisher(weatherChannel);

                await publisher.PublishDataAsync();
                subscriber1.UnSubscribe();

                var subscriber3 = new WeatherSubscriber("Subscriber 3", weatherChannel);

                await publisher.PublishDataAsync();

                subscriber2.UnSubscribe();
                subscriber3.UnSubscribe();

                Console.WriteLine("Do you want to continue (Y/N)? ");
            } while (Console.ReadLine().ToUpper() == "Y");
        }
    }
}
