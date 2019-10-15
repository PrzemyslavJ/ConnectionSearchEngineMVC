using System;
using System.Collections.Generic;

namespace ConnectionSearchEngineMVC.Models
{
    public partial class Ska3KrkTar
    {
        public int Id { get; set; }
        public string Station { get; set; }
        public string Train { get; set; }
        public TimeSpan TimeArrival { get; set; }
    }
}
