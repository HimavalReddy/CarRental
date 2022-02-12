using CarRental.Entity;
using CarRental.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.EFRepository
{
    public class Repository
    {
        private List<Car> cars;
        private CarRentalDBContext Context;
        private Payment payment;
        private Booking booking;
        private Car car;
        public Repository()
        {
            Context = new CarRentalDBContext();
        }
        public List<Car> GetAllCarsBySearch(Search CarSearch)
        {
            cars = Context.Cars.Include("Category").Where(c => c.Category.CategoryType.Equals(CarSearch.CarType)).ToList();
            for (int i = 0; i < cars.Count; i++)
            {
                if (CarHasBooking(cars[i].CarId, CarSearch.Pick, CarSearch.Drop))
                {
                    cars.Remove(cars[i]);
                }
            }
            return cars;
        }
        public Boolean AddCustomer(Customer Customer)
        {
            Context.Customers.Add(Customer);
            Context.SaveChanges();
            return true;
        }
        public int AddBooking(int carid, Search Searchobj, String UserName, double TotalPay)
        {
            car = GetCarById(carid);
            Boolean IsDoor = false;
            Customer cust = GetCustomerByName(UserName);
            if (Searchobj.DoorStep != null)
            {
                IsDoor = true;
            }
            booking = new Booking { CarId = car.CarId, DOTS = Searchobj.Pick, DOTE = Searchobj.Drop, DOB = DateTime.Now, TotalAmount = TotalPay, IsDoorDelivery = IsDoor, Package = Searchobj.Package, CustomerId = cust.CustomerId, HandOverDate = Searchobj.Drop };
            Context.Booking.Add(booking);
            Context.SaveChanges();
            return booking.BookingId;
        }
        public List<Booking> GetAllBookings(string UserName)
        {
            Customer Customer = GetCustomerByName(UserName);
            return Context.Booking.Where(c => c.Customer.CustomerId == Customer.CustomerId).ToList();
        }
        public Payment confirmBooking(Booking CarBooking)
        {
            payment = new Payment();
            car = GetCarById(CarBooking.CarId);
            //if (CarBooking.IsDoorDelivery)
            //    payment.DoorDeliveryCharge = car.Category.DoorDeliveryCharge;
            //payment.SecurityDeposit = car.Category.SecurityDeposit;
            //double bookingHour = (CarBooking.DOTE - CarBooking.DOTS).TotalHours;
            //payment.RentalCharges = car.RentalCharge + (car.Category.PerHourCharge * (bookingHour - 1));
            //payment.Paid = GetBookingById(CarBooking.BookingId).TotalAmount;
            //if (CarBooking.Package.Equals("limited120"))
            //{
            //    payment.PackageCharges = 100;
            //}
            //else if (CarBooking.Package.Equals("limited240"))
            //{
            //    payment.PackageCharges = 160;
            //}
            //else
            //{
            //    payment.PackageCharges = 500;
            //}
            //payment.TotalPay = payment.PackageCharges + payment.RentalCharegs + payment.DoorDeliveryCharge + payment.SecurityDeposit;
            Booking OldBooking = GetBookingById(CarBooking.BookingId);
            double ExtraTime = (CarBooking.DOTE - OldBooking.DOTE).TotalHours;
            double ExtensionReqTime = (OldBooking.DOTE - DateTime.Now).TotalHours;
            payment.RentalCharges = ExtraTime * car.Category.PerHourCharge;
            if(CarHasBooking(CarBooking.CarId, CarBooking.DOTS, CarBooking.DOTE, CarBooking.BookingId))
            {
                payment.AdditionalCharge = payment.RentalCharges * 2;
            }
            else
            {
                if(ExtensionReqTime >= 24)
                {
                    payment.AdditionalCharge = 0.3 * payment.RentalCharges;
                }
                else
                {
                    payment.AdditionalCharge = 0.5 * payment.RentalCharges;
                }
            }
            payment.TotalPay = payment.AdditionalCharge + payment.RentalCharges;
            return payment;
        }
        public Payment confirmBooking(Search SearchObj, int carid)
        {
            payment = new Payment();
            car = GetCarById(carid);
            if (SearchObj.DoorStep != null)
                payment.DoorDeliveryCharge = car.Category.DoorDeliveryCharge;
            payment.SecurityDeposit = car.Category.SecurityDeposit;
            SearchObj.NoOfHours = (SearchObj.Drop - SearchObj.Pick).TotalHours;
            payment.RentalCharges = car.RentalCharge + (car.Category.PerHourCharge * (SearchObj.NoOfHours - 1));
            payment.PerHourCharge = car.Category.PerHourCharge;
            if (SearchObj.Package.Equals("limited120"))
            {
                payment.PackageCharges = 100;
            }
            else if (SearchObj.Package.Equals("limited240"))
            {
                payment.PackageCharges = 160;
            }
            else
            {
                payment.PackageCharges = 500;
            }
            payment.TotalPay = payment.PackageCharges + payment.RentalCharges + payment.DoorDeliveryCharge + payment.SecurityDeposit;
            return payment;
        }
        public Car GetCarById(int id)
        {
            return Context.Cars.Include("Category").Include("Bookings").FirstOrDefault(c => c.CarId == id);
        }
        public Booking GetBookingById(int id)
        {
            return Context.Booking.FirstOrDefault(b => b.BookingId == id);
        }
        public Customer GetCustomerByName(string Name)
        {
            return Context.Customers.FirstOrDefault(c => c.CustomerName.Equals(Name));
        }
        public Boolean UpdateBooking(Booking UpBooking)
        {
            booking = GetBookingById(UpBooking.BookingId);
            //if ((booking.DOTE - UpBooking.DOTE).TotalHours > 0)
            //{
            //    return false;
            //}
            booking.DistanceTraveled = UpBooking.DistanceTraveled;
            booking.DOTE = UpBooking.DOTE;
            booking.HandOverDate = UpBooking.HandOverDate;
            booking.TotalAmount = UpBooking.TotalAmount;
            Context.SaveChanges();
            return true;
        }
        public double calcuateTotalPayment(Booking booking)
        {
            double Total = 0;
            string IsDoor = null;
            if (booking.IsDoorDelivery)
            {
                IsDoor = "Yes";
            }
            Double hour = (booking.DOTS - booking.DOTE).TotalHours - (booking.DOTS - booking.DOTE).TotalHours;
            car = GetCarById(booking.CarId);
            Search SearchObj = new Search { CarType = car.Category.CategoryType, NoOfHours = hour, DoorStep = IsDoor, Droptime = 0, Package = booking.Package, Picktime = 0 };
            Total = car.GetCost(SearchObj);
            return Total;
        }
        public Boolean CarHasBooking(int CarId, DateTime Start, DateTime End, int? BookingId = null)
        {
            Boolean HasBooking = false;
            car = GetCarById(CarId);
            foreach (var booking in car.Bookings)
            {
                if (booking.BookingId != BookingId)
                {
                    if ((Start - booking.DOTE).TotalHours < 1 && (booking.DOTS - End).TotalHours < 1)
                    {
                        HasBooking = true;
                    }
                }
            }
            return HasBooking;
        }
    }
}
