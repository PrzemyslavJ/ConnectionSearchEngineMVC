using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectionSearchEngineMVC.Models
{
    public class ListToModel
    {
        RailwayConnectionOfLesserPolandContext context = new RailwayConnectionOfLesserPolandContext();
        public string FirstStation { get; set; }
        public string SecondStation { get; set; }
        public TimeSpan? FirstArrival { get; set; }
        public TimeSpan? SecondArrival { get; set; }
        public string Train { get; set; }
        
        public ListToModel(string FirstStation, string SecondStation, TimeSpan? FirstArrival, TimeSpan? SecondArrival, string Train)
        {
            this.FirstStation = FirstStation;
            this.SecondStation = SecondStation;
            this.FirstArrival = FirstArrival;
            this.SecondArrival = SecondArrival;
            this.Train = Train;
        }
        
    }
}
