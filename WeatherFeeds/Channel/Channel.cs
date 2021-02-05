using System;
using System.Linq;

namespace WeatherFeeds.Channel
{
    public abstract class Channel<TEvent> where TEvent : EventArgs
    {
        public Channel() { }

        public event EventHandler<TEvent> ProcessingCompleted;

        public void Subscribe(string subscriber, EventHandler<TEvent> action)
        {
            if (ProcessingCompleted == null || !ProcessingCompleted.GetInvocationList().Contains(action))
            {
                ProcessingCompleted += action;
                Console.WriteLine($"'{subscriber}' subscribed");
            }
        }

        public void UnSubscribe(string subscriber, EventHandler<TEvent> action)
        {
            ProcessingCompleted -= action;
            Console.WriteLine($"'{subscriber}' unsubscribed");
        }

        protected virtual void OnProcessingCompleted(TEvent e)
        {
            ProcessingCompleted?.Invoke(this, e);
        }
    }
}
