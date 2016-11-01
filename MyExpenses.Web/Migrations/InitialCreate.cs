namespace MyExpenses.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Expenses",
                c => new
                    {
                        ExpenseId = c.Int(nullable: false),
                        OwnerId = c.Guid(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.ExpenseId, t.OwnerId });

            this.Sql(@"CREATE SEQUENCE dbo.ExpensesIds  
                        START WITH 1
                        INCREMENT BY 1 ;
                        GO");
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Expenses");
        }
    }
}
