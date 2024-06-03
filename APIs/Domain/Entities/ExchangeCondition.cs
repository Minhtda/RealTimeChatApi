using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExchangeCondition
    {
        public int ConditionId { get; set; }
        public string ConditionType { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
