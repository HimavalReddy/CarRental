using CarRental.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.EFRepository
{
    class CarDBInitializer : DropCreateDatabaseAlways<CarRentalDBContext>
    {
        protected override void Seed(CarRentalDBContext context)
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category {CategoryId = 1, CategoryType = "Basic", AddKmCharge=11, PerHourCharge = 30, DoorDeliveryCharge = 100, SecurityDeposit = 10000 });
            categories.Add(new Category { CategoryId = 2, CategoryType = "Sedan", AddKmCharge = 12, PerHourCharge = 40, DoorDeliveryCharge = 130, SecurityDeposit = 20000 });
            categories.Add(new Category { CategoryId = 3, CategoryType = "SUV", AddKmCharge = 14, PerHourCharge = 50, DoorDeliveryCharge = 180, SecurityDeposit = 30000 });
            categories.Add(new Category { CategoryId = 4, CategoryType = "Luxury", AddKmCharge = 18, PerHourCharge = 80, DoorDeliveryCharge = 250, SecurityDeposit = 50000 });

            context.Category.AddRange(categories);

            List<Car> Cars = new List<Car>();
            Cars.Add(new Car() { Model = "Maruti Swift", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/suzukiswift_4.jpg", CategoryId = categories[0].CategoryId });
            Cars.Add(new Car() { Model = "Maruti Dzire", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/suzukidzire_4.jpg", CategoryId = categories[1].CategoryId });
            Cars.Add(new Car() { Model = "Hyundai Grand i10", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/hyundaii10_4.jpg", CategoryId = categories[0].CategoryId });
            Cars.Add(new Car() { Model = "Mahindra Scorpio", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/mahindrascorpio_4.jpg", CategoryId = categories[2].CategoryId });
            Cars.Add(new Car() { Model = "Chevrolet Spark", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/chevroletspark_4.jpg", CategoryId = categories[0].CategoryId });
            Cars.Add(new Car() { Model = "Mercedes E Class", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/mercedeseclass_4.jpg", CategoryId = categories[3].CategoryId });
            Cars.Add(new Car() { Model = "Honda Amaze", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/hondaamaze_4.jpg", CategoryId = categories[0].CategoryId });
            Cars.Add(new Car() { Model = "Honda City", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/hondacity_4.jpg", CategoryId = categories[1].CategoryId });
            Cars.Add(new Car() { Model = "BMW X3", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/bmwx3_4.jpg", CategoryId = categories[3].CategoryId });
            Cars.Add(new Car() { Model = "Hyundai Creta", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/hyundaicreta_4.jpg", CategoryId = categories[2].CategoryId });
            Cars.Add(new Car() { Model = "Nissan Altima", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/nissanaltima_4.jpg", CategoryId = categories[1].CategoryId });
            Cars.Add(new Car() { Model = "Ford EcoSport", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/fordecosport_4.jpg", CategoryId = categories[1].CategoryId });
            Cars.Add(new Car() { Model = "Honda Brio", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/hondabrio_4.jpg", CategoryId = categories[0].CategoryId });
            Cars.Add(new Car() { Model = "Renault Duster", MeterReading = 0, Capacity = 5, RentalCharge = 110, CarImageUrl = "https://cars.cdn.easyterra.com/renaultduster_4.jpg", CategoryId = categories[2].CategoryId });

            
            context.Cars.AddRange(Cars);

            base.Seed(context);
        }
    }
}
