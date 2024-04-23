using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasteBinClone.Domain.Models;

namespace PasteBinClone.Persistence.EntityConfiguration
{
    public class ContentTypeConfiguration : IEntityTypeConfiguration<ContentType>
    {
        public void Configure(EntityTypeBuilder<ContentType> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(x => x.TypeName).HasMaxLength(50).IsRequired();
        }
    }
}
