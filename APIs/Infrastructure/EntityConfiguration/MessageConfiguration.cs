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
    internal class MessageConfiguration:IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(x => x.Sender).WithMany(u => u.SenderMessages).HasForeignKey(x => x.SenderId).OnDelete(deleteBehavior:DeleteBehavior.NoAction);
            builder.HasOne(x => x.Receiver).WithMany(u => u.ReceiverMessages).HasForeignKey(x => x.ReceiverId).OnDelete(deleteBehavior: DeleteBehavior.NoAction);
        }
    }
}
