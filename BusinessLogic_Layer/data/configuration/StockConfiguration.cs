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
    public class StockConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
             builder.HasKey(x => x.StockId);
            builder.Property(x => x.Symbol).HasColumnType("nvarchar(255)").IsRequired();
            builder.Property(x => x.CompanyName).HasColumnType("nvarchar(255)");
            builder.Property(x => x.purchase).HasPrecision(8,2);
            builder.Property(x => x.LastDiv).HasPrecision(8,2);
            builder.Property(x => x.Industry).HasColumnType("nvarchar(255)");
            builder.Property(x => x.marketCap).HasColumnType("Bigint");

            /// add relationship one to many between stock and comment
            builder.HasMany(x => x.Comments).WithOne(x => x.Stock).HasForeignKey(x => x.StockId).IsRequired();
            builder.ToTable("Stocks");


        }
    }
    
}
