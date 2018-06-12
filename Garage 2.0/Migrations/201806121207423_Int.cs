namespace Garage_2._0.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Int : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StreetAdress = c.String(),
                        PostalCode = c.String(),
                        City = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ParkedVehicles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RegNr = c.String(nullable: false),
                        Color = c.String(),
                        Make = c.String(),
                        Model = c.String(),
                        NrOfWheels = c.Int(nullable: false),
                        TimeStamp = c.DateTime(nullable: false),
                        ParkingSlot = c.Int(nullable: false),
                        MemberId = c.Int(nullable: false),
                        VehicleTypeName = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.VehicleTypes", t => t.VehicleTypeName)
                .Index(t => t.MemberId)
                .Index(t => t.VehicleTypeName);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        Size = c.Int(nullable: false),
                        Fee = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParkedVehicles", "VehicleTypeName", "dbo.VehicleTypes");
            DropForeignKey("dbo.ParkedVehicles", "MemberId", "dbo.Members");
            DropIndex("dbo.ParkedVehicles", new[] { "VehicleTypeName" });
            DropIndex("dbo.ParkedVehicles", new[] { "MemberId" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.ParkedVehicles");
            DropTable("dbo.Members");
        }
    }
}
