using Domain.Entities;
using Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfiguration
{
    internal class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasData(new ProductType
            {
                ProductTypeId = 1,
                ProductTypeName = nameof(ProductTypeName.Trade),
            },
            new ProductType
            {
                ProductTypeId=2,
                ProductTypeName= nameof(ProductTypeName.Sell),
            }
            );
        }
    }
}
