using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ZaloPay.Response
{
    public class CreateZaloRefundResponse
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string Refundid { get; set; }
    }
}
