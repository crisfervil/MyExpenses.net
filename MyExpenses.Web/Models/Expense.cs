using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyExpenses.Web.Models
{
    public class Expense
    {
        [Required]
        public int ExpenseId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}