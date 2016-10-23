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

        [Display(Name = "Billed Amount")]
        public double BilledAmount { get; set; }

        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }

        [Display(Name = "Due Date")]
        public DateTime DateDue { get; set; }

        [Display(Name = "Amount Paid")]
        public double AmountPaid { get; set; }

        [Display(Name = "Date Paid")]
        public DateTime DatePaid { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Adjustments")]
        public double Adjustments { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}