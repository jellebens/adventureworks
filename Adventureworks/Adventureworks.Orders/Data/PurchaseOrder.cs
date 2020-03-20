using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data
{

    [Table("PurchaseOrderHeader", Schema = "Purchasing")]
    public class PurchaseOrder
    {
        public int Id { get; set; }

        public Vendor Vendor{ get; set; }
    }
}
