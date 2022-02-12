using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity
{
    public class Customer
    {
        [key]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public int Age { get; set; }
        public List<Booking> Bookings { get; set; }
        [ForeignKey("CustomerDetail")]
        public int CustomerDetailsId { get; set; }
        public CustomerDetails CustomerDetail { get; set; }
    }
}
