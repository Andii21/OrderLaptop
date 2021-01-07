using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLaptop.Models
{
    public class LaptopSite
    {
        public int LaptopID { get; set; }
        public int SiteID { get; set; }
        public Laptop laptop { get; set; }

        public Site site { get; set; }


    }
}
