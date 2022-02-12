using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity.Model
{
    public class Payment
    {
        public double RentalCharges { get; set; }
        public double PackageCharges { get; set; }
        public double SecurityDeposit { get; set; }
        public double DoorDeliveryCharge { get; set; }
        public double PerHourCharge { get; set; }
        public double AddKmCharge { get; set; }
        public double AdditionalCharge { get; set; }
        public double Tax { get; set; }
        public double Paid { get; set; }
        public double TotalPay { get; set; }
    }
}
