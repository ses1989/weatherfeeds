using System.Threading.Tasks;
using NUnit.Framework;
using WeatherFeeds.Channel;
using WeatherFeeds.Events;

namespace WeatherFeeds.Tests
{
    public class WeatherChannel_Tests
    {
        IWeatherChannel _weatherChannel;
        bool _isSubscribed;
        int _triggerCount;

        [SetUp]
        public void Setup()
        {
            _weatherChannel = new WeatherChannel();
            _isSubscribed = false;
            _triggerCount = 0;
        }

        [Test]
        public async Task Test_validate_temperature_data()
        {
            WeatherInfoEventArgs eventArgs = null;
            _weatherChannel.Subscribe("Test Subscriber", delegate (object sender, WeatherInfoEventArgs e)
            {
                eventArgs = e;
            });
            await _weatherChannel.ProcessInputDataAsync(new WeatherData { Location = "kochi", Celsius = 30 });
            int fahren = (30 * 9) / 5 + 32;
            Assert.AreEqual(30, eventArgs.Celsius);
            Assert.AreEqual(fahren, eventArgs.Fahrenheit);
        }

        [Test]
        public async Task Test_validate_temperature_data_subscribe()
        {
            _weatherChannel.Subscribe("Test Subscriber", OnProcessedDataReceived);
            await _weatherChannel.ProcessInputDataAsync(new WeatherData { Location = "kochi", Celsius = 30 });
            Assert.IsTrue(_isSubscribed);
        }

        [Test]
        public async Task Test_validate_temperature_data_unsubscribe()
        {
            _weatherChannel.Subscribe("Test Subscriber", OnProcessedDataReceived);
            _weatherChannel.UnSubscribe("Test Subscriber", OnProcessedDataReceived);
            await _weatherChannel.ProcessInputDataAsync(new WeatherData { Location = "kochi", Celsius = 30 });
            Assert.IsFalse(_isSubscribed);
        }

        [Test]
        public async Task Test_validate_event_execution_count()
        {
            _weatherChannel.Subscribe("Test Subscriber", OnProcessedDataReceived);
            _weatherChannel.Subscribe("Test Subscriber", OnProcessedDataReceived);
            await _weatherChannel.ProcessInputDataAsync(new WeatherData { Location = "kochi", Celsius = 30 });
            Assert.AreEqual(1, _triggerCount);
        }

        protected virtual void OnProcessedDataReceived(object sender, WeatherInfoEventArgs e)
        {
            _isSubscribed = true;
            _triggerCount++;
        }

    }
}
