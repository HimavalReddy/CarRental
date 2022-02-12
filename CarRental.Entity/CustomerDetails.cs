using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity
{
    public class CustomerDetails
    {
        [key]
        public int CustomerDetailsId { get; set; }
        public string DoorNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string DrivingLicenceNo { get; set; }
        public string AadharNo { get; set; }
    }
}
