using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.CartModel
{
    public  class Item
    {
        public Guid ItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public long Price { get; set; }
        public int Amount { get; set; }
    }
}
