using CarRental.EFRepository;
using CarRental.Entity;
using CarRental.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Business
{
    public class CarManager
    {
        private Repository Repo;
        private Payment payment;
        public CarManager()
        {
            Repo = new Repository();
        }
        public Boolean AddCustomer(Customer Customer)
        {
            Repo.AddCustomer(Customer);
            return true;
        }
        public List<Booking> GetAllBookings(string name)
        {
            return Repo.GetAllBookings(name);
        }
        public Payment confirmBooking(Booking CarBooking)
        {
            payment = Repo.confirmBooking(CarBooking);
            payment.Tax = 0.18 * payment.TotalPay;
            payment.TotalPay += payment.Tax;
            return payment;
        }
        public Payment confirmBooking(Search CarSearch, int carid)
        {
            payment = Repo.confirmBooking(CarSearch, carid); ;
            payment.Tax = 0.18 * payment.TotalPay;
            payment.TotalPay += payment.Tax;
            return payment;
        }
        public Booking GetBookingById(int id)
        {
            return Repo.GetBookingById(id);
        }
        public Boolean UpdateBooking(Booking booking)
        {
            return Repo.UpdateBooking(booking);
        }
        public Billing CalculateFinalBill(Booking booking)
        {
            Billing FinalBill = new Billing();
            Car FinalCar = Repo.GetCarById(booking.CarId);
            double ExtraTime = (booking.HandOverDate - booking.DOTE).TotalHours;
            //double AmountPaid = (booking.TotalAmount / 0.18);
            double ExtraCost = 0;
            int PackageDistance = 0;
            FinalBill.AmountPaid = booking.TotalAmount;
            FinalBill.SecurityDeposit = FinalCar.Category.SecurityDeposit;
            if(ExtraTime > 1)
            {
                ExtraCost = FinalCar.Category.PerHourCharge * ExtraTime;
                if (Repo.CarHasBooking(booking.CarId, booking.DOTS, booking.HandOverDate, booking.BookingId))
                {
                    FinalBill.AdditionalTimeCost = ExtraCost * 2;
                }
                else
                {
                    FinalBill.AdditionalTimeCost = ExtraCost + (ExtraCost * 0.5);
                }
            }
            if (booking.Package.Contains("0"))
            {
                if (int.TryParse(booking.Package.Substring(7,10),out PackageDistance))
                {
                    if(booking.DistanceTraveled > PackageDistance)
                    {
                        FinalBill.AdditionalDistanceCost = (booking.DistanceTraveled - PackageDistance) * FinalCar.Category.AddKmCharge;
                    }
                }

            }
            FinalBill.Amount = FinalBill.SecurityDeposit - (FinalBill.AdditionalDistanceCost + FinalBill.AdditionalTimeCost);
            return FinalBill;
        }
        public Billing CalculateFinalBillForCancel(Booking booking)
        {
            Billing FinalCancelBill = new Billing();
            Car FinalCar = Repo.GetCarById(booking.CarId);
            FinalCancelBill.AmountPaid = booking.TotalAmount;
            FinalCancelBill.SecurityDeposit = FinalCar.Category.SecurityDeposit;
            double BookingTime = (booking.DOTE - booking.DOTS).TotalHours;
            //double Tax = 0.18 * payment.TotalPay;
            //double AmountPaid = (booking.TotalAmount);
            double CancelTime = (DateTime.Now - booking.DOTS).TotalHours;
            double CancelCharge = 0;
            if (BookingTime <= 24)
            {
                FinalCancelBill.CancellationCharges = booking.TotalAmount;
            }
            else
            {
                if(CancelTime > 24)
                {
                    FinalCancelBill.CancellationCharges = 200;
                }
                else if (CancelTime > 1)
                {
                    CancelCharge = 0.5 * booking.TotalAmount;
                    if(200 > CancelCharge)
                    {
                        FinalCancelBill.CancellationCharges = 200;
                    }
                    else
                    {
                        FinalCancelBill.CancellationCharges = CancelCharge;
                    }
                }
                else
                {
                    FinalCancelBill.CancellationCharges = booking.TotalAmount;
                }
                FinalCancelBill.ProcessingFee = 0.03 * (booking.TotalAmount - FinalCancelBill.CancellationCharges);
            }
            FinalCancelBill.Amount = booking.TotalAmount - (FinalCancelBill.ProcessingFee + FinalCancelBill.CancellationCharges) + FinalCancelBill.SecurityDeposit;
            return FinalCancelBill;
        }
        
    }
}
