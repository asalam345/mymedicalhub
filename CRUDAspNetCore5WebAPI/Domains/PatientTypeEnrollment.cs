using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains
{
    public class PatientTypeEnrollment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Patient Type Id")]
        public int PatientTypeId { get; set; }
    }
}
