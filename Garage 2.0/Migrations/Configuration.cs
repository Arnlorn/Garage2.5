namespace Garage_2._0.Migrations
{
    using Garage_2._0.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage_2._0.DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage_2._0.DataAccessLayer.RegisterContext context)
        {
            var MemberArray = new[] 
            {
                new Member{FirstName = "Adam", LastName = "Andersson", StreetAdress = "Abrahamsvägen 12", PostalCode = "136 36" ,City = "Arboga", PhoneNumber = "08-46774335" },
                new Member{FirstName = "Berit", LastName = "Blossom", StreetAdress = "Benjaminsbacken 2B", PostalCode = "550 17" ,City = "Borås", PhoneNumber = "033-45674785" },
                new Member{FirstName = "Cecilia", LastName = "Carlsson", StreetAdress = "Citrongränd 45", PostalCode = "673 32" ,City = "Charlottenberg", PhoneNumber = "0771 - 33 33 10" },
                new Member{FirstName = "Dave", LastName = "Duke", StreetAdress = "Tulsagatan 45", PostalCode = "673 32" ,City = "Oklahoma", PhoneNumber = "070-791 69 65" }
            };
            context.Members.AddOrUpdate(n => new { n.FirstName, n.LastName }, MemberArray);

            var TypeArray = new[]
            {
                new VehicleType{ Name = "Bil", Size = 3, Fee = 5 },
                new VehicleType{ Name = "Motorcykel", Size = 1, Fee = 2 },
                new VehicleType{ Name = "Båt", Size = 6, Fee = 10 },
                new VehicleType{ Name = "Buss", Size = 6, Fee = 10 },
                new VehicleType{ Name = "Lastbil", Size = 6, Fee = 10 },
                new VehicleType{ Name = "Flygplan", Size = 9, Fee = 15 }
            };
            context.VehicleTypes.AddOrUpdate(n => n.Name, TypeArray);

            context.SaveChanges();    
 
            context.ParkedVehicles.AddOrUpdate(r => r.RegNr,
            new ParkedVehicle
            {
                VehicleTypeName = TypeArray[5].Name,
                RegNr = "ABC123",
                Color = "Blue",
                Make = "Cessna",
                Model = "A307",
                NrOfWheels = 3,
                TimeStamp = DateTime.Now,
                MemberId = MemberArray[0].Id,
                ParkingSlot = 0
            },
            new ParkedVehicle
            {
                VehicleTypeName = TypeArray[1].Name,
                RegNr = "AFK365",
                Color = "Red",
                Make = "Kawasaki",
                Model = "125:a",
                NrOfWheels = 2,
                TimeStamp = DateTime.Now,
                MemberId = MemberArray[1].Id,
                ParkingSlot = 3
            },
           new ParkedVehicle
           {
               VehicleTypeName = TypeArray[1].Name,
               RegNr = "AFK001",
               Color = "Black",
               Make = "Harley Davidson",
               Model = "Iron 883",
               NrOfWheels = 2,
               TimeStamp = DateTime.Now,
               MemberId = MemberArray[2].Id,
               ParkingSlot = 3
           },
           new ParkedVehicle
           {
               VehicleTypeName = TypeArray[1].Name,
               RegNr = "HLP356",
               Color = "Green",
               Make = "Yamaha",
               Model = "R15B3",
               NrOfWheels = 2,
               TimeStamp = DateTime.Now,
               MemberId = MemberArray[2].Id,
               ParkingSlot = 3
           },
           new ParkedVehicle
           {
               VehicleTypeName = TypeArray[5].Name,
               RegNr = "ABC129",
               Color = "Blue",
               Make = "Cessna",
               Model = "A307",
               NrOfWheels = 3,
               TimeStamp = DateTime.Now,
               MemberId = MemberArray[0].Id,
               ParkingSlot = 4
           });
        }
    }
}
