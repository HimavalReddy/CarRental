using CarRental.Entity.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Entity
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public string Model { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        //public FuelType FuelType { get; set; }
        public double MeterReading { get; set; }
        public int Capacity { get; set; }
        public double RentalCharge { get; set; }
        public List<Booking> Bookings { get; set; }
        public string CarImageUrl { get; set; }

        public double GetCost(Search CarSearch)
        {
            double cost = RentalCharge;
            double time = CarSearch.NoOfHours;
            if (CarSearch.Package.Equals("limited120"))
            {
                cost += 100;
            }
            else if (CarSearch.Package.Equals("limited240"))
            {
                cost += 160;
            }
            else
            {
                cost += 500;
            }
            if(time > 1)
            {
                cost += Category.PerHourCharge * (time - 1);
            }
            if(CarSearch.DoorStep != null)
            {
                cost += Category.DoorDeliveryCharge;
            }
            return cost;
        }
    }
}
