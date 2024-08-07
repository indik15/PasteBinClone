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
    public class UserPasteInfoConfigurations : IEntityTypeConfiguration<UserPasteInfo>
    {
        public void Configure(EntityTypeBuilder<UserPasteInfo> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Id).IsUnique();
            builder.HasIndex(u => u.UserId).IsUnique();
            builder.Ignore(u => u.TotalActivePastes);
        }
    }
}
