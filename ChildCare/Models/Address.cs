using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChildCare.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }
        public string HouseNumber { get; set; }
        public string AptNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
    }


}