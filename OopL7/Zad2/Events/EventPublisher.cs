using System;
using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Zad2.Events
{
    public class EventPublisher : IEventPublisher
    {
        #region Private fields

        private readonly ConcurrentDictionary<Type, object> _subjects
            = new ConcurrentDictionary<Type, object>();

        #endregion
        #region IEventPublisher Members

        public IObservable<TEvent> GetEvent<TEvent>()
        {
            var subject =
                (ISubject<TEvent>) _subjects.GetOrAdd(typeof (TEvent),
                                                      t => new Subject<TEvent>());
            return subject.AsObservable();
        }

        public void Publish<TEvent>(TEvent sampleEvent)
        {
            object subject;
            if (_subjects.TryGetValue(typeof (TEvent), out subject))
            {
                ((ISubject<TEvent>) subject)
                    .OnNext(sampleEvent);
            }
        }

        #endregion
    }
}