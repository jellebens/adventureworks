using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Data
{
    public class Vendor
    {
        public int Id { get; internal set; }
        public string Name { get; internal set; }
        public IEnumerable<PurchaseOrder> Orders  { get; internal set; }
    }
}
