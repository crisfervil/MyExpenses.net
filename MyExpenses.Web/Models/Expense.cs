using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace MyExpenses.Web.Models
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

    public class MyExpensesDbContext: DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public int GetNextSequenceValue(string sequenceName)
        {
            var rawQuery = Database.SqlQuery<long>($"SELECT NEXT VALUE FOR {sequenceName};");
            var task = rawQuery.SingleAsync();
            long nextVal = task.Result;
            return (int)nextVal;
        }
    }
}