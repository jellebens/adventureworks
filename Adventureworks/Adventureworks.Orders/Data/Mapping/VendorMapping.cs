using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Mapping
{
    public class VendorMapping : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.ToTable("Vendor", "Purchasing");
            builder.Property(v => v.Id).HasColumnName("BusinessEntityID");
            builder.Property(v => v.Name).HasColumnName("Name");
            builder.HasMany(v => v.Orders)
                .WithOne(po => po.Vendor);
        }
    }
}
