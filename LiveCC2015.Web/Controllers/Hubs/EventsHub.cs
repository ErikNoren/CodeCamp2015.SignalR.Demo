using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace LiveCC2015.Web.Controllers.Hubs
{
    public class EventsHub : Hub
    {
        //We put in only events that we expect browsers to initiate using
        //hub.server.<method>

        //Since we're not doing browser-initiated communication, we just need
        //an empty hub so it gets registered, tracks clients, etc.

        //We get a reference to this context in Workers/EventsHubContext.cs for
        //sending messages from WebAPI or MVC controllers
    }
}