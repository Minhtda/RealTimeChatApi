using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.PostModel
{
    public class PostModel
    {
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
