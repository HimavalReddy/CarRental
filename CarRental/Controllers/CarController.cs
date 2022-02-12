using CarRental.Business;
using CarRental.EFRepository;
using CarRental.Entity;
using CarRental.Entity.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.Controllers
{
    public class CarController : Controller
    {
        private CarManager manager;
        private Repository Repo;
        public CarController()
        {
            manager = new CarManager();
            Repo = new Repository();
        }
        // GET: Car
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Search(Search CarSearch)
        {
            if (CarSearch.CarType == null || CarSearch.DropDate == null || CarSearch.hourDrop == null || CarSearch.hourpick == null || CarSearch.Package == null || CarSearch.PickDate == null)
            {
                ViewBag.error = "Fill All The Data Correctly";
                return PartialView("indexView");
            }
            CarSearch.Drop = Convert.ToDateTime(CarSearch.DropDate + " " + CarSearch.Droptime + ":00 " + CarSearch.hourDrop);
            CarSearch.Pick = Convert.ToDateTime(CarSearch.PickDate + " " + CarSearch.Picktime + ":00 " + CarSearch.hourpick);
            CarSearch.NoOfHours = (CarSearch.Drop - CarSearch.Pick).TotalHours;
            if (CarSearch.NoOfHours <= 0)
            {
                ViewBag.error = "Pick time cannot be greater than or equal to dropping time";
                return PartialView("indexView");
            }
            if ((DateTime.Now - CarSearch.Drop).TotalHours > 0 || (DateTime.Now - CarSearch.Pick).TotalHours > 0)
            {
                ViewBag.error = "you can't change the past but next time Book early";
                return PartialView("indexView");
            }
            List<Car> Cars = Repo.GetAllCarsBySearch(CarSearch);
            foreach (var car in Cars)
            {
                car.RentalCharge = car.GetCost(CarSearch);
            }
            ViewBag.SearchObj = CarSearch;
            return PartialView("CarViewP", Cars);
        }
        [Authorize]
        public ActionResult Payment(int carid, Search CarSearch)
        {
            string User = HttpContext.User.Identity.Name;
            Payment TotalPayment = manager.confirmBooking(CarSearch, carid);
            ViewBag.SearchObj = CarSearch;
            ViewBag.Carid = carid;
            ViewBag.url = "AddBooking";
            return View("PaymentView", TotalPayment);
        }
    }
}