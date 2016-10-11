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
        public DateTime Date { get; set; }
        public DateTime PickupTime { get; set; }

        [ForeignKey("Child")]
        public int ChildId { get; set; }
        public virtual Child Child { get; set; }

    }
}