using System;

namespace Zad2.Events
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent sampleEvent);
        IObservable<TEvent> GetEvent<TEvent>(); 
    }
}