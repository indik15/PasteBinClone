using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasteBinClone.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Persistence.EntityConfiguration
{
    public class ApiUserConfiguration : IEntityTypeConfiguration<ApiUser>
    {
        public void Configure(EntityTypeBuilder<ApiUser> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.HasIndex(x => x.UserId).IsUnique();
            builder.Property(u => u.UserId).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Name).IsRequired();
        }
    }
}
