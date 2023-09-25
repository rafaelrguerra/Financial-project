using Financial_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financial_project.Data.Map
{
    public class AccountMap : IEntityTypeConfiguration<AccountModel>
    {
        public void Configure(EntityTypeBuilder<AccountModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AccountNumber).IsRequired().HasMaxLength(15);
            builder.Property(x => x.Balance).IsRequired();

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasMany(x => x.ReceivedTransfers)
                .WithOne(x => x.AccountFrom)
                .HasForeignKey(x => x.AccountIdFrom)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.SentTransfers)
                .WithOne(x => x.AccountTo)
                .HasForeignKey(x => x.AccountIdTo)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}