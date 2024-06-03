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
        public Guid? ReportUserId { get; set; }
        public User? ReportUser { get; set; }
        public Guid? ReportPostId { get; set; }
        public Post? ReportPost { get; set; }
    }
}
