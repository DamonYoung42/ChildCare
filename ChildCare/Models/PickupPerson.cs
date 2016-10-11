using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChildCare.Models
{
    public class PickupPerson
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] Photo { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }

        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }


    }
}