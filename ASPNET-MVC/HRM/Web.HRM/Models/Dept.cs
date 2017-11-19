using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Web.HRM.Models
{
    public class Dept
    {
        public Dept()
        {
            ActionDate = DateTime.Now;
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Dept")]
        public string Name { get; set; }

        public virtual List<Employee> Employees { get; set; }
        public DateTime ActionDate { get; set; }
    }
}