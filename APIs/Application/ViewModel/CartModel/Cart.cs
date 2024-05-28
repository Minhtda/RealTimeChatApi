using Domain.Entities;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.CartModel
{
    public  class Cart
    {
        public List<Item> Items { get; set; }  
        public double TotalPrice { get; set; }
        public Cart() 
        {
            Items = new List<Item>();
            TotalPrice = 0;
        }
        public void AddToCart(Item item)
        {
            this.Items.Add(item);
            this.TotalPrice += item.Price * item.Amount;
        }
        public void RemoveItemFromCart(Guid itemId)
        {
            var item = Items.Where(x => x.ItemId == itemId).Single();
            this.Items.Remove(item);
        }
    }
}
