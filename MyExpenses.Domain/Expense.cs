﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyExpenses.Domain
{
    public class Expense
    {
        [Required]
        [Key]
        [Column(Order = 1)]
        public int ExpenseId { get; set; }
        [Required]
        [Key]
        [Column(Order = 2)]
        public Guid OwnerId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
