using CarRental.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.EFRepository
{
    public class CarRentalDBContext :DbContext
    {
        public CarRentalDBContext() : base()
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<CarRentalDBContext>());
            Database.SetInitializer(new CarDBInitializer());
        }
        public DbSet<CarRental.Entity.CarRental> CarRental { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerDetails> CustomerDetails { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Booking> Booking { get; set; }
    }
}
