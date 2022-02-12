namespace CarRental.EFRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        DOB = c.DateTime(nullable: false),
                        DOTS = c.DateTime(nullable: false),
                        DOTE = c.DateTime(nullable: false),
                        HandOverDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        Package = c.String(),
                        IsDoorDelivery = c.Boolean(nullable: false),
                        TotalAmount = c.Double(nullable: false),
                        DistanceTraveled = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        CarId = c.Int(nullable: false, identity: true),
                        Model = c.String(),
                        MeterReading = c.Double(nullable: false),
                        Capacity = c.Int(nullable: false),
                        RentalCharge = c.Double(nullable: false),
                        CarImageUrl = c.String(),
                        Category_CategoryId = c.Int(),
                        CarRental_CarRentalid = c.Int(),
                    })
                .PrimaryKey(t => t.CarId)
                .ForeignKey("dbo.Categories", t => t.Category_CategoryId)
                .ForeignKey("dbo.CarRentals", t => t.CarRental_CarRentalid)
                .Index(t => t.Category_CategoryId)
                .Index(t => t.CarRental_CarRentalid);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryType = c.String(),
                        AddKmCharge = c.Double(nullable: false),
                        PerHourCharge = c.Double(nullable: false),
                        DoorDeliveryCharge = c.Double(nullable: false),
                        SecurityDeposit = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(),
                        Password = c.String(),
                        PhoneNo = c.String(),
                        EmailId = c.String(),
                        Age = c.Int(nullable: false),
                        CustomerDetailsId = c.Int(nullable: false),
                        CarRental_CarRentalid = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId)
                .ForeignKey("dbo.CustomerDetails", t => t.CustomerDetailsId, cascadeDelete: true)
                .ForeignKey("dbo.CarRentals", t => t.CarRental_CarRentalid)
                .Index(t => t.CustomerDetailsId)
                .Index(t => t.CarRental_CarRentalid);
            
            CreateTable(
                "dbo.CustomerDetails",
                c => new
                    {
                        CustomerDetailsId = c.Int(nullable: false, identity: true),
                        DoorNo = c.String(),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        DrivingLicenceNo = c.String(),
                        AadharNo = c.String(),
                    })
                .PrimaryKey(t => t.CustomerDetailsId);
            
            CreateTable(
                "dbo.CarRentals",
                c => new
                    {
                        CarRentalid = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CarRentalid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CarRental_CarRentalid", "dbo.CarRentals");
            DropForeignKey("dbo.Cars", "CarRental_CarRentalid", "dbo.CarRentals");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "CustomerDetailsId", "dbo.CustomerDetails");
            DropForeignKey("dbo.Bookings", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "Category_CategoryId", "dbo.Categories");
            DropIndex("dbo.Customers", new[] { "CarRental_CarRentalid" });
            DropIndex("dbo.Customers", new[] { "CustomerDetailsId" });
            DropIndex("dbo.Cars", new[] { "CarRental_CarRentalid" });
            DropIndex("dbo.Cars", new[] { "Category_CategoryId" });
            DropIndex("dbo.Bookings", new[] { "CarId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropTable("dbo.CarRentals");
            DropTable("dbo.CustomerDetails");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
            DropTable("dbo.Cars");
            DropTable("dbo.Bookings");
        }
    }
}
