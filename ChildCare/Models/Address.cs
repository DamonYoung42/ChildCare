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
        public int Id { get; set; }

        [Required]
        [Display(Name = "House Number")]
        public string HouseNumber { get; set; }
     
        [Display(Name = "Apt. Number")]
        public string AptNumber { get; set; }

        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public int ZipCode { get; set; }
    }


}