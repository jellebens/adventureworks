using System;
using System.Collections.Generic;
using System.Text;

namespace Adventureworks.Core.Contracts.Orders
{
    public class PurchaseOrdersByVendor
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumOrders { get; set; }
    }
}
