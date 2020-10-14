using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public class ReservationClass: IgetRegisterRecords
    {
        private RailwayConnectionOfLesserPolandContext context = new RailwayConnectionOfLesserPolandContext();

        public IEnumerable<ReservationRegister> GetAllRegisters
        {
            get { return context.ReservationRegister; }
        }

        public ReservationRegister select(int id)
        {
            return context.ReservationRegister.Where(x => x.Id == id).FirstOrDefault();
        }

        public void Delete(int id)
        {
            var DeleteRec = context.ReservationRegister.Where(x => x.Id == id).FirstOrDefault();
            context.ReservationRegister.Remove(DeleteRec);
            context.SaveChanges();
        }

        public void Update(ReservationRegister UpdReserv)
        {
            var EditableRes = context.ReservationRegister.Where(x => x.Id == UpdReserv.Id).FirstOrDefault();
            context.ReservationRegister.Remove(EditableRes);
            context.SaveChanges();
            context.ReservationRegister.Add(UpdReserv);
            context.SaveChanges();
        }

        public void Add(ReservationRegister AdddReserv)
        {
            context.ReservationRegister.Add(AdddReserv);
            context.SaveChanges();
        }
    }
}
