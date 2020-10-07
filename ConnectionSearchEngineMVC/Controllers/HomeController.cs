using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConnectionSearchEngineMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;
using CaptchaMvc.HtmlHelpers;

namespace ConnectionSearchEngineMVC.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdministratorPage()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        RailwayConnectionOfLesserPolandContext context = new RailwayConnectionOfLesserPolandContext();
        

        [HttpPost]
        public IActionResult Search(string FirstPlace, string SecondPlace, TimeSpan time)
        {
            TimeSpan timeBorder = time.Add(TimeSpan.FromHours(23));

            var AllRecords = context.Ska1KrkWiel.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 1 }).
                              Union(context.Ska1WielKrk.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 2 })).
                              Union(context.Ska2KrkSed.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 3 })).
                              Union(context.Ska2SedKrk.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 4 })).
                              Union(context.Ska3KrkTar.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 5 })).
                              Union(context.Ska3TarKrk.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 6 }));


            var FirstResult = from x in AllRecords
                              where x.Station == FirstPlace && (x.TimeArrival >= time && x.TimeArrival <= timeBorder)
                              select new { x.Id, x.Station, x.Train, x.TimeArrival, x.IdRoute };

            List<ListToModel> ModelList = new List<ListToModel>();


            foreach (var x in FirstResult)
            {
                var SecondResult = from y in AllRecords
                                   where y.Station == SecondPlace && y.Train == x.Train && y.IdRoute == x.IdRoute && y.Id > x.Id
                                   select new { y.Station, y.TimeArrival };

                if (SecondResult.Count() > 0)
                {
                    string LastPlace = "";
                    TimeSpan? SecondArrival = new TimeSpan();

                    foreach (var p in SecondResult)
                    {
                        LastPlace = p.Station;
                        SecondArrival = p.TimeArrival;
                    }

                    ModelList.Add(new ListToModel(x.Station, LastPlace, x.TimeArrival, SecondArrival, x.Train));
                }
            }
            if (ModelList.Count() > 0)
            {
                return View("Search", ModelList);
            }
            else
            {
                ViewBag.Communicate = "Nie znaleziono szukanego połączenia !";
                return View("CommunicateView");
            }

        }


        [HttpGet]
        public IActionResult Reservation(string FS, TimeSpan? FA, string SS, TimeSpan? SA, string T)
        {
            ViewBag.IdCurrent = context.ReservationRegister.Count() + 1;
            ViewBag.FirstSt = FS;
            ViewBag.FirstAr = FA;
            ViewBag.SecondSt = SS;
            ViewBag.SecondAr = SA;
            ViewBag.Tr = T;
            return View("Reservation");
        }
        

        public IActionResult AdministratorPanel(string Login, string Password)

        {
            if (Login == "Przemek" || Password == "1234")
            {
                return View("AdministratorPanel",context.ReservationRegister);
            }
            else
            {
                ViewBag.FailCommunicate = "Błędne logowanie !";
                return View("AdministratorPage");
            }
        }

        [HttpPost]
        public IActionResult Reservation(ReservationRegister reservationRegister)
        {
            if (ModelState.IsValid)
            {
                context.ReservationRegister.Add(reservationRegister);
                context.SaveChanges();
                ViewBag.Communicate = "Dokonano pomyślnej rezerwacji !";
                return View("CommunicateView");
            }
            else
            {
                ViewBag.Communicate = "Wypełnij wszystkie dane !";
                return View("CommunicateView");
            }
        }


        [HttpGet]
        public IActionResult Schedule(int Option)
        {
            List<ScheduleStd> schedule = new List<ScheduleStd>();
            switch (Option)
            {
                case 1:
                    ViewBag.Name= "Trasa nr.1 Kraków - Wieliczka";
                    var schedules = context.Ska1KrkWiel.Select(x => new { x.Station, x.Train, x.TimeArrival }).OrderBy(x => x.Station);
                    foreach (var i in schedules)
                        schedule.Add(new ScheduleStd(i.Station, i.Train, i.TimeArrival));
                    break;
                case 2:
                    ViewBag.Name = "Trasa nr.2 Wieliczka - Kraków";
                    schedules = context.Ska1WielKrk.Select(x => new { x.Station, x.Train, x.TimeArrival }).OrderBy(x => x.Train);
                    foreach (var i in schedules)
                        schedule.Add(new ScheduleStd(i.Station, i.Train, i.TimeArrival));
                    break;
                case 3:
                    ViewBag.Name = "Trasa nr.2 Kraków - Sędziszów";
                    schedules = context.Ska2KrkSed.Select(x => new { x.Station, x.Train, x.TimeArrival }).OrderBy(x => x.Train);
                    foreach (var i in schedules)
                        schedule.Add(new ScheduleStd(i.Station, i.Train, i.TimeArrival));
                    break;
                case 4:
                    ViewBag.Name = "Trasa nr.2 Sędziszów - Kraków";
                    schedules = context.Ska2SedKrk.Select(x => new { x.Station, x.Train, x.TimeArrival }).OrderBy(x => x.Train);
                    foreach (var i in schedules)
                        schedule.Add(new ScheduleStd(i.Station, i.Train, i.TimeArrival));
                    break;
                case 5:
                    ViewBag.Name = "Trasa nr.3 Kraków - Tarnów";
                    schedules = context.Ska3KrkTar.Select(x => new { x.Station, x.Train, x.TimeArrival }).OrderBy(x => x.Train);
                    foreach (var i in schedules)
                        schedule.Add(new ScheduleStd(i.Station, i.Train, i.TimeArrival));
                    break;
                case 6:
                    ViewBag.Name = "Trasa nr.3 Tarnów - Kraków";
                    schedules = context.Ska3TarKrk.Select(x => new { x.Station, x.Train, x.TimeArrival }).OrderBy(x => x.Train);
                    foreach (var i in schedules)
                        schedule.Add(new ScheduleStd(i.Station, i.Train, i.TimeArrival));
                    break;
                default:
                    schedule = null;
                    break;
            }
            
            return View("Schedule", schedule);
        }

        public IActionResult EditReservation(int Id)
        {
            var EditableRes = context.ReservationRegister.Where(x => x.Id == Id).FirstOrDefault();
            return View("EditReservation", EditableRes);
        }

        [HttpPost]
        public IActionResult EditReservation(ReservationRegister EditRecord)
        {
            if (ModelState.IsValid)
            {
                var EditableRes = context.ReservationRegister.Where(x => x.Id == EditRecord.Id).FirstOrDefault();
                context.ReservationRegister.Remove(EditableRes);
                context.SaveChanges();
                context.ReservationRegister.Add(EditRecord);
                context.SaveChanges();
                ViewBag.Communicate = "Dokonano pomyślnej edycji rezerwacji !";
                return View("CommunicateView");
            }
            else
            {
                ViewBag.Communicate = "Wypełnij wszystkie dane !";
                return View("CommunicateView");
            }
            
        }

        public IActionResult DeleteReservation(int Id)
        {
            var ToDeleteRec = context.ReservationRegister.Where(x => x.Id == Id).FirstOrDefault();
            return View("DeleteReservation", ToDeleteRec);
        }

        [HttpPost]
        public IActionResult DeleteReservationRec(int IdRec)
        {
                var DeleteRec = context.ReservationRegister.Where(x => x.Id == IdRec).FirstOrDefault();
                context.ReservationRegister.Remove(DeleteRec);
                context.SaveChanges();
                ViewBag.Communicate = "Dokonano pomyślnego usunięcia rezerwacji !";
                return View("CommunicateView");
            
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}