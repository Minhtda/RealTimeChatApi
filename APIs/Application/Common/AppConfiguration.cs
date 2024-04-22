using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public class AppConfiguration
    {
        public string DatabaseConnectionString {  get; set; }
        public string CacheConnectionString { get; set; }
        public string JWTSecretKey { get; set; }
    }
}
