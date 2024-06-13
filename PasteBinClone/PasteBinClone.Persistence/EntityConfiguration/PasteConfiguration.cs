using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PasteBinClone.Application.Interfaces;
using PasteBinClone.Domain.Models;
using PasteBinClone.Persistence.EntityConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasteBinClone.Persistence.EntityConfiguration
{
    public class PasteConfiguration : IEntityTypeConfiguration<Paste>
    {
        public void Configure(EntityTypeBuilder<Paste> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(x => x.Id).IsUnique();
            builder.Property(u => u.Title).IsRequired().HasMaxLength(60);
            builder.Property(u => u.BodyUrl).IsRequired();
            builder.Property(u => u.CreateAt).IsRequired();
            builder.Property(u => u.ExpireAt).IsRequired();

            builder
                .HasOne(u => u.Category);
            builder
                .HasOne(u => u.Language);
            builder
                .HasOne(u => u.Type);
            builder
                .HasOne(u => u.User);
        }
    }
}