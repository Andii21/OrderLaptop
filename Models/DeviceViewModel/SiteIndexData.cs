using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderLaptop.DeviceModel.Models;

namespace OrderLaptop.Models.DeviceViewModel
{
    public class SiteIndexData
    {
        public IEnumerable<Site> Sites { get; set; }
        public IEnumerable<Laptop> Laptops { get; set; }
        public IEnumerable<Order> Orders { get; set; }

    }
}
