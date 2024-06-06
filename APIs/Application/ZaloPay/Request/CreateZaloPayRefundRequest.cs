using Application.Util;
using Application.ZaloPay.Response;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ZaloPay.Request
{
    public class CreateZaloPayRefundRequest
    {
        public string Mrefundid { get; set; } = string.Empty;
        public int AppId { get; set; }
        public long ZpTransId { get; set; }
        public long Amount { get; set; }
        public long TimeStamp { get; set; }
        public string Mac {  get; set; }
        public string Description { get; set; }=string.Empty;
        public CreateZaloPayRefundRequest(string mrefundid,int appId,long zpTransid,long amount,long timeStamp,string description) 
        { 
            Mrefundid = mrefundid;
            AppId = appId;
            ZpTransId = zpTransid;
            Amount = amount;
            TimeStamp = timeStamp;
            Amount = amount;
            Description = description;
        }
        public void MakeSignature(string key)
        {
            var data= AppId + "|"+ ZpTransId + "|" + Amount + "|" + Description + "|" + TimeStamp ;
            this.Mac= HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key, data);
        }
        public Dictionary<string, string> GetContent()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("appid", AppId.ToString());
            keyValuePairs.Add("mrefundid", Mrefundid);
            keyValuePairs.Add("zptransid", ZpTransId.ToString());
            keyValuePairs.Add("amount", Amount.ToString());
            keyValuePairs.Add("timestamp", TimeStamp.ToString());
            keyValuePairs.Add("description", Description);
           // var data = keyValuePairs["appid"] + "|" + keyValuePairs["zptransid"] + "|" + keyValuePairs["amount"] + "|" + keyValuePairs["description"] + "|" + keyValuePairs["timestamp"];
            keyValuePairs.Add("mac", Mac);
            return keyValuePairs;
        }
        public (bool,string) GetRefundLink(string refundUrl)
        {
            using var client= new HttpClient();
            var content = new FormUrlEncodedContent(GetContent());
            var response = client.PostAsync(refundUrl, content).Result;
            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var responseData = JsonConvert
                    .DeserializeObject<CreateZaloRefundResponse>(responseContent);
                if (responseData.ReturnCode > 1)
                {
                    return (true, responseData.Refundid);
                }
                else
                {
                    return (false, responseData.Refundid);
                }

            }
            else
            {
                return (false, response.ReasonPhrase ?? string.Empty);
            }
        }
    }
}
