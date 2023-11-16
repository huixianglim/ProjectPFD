using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;


public class Transaction
{
	

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "TransactionID")]
        public int TransactionID { get; set; }


        [Display(Name = "Type")]
        public string Type { get;set; }


        [Display(Name = "Amount")]
        public decimal Amount { get;set; }


        [Display(Name = "UserID")]
        public int UserID { get; set; }


        [Display(Name = "DateOfTransaction")]

        public DateTime DateOfTransaction { get; set; }


        [Display(Name = "Location")]

        public string Location { get; set; }

}
