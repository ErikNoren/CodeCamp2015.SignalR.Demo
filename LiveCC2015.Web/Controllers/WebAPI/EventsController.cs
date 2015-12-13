using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using LiveCC2015.Data;
using LiveCC2015.Data.Models;
using Microsoft.AspNet.SignalR;
using LiveCC2015.Web.Controllers.Hubs;
using LiveCC2015.Web.Workers;

namespace LiveCC2015.Web.Controllers.WebAPI
{
    public class EventsController : ApiController
    {
        public IHttpActionResult Get()
        {
            return Ok(eventRepo.GetEvents());
        }

        public IHttpActionResult Get(int id)
        {
            return Ok(eventRepo.FindEvent(id));
        }

        public IHttpActionResult Put(int id, Event updatedEvent)
        {
            if (id != updatedEvent.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            var result = eventRepo.UpdateEvent(updatedEvent);
            EventsHub.UpdatedEvent(updatedEvent);

            return Ok(result);
        }

        public IHttpActionResult Post(Event newEvent)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = eventRepo.AddEvent(newEvent);
            EventsHub.NewEvent(newEvent);

            return Ok(result);
        }

        public IHttpActionResult Delete(int id)
        {
            var found = eventRepo.FindEvent(id);
            if (found == null)
                return NotFound();

            var success = eventRepo.DeleteEvent(id);
            if (success) EventsHub.DeletedEvent(id);

            return Ok(success);
        }

        //Using DI to get the repository and events hub context.
        //We always need the repo so we don't bother wrapping in Lazy<> but hub is only needed on create / update
        //so there's no need to instantiate it every time. Let DI take care of creating on demand.
        public EventsController(EventRepository eventRepository, Lazy<EventsHubContext> eventsHubContext)
        {
            eventRepo = eventRepository;
            eventsHubCtx = eventsHubContext;
        }

        EventsHubContext EventsHub
        {
            get { return eventsHubCtx.Value; }
        }

        EventRepository eventRepo;
        Lazy<EventsHubContext> eventsHubCtx;
    }
}
