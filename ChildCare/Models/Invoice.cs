using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChildCare.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        public string Month { get; set; }
        public int Year { get; set; }
        public double AmountDue { get; set; }
        public DateTime DateDue { get; set; }
        public double AmountPaid { get; set; }
        public DateTime DatePaid { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}