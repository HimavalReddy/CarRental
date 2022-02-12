using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRental.Business;
using CarRental.Entity.Model;
using CarRental.Entity;
using CarRental.EFRepository;

namespace CarRental.Controllers
{
    public class BookingController : Controller
    {
        private CarManager manager;
        private Repository Repo;
        public BookingController()
        {
            Repo = new Repository();
            manager = new CarManager();
        }
        [Authorize]
        public ActionResult ViewBooking()
        {
            string User = HttpContext.User.Identity.Name;
            return View("BookingView", manager.GetAllBookings(User));
        }
        [Authorize]
        public ActionResult ExtendBooking(int BookingId)
        {
            return View("BookingEditView", manager.GetBookingById(BookingId));
        }
        [HttpPost]
        [Authorize]
        public ActionResult UpdateBookingconfirm(Booking Booking, Search SearchObj)
        {
            SearchObj.Drop = Convert.ToDateTime(SearchObj.DropDate + " " + SearchObj.Droptime + ":00 " + SearchObj.hourDrop);
            Booking.DOTE = SearchObj.Drop;
            Booking CarBooking = manager.GetBookingById(Booking.BookingId);
            if ((CarBooking.DOTE-Booking.DOTE).TotalHours > 0)
            {
                ViewBag.UpdateBookingError = "Drop Off Date and Time was not in correct Format";
                return View("BookingEditView", CarBooking);
            }
            Payment TotalPayment = manager.confirmBooking(Booking);
            TotalPayment.Paid = Booking.TotalAmount;
            Booking.TotalAmount += TotalPayment.TotalPay;
            ViewBag.url = "UpdateBooking";
            ViewBag.Booking = Booking;
            return View("PaymentView", TotalPayment);
        }
        [Authorize]
        public ActionResult AddBooking(int carid, Search CarSearch, double TotalPay)
        {
            string User = HttpContext.User.Identity.Name;
            int id = Repo.AddBooking(carid, CarSearch, User, TotalPay);
            if (id > 0)
            {
                ViewBag.error = "Booked Successfully! Your Booking Id is CRB" + id + "Z";
            }
            else
            {
                ViewBag.error = "Error While Booking";
            }
            return View("index");
        }
        [Authorize]
        public ActionResult UpdateBooking(Booking booking)
        {
            string User = HttpContext.User.Identity.Name;
            if (manager.UpdateBooking(booking))
            {
                ViewBag.error = "Updated Successfully!";
            }
            else
            {
                ViewBag.error = "You Can only Extend the Booking";
            }
            return View("index");
        }
        [Authorize]
        public ActionResult Billing()
        {
            return View("Billing");
        }
        [Authorize]
        public ActionResult GetBooking(string BookingId, DateTime HandDT, string DistanceTraveled)
        {
            int id = 0;
            double distanceCovered = 0;
            string s = BookingId.Replace("CRB", string.Empty);
            s = s.Replace("Z", string.Empty);
            if(!int.TryParse(s, out id))
            {
                ViewBag.Invalid = "InValid Booking Id";
                return View("Billing");
            }
            Booking CarBooking = manager.GetBookingById(id);
            if(CarBooking == null)
            {
                ViewBag.Invalid = "InValid Booking Id";
                return View("Billing");
            }
            if(!double.TryParse(DistanceTraveled,out distanceCovered))
            {
                ViewBag.Invalid = "Fill the Details correctly";
                return View("Billing");
            }
            if ((DateTime.Now - CarBooking.DOTE).TotalHours > 0)
            {
                ViewBag.Invalid = "Billing Completed For the Given Booking Id";
                return View("Billing");
            }
            CarBooking.HandOverDate = HandDT;
            CarBooking.DistanceTraveled = distanceCovered;
            CarBooking.DOTE = DateTime.Now;
            Billing CarFinalBill = manager.CalculateFinalBill(CarBooking);
            ViewBag.Booking = CarBooking;
            CarBooking.TotalAmount = CarFinalBill.AmountPaid + CarFinalBill.AdditionalDistanceCost + CarFinalBill.AdditionalTimeCost;
            return PartialView("FinalPaymentView", CarFinalBill);
        }
        [Authorize]
        public ActionResult CancelBooking(int BookingId)
        {
            Booking CarBooking = manager.GetBookingById(BookingId);
            Billing CarFinalBill = manager.CalculateFinalBillForCancel(CarBooking);
            CarBooking.HandOverDate = DateTime.Now;
            CarBooking.DOTE = DateTime.Now;
            CarBooking.DOTS = DateTime.Now;
            CarBooking.TotalAmount = CarFinalBill.AmountPaid + CarFinalBill.CancellationCharges + CarFinalBill.ProcessingFee;
            ViewBag.Booking = CarBooking;
            return View("CancelView", CarFinalBill);
        }
        [Authorize]
        public ActionResult UpdateBookingCancelAndFinal(Booking CarBooking, string name)
        {
            if (manager.UpdateBooking(CarBooking))
            {
                ViewBag.error = name;
            }
            else
            {
                ViewBag.error = "Error While Processing Try Again";
            }
            return View("index");
        }
    }
}