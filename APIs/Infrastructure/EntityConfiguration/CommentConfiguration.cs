using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.EntityConfiguration
{
    internal class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasOne(x => x.ParentComment).WithMany(x => x.ReplyComments).HasForeignKey(x => x.ReplyCommentId).OnDelete(deleteBehavior: DeleteBehavior.NoAction);
            builder.HasOne(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);
        }
    }
}
