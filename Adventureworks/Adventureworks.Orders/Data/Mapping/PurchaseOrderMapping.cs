using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data.Mapping
{
    public class PurchaseOrderMapping : IEntityTypeConfiguration<PurchaseOrder>
    {
        
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrderHeader", "Purchasing");
            builder.Property(po => po.Id).HasColumnName("PurchaseOrderID");
            builder.HasOne(po => po.Vendor).WithMany(v => v.Orders);
        }
    }
}
