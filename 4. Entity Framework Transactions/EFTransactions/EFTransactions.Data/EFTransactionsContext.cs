namespace EFTransactions.Data
{
    using EFTransactions.Data.Migrations;
    using EFTransactions.Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EFTransactionsContext : DbContext
    {
        public EFTransactionsContext()
            : base("name=EFTransactionsContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFTransactionsContext, Configuration>());
        }

        public IDbSet<News> News { get; set; }
    }
}