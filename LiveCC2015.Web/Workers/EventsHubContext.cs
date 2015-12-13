using LiveCC2015.Data.Models;
using LiveCC2015.Web.Controllers.Hubs;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveCC2015.Web.Workers
{
    public class EventsHubContext
    {
        public void NewEvent(Event newEvent)
        {
            eventsHubContext.Clients.All.NewEvent(newEvent);
        }

        public void UpdatedEvent(Event updatedEvent)
        {
            eventsHubContext.Clients.All.UpdatedEvent(updatedEvent);
        }

        public void DeletedEvent(int deletedEventId)
        {
            eventsHubContext.Clients.All.DeletedEvent(deletedEventId);
        }

        public IHubContext UnderlyingHubContext
        {
            get { return eventsHubContext; }
        }

        public EventsHubContext()
        {
            eventsHubContext = GlobalHost.ConnectionManager.GetHubContext<EventsHub>();
        }

        IHubContext eventsHubContext;
    }
}
