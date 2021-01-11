using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderLaptop.Models;
using OrderLaptop.DeviceModel.Models;
using OrderLaptop.DeviceModel.Data;

namespace OrderLaptop.Data
{
    public class DbInitializer
    {
        public static void Initialize(LibraryContext context)
        {
            context.Database.EnsureCreated();

            if (context.Laptops.Any())
            {
                return; // BD a fost creata anterior
            }

            var laptops = new Laptop[]
            {
                new Laptop{Name="Allview Allbook H",
                           Description="procesor Intel Celeron N4000 pana la 2.60 GHz, 15.6,Full HD, 4GB, 256GB SSD, Intel UHD 600, Ubuntu, Grey",
                           Price=Decimal.Parse("1599") },
                new Laptop{Name="ASUS Gaming ROG Strix",
                           Description="G15 G512LV cu procesor Intel® Core™ i7-10870H pana la 5.00 GHz, 15.6 Full HD, 144Hz, 16GB, 512GB SSD, NVIDIA® GeForce RTX™ 2060 6GB, FreeDOS, Original Black",
                           Price=Decimal.Parse("5790") },
                new Laptop{Name="Dell ultraportabil",
                           Description="Latitude 5400 cu procesor Intel Core i5-8365U pana la 4.10 GHz, 14, Full HD, 4GB, 256GB SSD, Intel UHD Graphics, Ubuntu, Grey",
                           Price=Decimal.Parse("2999") },
                new Laptop{Name="ASUS 2 in 1 VivoBook",
                           Description="14 TP412FA cu procesor Intel® Core™ i5-10210U pana la 4.20 GHz, 14, Full HD, 8GB, 256GB SSD, Intel® UHD Graphics, Windows 10 Home, Star Grey",
                           Price=Decimal.Parse("3649") },
                new Laptop{Name="Apple MacBook Air 13",
                           Description="ecran Retina, procesor Intel® Core™ i3 1.1GHz, 8GB, 256GB SSD, Intel Iris Plus Graphics, Space Grey, INT KB",
                           Price=Decimal.Parse("5600") },
                new Laptop{Name="Lenovo IdeaPad 3",
                           Description="15IML05 cu procesor Intel® Pentium® Gold 6405U, 15.6 HD, 4GB, 128GB SSD, Intel® UHD Graphics, Windows 10 Home S, Platinum Grey",
                           Price=Decimal.Parse("2149") },
                new Laptop{Name="ASUS X543MA",
                           Description="cu procesor Intel® Celeron® N4000 pana la 2.60 GHz, 15.6, HD, 4GB, 1TB HDD, Intel® UHD Graphics 600, Endless OS, Star Grey",
                           Price=Decimal.Parse("2459") },
                new Laptop{Name="Acer GAming Predator Helios 300 PH315-53",
                           Description="cu procesor Intel Core i5-10300H pana la 4.50 GHz, 15.6, Full HD, 144Hz, 16GB, 256GB SSD, NVIDIA® GeForce RTX™ 2060 6GB, No OS, Black",
                           Price=Decimal.Parse("6999") },
                new Laptop{Name="HP 15-db1100ny",
                           Description="cu procesor AMD Ryzen 5 3500U pana la 3.70 GHz, 15.6, Full HD, 4GB, 1TB HDD, AMD Radeon Vega 8, Free DOS, Black",
                           Price=Decimal.Parse("2399") },

            };
            foreach (Laptop l in laptops)
            {
                context.Laptops.Add(l);
            }
            context.SaveChanges();


            var customers = new Customer[]
            {

                    new Customer{CustomerID=101,Name="Kanyadi Monika",Adress="Brasov str. Petofi Sandor nr.5",BirthDate=DateTime.Parse("1996-04-28"), PhoneNumber="0752154689"},
                    new Customer{CustomerID=102,Name="Szasz Balazs",Adress="Sighisoara str. Andrei Saguna nr.7",BirthDate=DateTime.Parse("1997-02-07"),PhoneNumber="0745072845"},
                    new Customer{CustomerID=103,Name="Ban Hermina",Adress="Mureni str. Principala nr.22",BirthDate=DateTime.Parse("1992-11-19"),PhoneNumber="0753232119"},
                    new Customer{CustomerID=104,Name="Topolyai Gergely",Adress="Surcea nr.225",BirthDate=DateTime.Parse("1989-10-07"),PhoneNumber="0752129689"},
                    new Customer{CustomerID=105,Name="Kallo Helen",Adress="Cristuru Secuiesc str. Harghitei P3/9",BirthDate=DateTime.Parse("1996-10-05"),PhoneNumber="0748160450"},
                    new Customer{CustomerID=106,Name="Kiss Alpar",Adress="Orodheiu Secuiesc str. Kossuth Lajos",BirthDate=DateTime.Parse("1997-12-06"),PhoneNumber="0752964689"},

 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            var orders = new Order[]
            {
                     new Order{LaptopID=1,CustomerID=101, OrderDate=DateTime.Parse("2020-03-25")},
                     new Order{LaptopID=3,CustomerID=102,OrderDate=DateTime.Parse("2020-05-27")},
                     new Order{LaptopID=1,CustomerID=103,OrderDate=DateTime.Parse("2020-04-18")},
                     new Order{LaptopID=2,CustomerID=102,OrderDate=DateTime.Parse("2020-06-20")},
                     new Order{LaptopID=4,CustomerID=104,OrderDate=DateTime.Parse("2020-07-01")},
                     new Order{LaptopID=5,CustomerID=106,OrderDate=DateTime.Parse("2020-03-21")},
                     new Order{LaptopID=6,CustomerID=105,OrderDate=DateTime.Parse("2020-05-12")},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();

            var sites = new Site[]
                {
                    new Site{SiteName="Emag",URL="https://www.emag.ro/" },
                    new Site{SiteName="Altex",URL="https://altex.ro/" },
                    new Site{SiteName="Domo",URL="https://www.domo.ro/index.php?main_page=shopping_cart" },
                    new Site{SiteName="Flanco",URL="https://www.flanco.ro/" },
                    new Site{SiteName="Ideall",URL="https://www.ideall.ro/" },

                };
            foreach (Site s in sites)
            {
                context.Sites.Add(s);
            }
            context.SaveChanges();

            var laptopsites = new LaptopSite[]
            {
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="ASUS Gaming ROG Strix").LaptopID, 
                                SiteID = sites.Single(s => s.SiteName =="Emag").SiteID},
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="ASUS Gaming ROG Strix").LaptopID,
                                SiteID = sites.Single(s => s.SiteName =="Altex").SiteID},
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="ASUS Gaming ROG Strix").LaptopID,
                                SiteID = sites.Single(s => s.SiteName =="Ideall").SiteID},
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="Apple MacBook Air 13").LaptopID,
                                SiteID = sites.Single(s => s.SiteName =="Emag").SiteID},
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="HP 15-db1100ny").LaptopID,
                                SiteID = sites.Single(s => s.SiteName =="Flanco").SiteID},
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="Acer GAming Predator Helios 300 PH315-53").LaptopID,
                                SiteID = sites.Single(s => s.SiteName =="Altex").SiteID},
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="Lenovo IdeaPad 3").LaptopID,
                                SiteID = sites.Single(s => s.SiteName =="Ideall").SiteID},
                new LaptopSite{ LaptopID = laptops.Single(l => l.Name =="ASUS X543MA").LaptopID,
                                SiteID = sites.Single(s => s.SiteName =="Emag").SiteID},

            };


            foreach (LaptopSite ls in laptopsites)
            {
                context.LaptopSites.Add(ls);
            }
            context.SaveChanges();

        }
    }
}
