using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Adventureworks.Web.Models
{
    public class VendorListItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NumOrders { get; set; }
    }
}
