using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public class ScheduleStd
    {
        public int Id { get; set; }
        public string Station { get; set; }
        public string Train { get; set; }
        public TimeSpan TimeArrival { get; set; }


        public ScheduleStd(string station, string train, TimeSpan timeArrival)
        {
            Station = station;
            Train = train;
            TimeArrival = timeArrival;
        }

    }
}
