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
using Ninject;

namespace ConnectionSearchEngineMVC.Controllers
{
    public class HomeController : Controller
    {
        private IResultRepository RoutesRepository;
        private IgetRegisterRecords RegisterRepository;
        RailwayConnectionOfLesserPolandContext context = new RailwayConnectionOfLesserPolandContext();

        public HomeController(IResultRepository RoutesRepository, IgetRegisterRecords RegisterRepository)
        {
            this.RoutesRepository = RoutesRepository;
            this.RegisterRepository = RegisterRepository;
        }
        
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
        
        [HttpPost]
        public IActionResult Search(string FirstPlace, string SecondPlace, TimeSpan time)
        {
            var ListSearch = RoutesRepository.SearchResultRecords(FirstPlace, SecondPlace, time, RoutesRepository.GetAllRoutes);
            
            if (ListSearch.Count() > 0)
            {
                return View("Search", ListSearch);
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
            RailwayConnectionOfLesserPolandContext context = new RailwayConnectionOfLesserPolandContext();
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
            if (Login == "Przemek" && Password == "1234")
            {
                return View("AdministratorPanel", RegisterRepository.GetAllRegisters);
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
            RailwayConnectionOfLesserPolandContext context = new RailwayConnectionOfLesserPolandContext();
            if (ModelState.IsValid)
            {
                RegisterRepository.Add(reservationRegister);
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
            string[] NamesOfRoutes = new string[] { "Kraków - Wieliczka", "Wieliczka - Kraków", "Kraków - Sędziszów", "Sędziszów - Kraków", "Kraków - Tarnów", "Tarnów - Kraków" };
            ViewBag.Name = NamesOfRoutes[Option - 1];
            return View("Schedule", RoutesRepository.SingleSchedule(Option));
        }

        public IActionResult EditReservation(int Id)
        {
            return View("EditReservation", RegisterRepository.select(Id));
        }

        [HttpPost]
        public IActionResult EditReservation(ReservationRegister EditRecord)
        {
            if (ModelState.IsValid)
            {
                RegisterRepository.Update(EditRecord);
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
            return View("DeleteReservation", RegisterRepository.select(Id));
        }

        [HttpPost]
        public IActionResult DeleteReservationRec(int IdRec)
        {
                RegisterRepository.Delete(IdRec);
                ViewBag.Communicate = "Dokonano pomyślnego usunięcia rezerwacji !";
                return View("CommunicateView");
            
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}