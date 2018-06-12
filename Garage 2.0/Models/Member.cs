using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class Member
    {
        [Display(Name = "Member ID")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string FirstName { get; set; }

        [Display(Name = "Family Name")]
        public string LastName { get; set; }

        [Display(Name = "Street")]
        public string StreetAdress { get; set; }

        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Phone nr")]
        public string PhoneNumber { get; set; }

        [Display(Name="Full Name")]
        public string FullName => FirstName + " " + LastName;

        [Display(Name = "Adress")]
        public string Adress => PostalCode + " " + StreetAdress;

        // Navigational property
        public virtual ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}