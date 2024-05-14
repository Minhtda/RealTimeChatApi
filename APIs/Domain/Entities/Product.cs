using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public long ProductPrice { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
