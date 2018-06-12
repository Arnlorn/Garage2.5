using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class VehicleType
    {
        [Display(Name = "Vehicletype")]
        [Key]
        public string Name { get; set; }

        [Display(Name = "Size")]
        public int Size { get; set; }

        [Display(Name = "Parking Fee")]
        public decimal Fee { get; set; }

        //Navigational property
        public virtual ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}