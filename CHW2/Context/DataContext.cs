using System.Data.Entity;
using CHW2.Models;

namespace CHW2.Context
{
    public class DataContext: DbContext
    {
        public DataContext(): base("test-db") {}

        public DbSet<Cash> Cashes { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Money> Monies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Statistic> Statistics { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
    }
}
