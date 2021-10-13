using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domains
{
    public class RegConfirmation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Id")]
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Device")]
        public char Device { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
    }
}
