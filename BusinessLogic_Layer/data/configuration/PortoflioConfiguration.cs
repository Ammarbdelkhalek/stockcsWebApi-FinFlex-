using Domain_or_abstractionLayer.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic_Layer.data.configuration
{
    public class PortoflioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.HasKey(x => new { x.AppUserId, x.SockId });
            builder.HasOne(x=>x.Stock).WithMany(x=>x.portfolios).HasForeignKey(x=>x.SockId);
            builder.HasOne(x=>x.AppUser).WithMany(x=>x.portfolios).HasForeignKey(x=>x.AppUserId);
            builder.ToTable("Portfolios");
        }
    }
}
