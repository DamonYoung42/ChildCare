using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ChildCare.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        [Display(Name = "Pickup Time")]
        public DateTime PickupTime { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }

        public DateTime start { get; set; }
        public DateTime end { get; set; }

        public Boolean allDay { get; set; }

        public string title { get; set; }

        public Boolean editable { get; set; }

    }
}