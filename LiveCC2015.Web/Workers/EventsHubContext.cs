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
        public void NewEvent()
        {
            eventsHubContext.Clients.All.NewEvent();
        }

        public void UpdatedEvent()
        {
            eventsHubContext.Clients.All.UpdatedEvent();
        }

        public void DeletedEvent()
        {
            eventsHubContext.Clients.All.DeletedEvent();
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
