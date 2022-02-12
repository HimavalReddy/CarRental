using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.Entity.Model
{
    public class Search
    {
        public string PickDate { get; set; }
        public int Picktime { get; set; }
        public string hourpick { get; set; }
        public string DropDate { get; set; }
        public int Droptime { get; set; }
        public string hourDrop { get; set; }
        public string CarType { get; set; }
        public string Package { get; set; }
        public string DoorStep { get; set; }
        public Double NoOfHours { get; set; }
        public DateTime Drop { get; set; }
        public DateTime Pick { get; set; }

    }
}