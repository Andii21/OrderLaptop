using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLaptop.Models.DeviceViewModel
{
    public class SiteIndexData
    {
        public IEnumerable<Site> Sites { get; set; }
        public IEnumerable<Laptop> Laptops { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
