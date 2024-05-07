using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Report:BaseEntity
    {
        public string ReportContent { get; set; }
        public int ReportTypeId { get; set; }
        public ReportType ReportType { get; set; }
    }
}
