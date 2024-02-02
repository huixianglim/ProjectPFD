using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFD.Models
{
    public class Update
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Location { get; set; }
        public DateTime DateOfTransaction { get; set; }
        public DateTime LastUpdatedEmail { get; set; }

        // Constructor for transaction
        public Update(Transaction transaction)
        {
            Type = transaction.Type;
            Amount = transaction.Amount;
            Location = transaction.Location;
            DateOfTransaction = transaction.DateOfTransaction;
        }

        // Constructor for email update
        public Update(DateTime lastUpdatedEmail)
        {
            LastUpdatedEmail = lastUpdatedEmail;
        }
    }
}
