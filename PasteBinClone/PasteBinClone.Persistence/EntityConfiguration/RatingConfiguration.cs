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
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.Id);

            builder.HasOne(u => u.ApiUser)
                .WithMany(u => u.Ratings)
                .HasForeignKey(u => u.UserId);

            builder.HasOne(u => u.Paste)
                .WithMany(u => u.Ratings);
        }
    }
}
