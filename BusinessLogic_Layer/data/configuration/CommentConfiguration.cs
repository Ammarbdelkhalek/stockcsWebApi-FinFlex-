using Domain_or_abstractionLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Layer.data.configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.CommentId);
            builder.Property(x => x.Title).HasColumnType("nvarchar(255)");
            builder.Property(x => x.Content).HasColumnType("nvarchar(255)");
            builder.Property(x => x.CreatedOn).HasColumnType("date");
            builder.ToTable("Comments");
             


        }
         

    }
}
