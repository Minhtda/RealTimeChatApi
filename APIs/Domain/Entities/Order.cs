using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order:BaseEntity
    {
        public float TotalPrice { get; set; }
        public int Status { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public ICollection<Product> Products { get; set; }  
        public ICollection<WalletTransaction> WalletTransactions { get; set; }
    }
}
