using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CRUD_DAL.Models
{
    public class Prescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Schedule Id")]
        public int ScheduleId { get; set; }

        [Required]
        [Display(Name = "Schedule Id")]
        public string Description { get; set; }

        [Display(Name = "Report Id")]
        public int ReportId { get; set; } = 0;
        public string Comment { get; set; }
    }
}
