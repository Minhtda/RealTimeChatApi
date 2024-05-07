using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Wallet:BaseEntity
    {
        public float  UserBalance { get; set; }
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }
        public ICollection<WalletTransaction> Transactions { get; set; }
    }
}
