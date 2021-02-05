# WeatherFeeds

WeatherFeeds is a C# .NET Core Console Application developed to notify the subscribers about the weather updates of any region. This application is a demonstration of Publish/Subscribe pattern.

## Prerequisites
1. Visual Studio

## Quick Start

1. Download [latest release](https://github.com/ses1989/weatherfeeds/archive/master.zip).
2. Clone the repo: git clone https://github.com/ses1989/weatherfeeds.git

## Description


The Weather application is developed to notify the subscribers about the weather information of a region. Application consists of 3 modules. Publisher, Subscriber and WeatherChannel. The Publisher module will post the latest updates about weather on a particular region. The WeatherChannel module will receive the weather updates of a region from the Publisher and notify all the subscribers listening to the channel. And finally, the Subscriber who can subscribe/unsubscribe to the WeatherChannel and get notified about new weather updates. This application follows the concept of Publish/Subscribe pattern. The Publisher and Subscribers will never know the existence of each other.



### Publisher
Publisher simply post latest weather information of a region into the channel. They will never know the existence of any Subscribers.
```c#
public async Task PublishDataAsync()
{
    Console.Write("Enter the location : ");
    var location = Console.ReadLine();
    Console.Write("Enter the Temperature in Celsius(°C) : ");
    int.TryParse(Console.ReadLine(), out int celsius);

    await _weatherChannel.PublishInputDataAsync(new WeatherData 
                        { Location = location, Celsius = celsius });
}
```
Publisher will take the Location and Temperature in Celsius as inputs and pass it to the WeatherChannel for further processing. 

### WeatherChannel
WeatherChannel will take the input from the Publisher and process it before publishing it to the subscribed users. 


WeatherChannel is extended from an abstract class Channel<TEvent>. Channel<TEvent> declared an event named ProcessingCompleted, which is associated with the EventHandler<TEventArgs> delegate and raised in the OnProcessingCompleted method.
```c#
public event EventHandler<TEvent> ProcessingCompleted;

protected virtual void OnProcessingCompleted(TEvent e)
{
    ProcessingCompleted?.Invoke(this, e);
}
```
Channel<TEvent> also contains method to subscribe/unsubscribe, which takes a unique subscriber name and an event handler method as input. This event handler method must subscribe to the event to receive the notifications in the Subscriber.
```c#
public void Subscribe(string subscriber, EventHandler<TEvent> action)
{
    if (ProcessingCompleted == null || 
        !ProcessingCompleted.GetInvocationList().Contains(action))
    {
        ProcessingCompleted += action;
    }
}

public void UnSubscribe(string subscriber, EventHandler<TEvent> action)
{
    ProcessingCompleted -= action;
}
```

WeatherChannel process the received input location and temperature and convert the Celsius to Fahrenheit. Then it calls the OnProcessingCompleted method to notify all the subscribers. Here we used a custom EventArgs class named WeatherInfoEventArgs to hold event data.
```c#
public async Task ProcessInputDataAsync(WeatherData data)
{
    int fahren = (data.Celsius * 9) / 5 + 32;
    OnProcessingCompleted(new WeatherInfoEventArgs(data.Location, data.Celsius, fahren));
}
```
### Subscriber
A subscriber can subscribe/unsubscribe to a WeatherChannel and listen to the new weather updates published by the Publisher. The Subscriber have an Event handler method to listen to the new weather updates and able to display them.
```c#
protected virtual void OnProcessedDataReceived(object sender, WeatherInfoEventArgs e)
{
   Console.WriteLine($"Hey {Name}, Temperature at {e.Location} is {e.Celsius}°C" +
       "| {e.Fahrenheit}°F. Updated on {e.UpdatedOn}");
}
```

## Running the website locally
The application is build on C# and .NET Core 3.1 Console Application. This application can be executed from a command prompt by running the following command.

```cmd
dotnet build
dotnet run
```
Another option is to run it locally from the Visual Studio.
Please see a sample screenshot of the application running.
![Application_Screenshot](https://postimg.cc/jL0NBTCs)

## TODOs
As per the current implementation, all the subscribers will get notified about all the weather updates. Needed to do additional changes in the WeatherChannel if we wanted to filter the notifications.
