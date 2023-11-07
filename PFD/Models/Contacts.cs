using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFD.Models
{
    public class Contacts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ContactID")]
        public int ContactID { get; set; }

        [Display(Name = "Contact Number")]
        public string Number { get; set; }

        [Display(Name = "Contact Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "User ID")]

        public int UserID { get; set; }
    }
}
