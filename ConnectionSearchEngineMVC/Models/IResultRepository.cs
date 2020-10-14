using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public interface IResultRepository
    {
        IEnumerable <AllRecords> GetAllRoutes { get; }
        IEnumerable<ListToModel> SearchResultRecords(string FirstPlace, string SecondPlace, TimeSpan time,IEnumerable<AllRecords> allrecs);
        IEnumerable<ScheduleStd> SingleSchedule(int id);
    }
}
