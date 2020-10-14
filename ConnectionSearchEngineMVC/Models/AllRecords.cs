using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public class AllRecords : IResultRepository
    {
        
        private RailwayConnectionOfLesserPolandContext context = new RailwayConnectionOfLesserPolandContext();

        public int Id { get; set; }
        public string Station { get; set; }
        public string Train { get; set; }
        public TimeSpan TimeArrival { get; set; }
        public int IdRoute { get; set; }

        public IEnumerable<AllRecords> GetAllRoutes
        {
            get {
                var allrec = context.Ska1KrkWiel.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 1 }).
                                  Union(context.Ska1WielKrk.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 2 })).
                                  Union(context.Ska2KrkSed.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 3 })).
                                  Union(context.Ska2SedKrk.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 4 })).
                                  Union(context.Ska3KrkTar.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 5 })).
                                  Union(context.Ska3TarKrk.Select(x => new { x.Id, x.Station, x.Train, x.TimeArrival, IdRoute = 6 })).ToList();

                List<AllRecords> AllRec = new List<AllRecords>();

                foreach (var i in allrec)
                {
                    AllRec.Add(new AllRecords() { Id = i.Id, Station = i.Station, Train = i.Train, TimeArrival = i.TimeArrival, IdRoute = i.IdRoute });
                }
                return AllRec;
            }
        }


        
        public IEnumerable<ListToModel> SearchResultRecords(string FirstPlace, string SecondPlace, TimeSpan time, IEnumerable<AllRecords> allrecs)
        {
            TimeSpan timeBorder = time.Add(TimeSpan.FromHours(23));
            var FirstResult = from x in allrecs
                              where x.Station == FirstPlace && (x.TimeArrival >= time && x.TimeArrival <= timeBorder)
                              select new { x.Id, x.Station, x.Train, x.TimeArrival, x.IdRoute };

            List<ListToModel> ModelList = new List<ListToModel>();


            foreach (var x in FirstResult)
            {
                var SecondResult = from y in allrecs
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

            return ModelList;
        }

        public IEnumerable<ScheduleStd> SingleSchedule(int idR)
        {
            List<ScheduleStd> schedule = new List<ScheduleStd>();
            var schedules = GetAllRoutes.Select(x => new { x.Station, x.Train, x.TimeArrival, x.IdRoute }).Where(x => x.IdRoute == idR).OrderBy(x => x.Train);
            foreach (var i in schedules)
                schedule.Add(new ScheduleStd(i.Station, i.Train, i.TimeArrival));

            return schedule;
        }
    }
}