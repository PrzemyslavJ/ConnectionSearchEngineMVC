using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public interface IgetRegisterRecords
    {
        IEnumerable<ReservationRegister> GetAllRegisters { get; }
        ReservationRegister select(int id);
        void Add(ReservationRegister AdddReserv);
        void Update(ReservationRegister UpdReserv);
        void Delete(int id);
    }
}
 