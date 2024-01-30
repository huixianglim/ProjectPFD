using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PFD.Models
{
    public class Users
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "UserID")]
        public int UserID { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Monetary Amount")]
        public Decimal Money { get; set; }

        public string Email { get; set; }

        public DateTime LastLoggedIn { get; set; }

    }
}
