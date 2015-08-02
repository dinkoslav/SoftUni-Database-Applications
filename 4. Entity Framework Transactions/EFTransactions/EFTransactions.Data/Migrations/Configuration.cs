namespace EFTransactions.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EFTransactions.Data.EFTransactionsContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "EFTransactions.Data.EFTransactionsContext";
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(EFTransactions.Data.EFTransactionsContext context)
        {
            
            context.News.AddOrUpdate(
                new Model.News()
                {
                    Content = "EF 7 Beta To Be Released in May 2016."
                });

            context.SaveChanges();
        }
    }
}
