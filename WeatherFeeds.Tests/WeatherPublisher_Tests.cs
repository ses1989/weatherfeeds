using System;
using System.IO;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WeatherFeeds.Channel;
using WeatherFeeds.Publisher;

namespace WeatherFeeds.Tests
{
    public class WeatherPublisher_Tests
    {
        Mock<IWeatherChannel> _mockWeatherChannel;

        [SetUp]
        public void SetUp()
        {
            _mockWeatherChannel = new Mock<IWeatherChannel>();
        }

        [Test]
        public async Task Test_verify_publisher_submit_data_called()
        {
            var data = string.Join(Environment.NewLine, new[]
            {
                "Kochi",
                "30"
            });

            Console.SetIn(new StringReader(data));

            var publisher = new WeatherPublisher(_mockWeatherChannel.Object);
            _mockWeatherChannel.Setup(t => t.ProcessWeatherDataAsync("Kochi", 30)).Verifiable();
            await publisher.PublishDataAsync();

            _mockWeatherChannel.Verify(t => t.ProcessWeatherDataAsync("Kochi", 30), Times.Once);
        }
    }
}
