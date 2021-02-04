using System;
using Moq;
using NUnit.Framework;
using WeatherFeeds.Subscriber;
using WeatherFeeds.Events;
using WeatherFeeds.Channel;

namespace WeatherFeeds.Tests
{
    public class WeatherSubscriber_Tests
    {
        Mock<IWeatherChannel> _mockWeatherChannel = null;

        [SetUp]
        public void SetUp()
        {
            _mockWeatherChannel = new Mock<IWeatherChannel>();
        }

        [Test]
        public void Test_verify_subscribe_called()
        {
            var name = "Test Subsciber";
            _mockWeatherChannel.Setup(t => t.Subscribe(name, It.IsAny<EventHandler<WeatherInfoEventArgs>>())).Verifiable();
            var subscribe = new WeatherSubscriber(name, _mockWeatherChannel.Object);

            _mockWeatherChannel.Verify();
        }

        [Test]
        public void Test_verify_unsubscribe_called()
        {
            var name = "Test Subsciber";
            _mockWeatherChannel.Setup(t => t.Subscribe(name, It.IsAny<EventHandler<WeatherInfoEventArgs>>())).Verifiable();
            _mockWeatherChannel.Setup(t => t.UnSubscribe(name, It.IsAny<EventHandler<WeatherInfoEventArgs>>())).Verifiable();
            var subscribe = new WeatherSubscriber(name, _mockWeatherChannel.Object);
            subscribe.UnSubscribe();

            _mockWeatherChannel.Verify();
        }
    }
}
