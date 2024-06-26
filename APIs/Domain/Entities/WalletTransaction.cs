﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WalletTransaction:BaseEntity
    {
        public string TransactionType { get; set; }
        public Guid WalletId { get; set; }  
        public Wallet Wallet { get; set; }  
        public ICollection<Subcription> Subcriptions { get; set;}
    }
}
