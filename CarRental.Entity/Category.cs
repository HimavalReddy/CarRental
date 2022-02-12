using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        //public CategoryType CategoryType { get; set; }
        public string CategoryType { get; set; }
        public double AddKmCharge { get; set; }
        public double PerHourCharge { get; set; }
        public double DoorDeliveryCharge { get; set; }
        public double SecurityDeposit { get; set; }
    }
}
