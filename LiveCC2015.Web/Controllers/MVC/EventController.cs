using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using LiveCC2015.Data;
using LiveCC2015.Data.Models;
using LiveCC2015.Web.Workers;
using Microsoft.AspNet.SignalR;
using LiveCC2015.Web.Controllers.Hubs;

namespace LiveCC2015.Web.Controllers.MVC
{
    public class EventController : Controller
    {
        // GET: Event
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult View(int id)
        {
            var ev = eventRepo.FindEvent(id);

            return View(ev);
        }

        public ActionResult Edit(int id)
        {
            var ev = eventRepo.FindEvent(id);

            return View(ev);
        }

        [HttpPost]
        public ActionResult Edit(Event updatedEvent)
        {
            if (!ModelState.IsValid)
                return View(updatedEvent);

            var result = eventRepo.UpdateEvent(updatedEvent);
            EventsHub.UpdatedEvent();

            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Event newEvent)
        {
            if (!ModelState.IsValid)
                return View(newEvent);

            eventRepo.AddEvent(newEvent);
            EventsHub.NewEvent();

            return RedirectToAction("Index");
        }

        [HttpDelete]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var success = eventRepo.DeleteEvent(id);
            EventsHub.DeletedEvent();

            return RedirectToAction("Index");
        }
        
        //Using DI to get the repository and events hub context.
        //We always need the repo so we don't bother wrapping in Lazy<> but hub is only needed on create / update
        //so there's no need to instantiate it every time. Let DI take care of creating on demand.
        public EventController(EventRepository eventRepository, Lazy<EventsHubContext> eventsHubContext)
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