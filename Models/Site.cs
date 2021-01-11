using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OrderLaptop.Models
{
    public class Site
    {
        public int SiteID { get; set; }
        [Required]
        [Display(Name ="Site Name")]
        [StringLength(50)]
        public string SiteName { get; set; }

        [StringLength(100)]
        public string URL { get; set; }

        public ICollection<LaptopSite> LaptopSites { get; set; }



    }
}
