using Financial_project.Data.Map;
using Financial_project.Models;
using Microsoft.EntityFrameworkCore;

namespace Financial_project.Data
{
    public class FinancialProjectDBContext : DbContext
    {

        public FinancialProjectDBContext() { }
        public FinancialProjectDBContext(DbContextOptions<FinancialProjectDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerModel> Customers { get; set; }
        public virtual DbSet<AccountModel> Accounts { get; set; }
        public virtual DbSet<TransferModel> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new TransferMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
