using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderLaptop.Models
{
    public class Site
    {
        public int SiteID { get; set;}
        public string SiteName { get; set; }
        public string URL { get; set; }

        public ICollection<LaptopSite> LaptopSites { get; set; }



    }
}
