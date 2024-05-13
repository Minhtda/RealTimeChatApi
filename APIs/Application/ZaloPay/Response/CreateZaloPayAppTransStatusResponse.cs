using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ZaloPay.Response
{
    public class CreateZaloPayAppTransStatusResponse
    {
        public int ReturnCode {  get; set; }
        public string ReturnMessage { get; set; }
        public bool IsProcessing { get; set; }
        public long Amount { get; set; }
        public long DiscountAmount { get; set; }
        public long ZpTransId { get; set; }
    }
}
