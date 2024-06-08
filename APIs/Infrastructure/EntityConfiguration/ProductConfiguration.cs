using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfiguration
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
          //  builder.HasOne(x => x.Post).WithOne(x => x.Product).HasForeignKey<Product>(x=>x.PostId);
            builder.HasOne(x => x.ConditionType).WithMany(x => x.Products).HasForeignKey(x => x.ConditionId);
        }
    }
}
