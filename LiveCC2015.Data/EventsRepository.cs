using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveCC2015.Data.Models;

namespace LiveCC2015.Data
{
    public class EventRepository
    {
        public IEnumerable<Event> GetEvents()
        {
            return EventMockStorage;
        }

        public Event FindEvent(int eventId)
        {
            return EventMockStorage.SingleOrDefault(e => e.Id == eventId);
        }

        public Event AddEvent(Event newEvent)
        {
            newEvent.Id = GenerateNewEventId();
            EventMockStorage.Add(newEvent);

            return newEvent;
        }

        public Event UpdateEvent(Event updatedEvent)
        {
            var found = EventMockStorage.SingleOrDefault(e => e.Id == updatedEvent.Id);
            if (found != null)
                EventMockStorage.Remove(found);

            EventMockStorage.Add(updatedEvent);

            return updatedEvent;
        }

        public bool DeleteEvent(int eventId)
        {
            var found = EventMockStorage.SingleOrDefault(e => e.Id == eventId);
            if (found == null)
                return false;

            EventMockStorage.Remove(found);
            return true;
        }

        public EventRepository()
        {
            if (EventMockStorage.Count < 1)
            {
                lock (LockObject)
                {
                    if (EventMockStorage.Count < 1)
                    {
                        AddEvent(new Event() { Name = "Filler Event 1", Description = "Generic Description of Filler Event 1", StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) });
                        AddEvent(new Event() { Name = "Filler Event 2", Description = "Generic Description of Filler Event 2", StartDate = DateTime.Now.AddDays(3), EndDate = DateTime.Now.AddDays(4) });
                        AddEvent(new Event() { Name = "Filler Event 3", Description = "Generic Description of Filler Event 3", StartDate = DateTime.Now.AddDays(8), EndDate = DateTime.Now.AddDays(10) });
                        AddEvent(new Event() { Name = "Filler Event 4", Description = "Generic Description of Filler Event 4", StartDate = DateTime.Now.AddDays(10), EndDate = DateTime.Now.AddDays(11) });
                    }
                }
            }
        }

        private static readonly List<Event> EventMockStorage = new List<Event>();
        private static readonly object LockObject = new object();

        private int GenerateNewEventId()
        {
            if (EventMockStorage.Count < 1)
            {
                return 1;
            }

            return EventMockStorage.Max(e => e.Id) + 1;
        }
    }
}
