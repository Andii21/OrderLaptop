using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderLaptop.Models
{
    public class Laptop
    {
            public int LaptopID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            [Column(TypeName ="decimal(6,2)")]
            public decimal Price { get; set; }

            public ICollection<Order> Orders { get; set; }

            public ICollection<LaptopSite> LaptopSites { get; set; }

    }
}

