using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity
{
    public class CarRental
    {
        [Key]
        public int CarRentalid { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; }
        public List<Customer> Customers { get; set; }

        public CarRental()
        {
            Cars = new List<Car>();
            Customers = new List<Customer>();
        }
    }
}
