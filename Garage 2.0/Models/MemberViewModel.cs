using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage_2._0.Models
{
    public class MemberViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAdress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        //public string FullName => FirstName + " " + LastName;
        //public string Adress => PostalCode + " " + StreetAdress;
        // Navigational property
        public virtual ICollection<ParkedVehicle> ParkedVehicles { get; set; }
    }
}