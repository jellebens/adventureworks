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
            builder.Property(po => po.Id).HasField("PurchaseOrderID");
            builder.HasOne(po => po.Vendor).WithMany(v => v.Orders);
        }
    }
}
