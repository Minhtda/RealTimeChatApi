using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfiguration
{
    internal class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
           builder.HasOne(x => x.Rater).WithMany(x => x.Raters).HasForeignKey(x => x.RaterId).OnDelete(deleteBehavior: DeleteBehavior.NoAction);
           builder.HasOne(x => x.RatedUser).WithMany(x => x.RatedUsers).HasForeignKey(x => x.RatedUserId).OnDelete(deleteBehavior: DeleteBehavior.NoAction);
        }
    }
}
