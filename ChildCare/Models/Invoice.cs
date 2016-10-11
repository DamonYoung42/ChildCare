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

        [Required]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Balance Due")]
        public double AmountDue { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DateDue { get; set; }

        [Display(Name = "Amount Paid")]
        public double AmountPaid { get; set; }

        [Display(Name = "Date Paid")]
        public DateTime DatePaid { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}