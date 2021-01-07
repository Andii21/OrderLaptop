using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLaptop.Models
{
    public class Laptop
    {
            public int LaptopID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public decimal Price { get; set; }

            public ICollection<Order> Orders { get; set; }

            public ICollection<LaptopSite> LaptopSites { get; set; }

    }
}

