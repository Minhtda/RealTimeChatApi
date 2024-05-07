using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post:BaseEntity
    {
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
