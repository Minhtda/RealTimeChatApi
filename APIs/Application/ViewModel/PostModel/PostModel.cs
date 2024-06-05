using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel.PostModel
{
    public class PostModel
    {
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public Guid ProductId { get; set; }
        public ProductModel Product { get; set; }
        public List<CommentModel> Comments { get; set; }
    }

    public class ProductModel
    {
        public string ProductName { get; set; }
        public string ProductImageUrl { get; set; }
        public long ProductPrice { get; set; }
        public int? ConditionId { get; set; }
        public string ConditionName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class CommentModel
    {
        public Guid CommentId { get; set; }
        public string CommentContent { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid? ReplyCommentId { get; set; }
        public CommentModel ParentComment { get; set; }
        public List<CommentModel> ReplyComments { get; set; }
    }
}
