using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment:BaseEntity
    {
        public string CommentContent { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ReplyCommentId { get; set; }
        public Comment ParentComment { get; set; }
        public virtual ICollection<Comment> ReplyComments { get; set; }
    }
}
