using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Garage_2._0.Models
{
    public class ParkedVehicle
    {
        public int Id { get; set; }

        [Display(Name = "Reg Nr")]
        // [Index("IX_RegNum", IsUnique = true)]
        [Required]
        public string RegNr { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }

        [Display(Name = "Make")]
        public string Make { get; set; }

        [Display(Name = "Model")]
        public string Model { get; set; }

        [Range(0,50, ErrorMessage = "Please input the number of wheels (between zero and fifty)")]
        [Display(Name="Wheels")]
        public int NrOfWheels { get; set; }

        [Display(Name = "Time of parking")]
        public DateTime TimeStamp { get; set; }

        [Display(Name = "Parking Slot")]
        public int ParkingSlot { get; set; }

        // Navigational Property
        [ForeignKey("Member")]
        [Display(Name = "Owner")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }

        // Navigational Property
        [Display(Name = "Type")]
        [ForeignKey("VehicleType")]
        public string VehicleTypeName { get; set; }

        [Display(Name = "Type")]
        public virtual VehicleType VehicleType { get; set; }
    }
}