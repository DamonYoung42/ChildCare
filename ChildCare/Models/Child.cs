using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChildCare.Models
{
    public class Child
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Grade Level")]
        public string GradeLevel { get; set; }

        [Required]
        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }

        [ForeignKey("ApplicationUser")]
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }

        [Display(Name = "Teacher")]
        public Teacher Teacher { get; set; }

        [Required]
        [Display(Name = "Medications")]
        public string Medications { get; set; }

        [Required]
        [Display(Name = "Notes")]
        public string Notes { get; set; }
    }
}