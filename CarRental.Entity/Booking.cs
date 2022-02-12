using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public DateTime DOB { get; set; }
        public DateTime DOTS { get; set; }
        [Required]
        public DateTime DOTE { get; set; }
        public DateTime HandOverDate { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Car")]
        public int CarId { get; set; }
        public Car Car { get; set; }
        //public Package Package { get; set; }
        public string Package { get; set; }
        public Boolean IsDoorDelivery { get; set; }
        public double TotalAmount { get; set; }
        public double DistanceTraveled { get; set; }
    }
}
