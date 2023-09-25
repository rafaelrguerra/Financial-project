using Financial_project.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Financial_project.Data.Map
{
    public class TransferMap : IEntityTypeConfiguration<TransferModel>
    {
        public void Configure(EntityTypeBuilder<TransferModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
        }
    }
}
