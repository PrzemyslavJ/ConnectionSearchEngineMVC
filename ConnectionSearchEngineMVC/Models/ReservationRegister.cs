using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public partial class ReservationRegister
    {
        public int Id {get; set;}
        [Required(ErrorMessage = "Proszę podać typ biletu! ")]
        public string TypeOfTicket { get; set; }
        [Required(ErrorMessage = "Proszę podać swoje imię i nazwisko! ")]
        public string SurName { get; set; }
        [Required(ErrorMessage = "Proszę podać swój email! ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Proszę podać swój numer telefonu! ")]
        public string Phone { get; set; }
        public string FirstStation { get; set; }
        public string SecondStation { get; set; }
        public TimeSpan? FirstArrival { get; set; }
        public TimeSpan? SecondArrival { get; set; }
        public string Train { get; set; }
        public DateTime Date { get; set; }
    }
}
