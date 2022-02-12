using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity.Model
{
    public class Billing
    {
        public double AmountPaid { get; set; }
        public double SecurityDeposit { get; set; }
        public double AdditionalTimeCost { get; set; }
        public double AdditionalDistanceCost { get; set; }
        public double CancellationCharges { get; set; }
        public double ProcessingFee { get; set; }
        public double Amount { get; set; }

    }
}
