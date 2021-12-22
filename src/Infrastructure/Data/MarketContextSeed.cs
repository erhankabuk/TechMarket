using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class MarketContextSeed
    {
        public static async Task SeedAsync(MarketContext db)
        {
            if (await db.Categories.AnyAsync() || await db.Brands.AnyAsync() || await db.Products.AnyAsync()) return;
            Category phone = new Category() { Name = "Phone" };
            Category monitor = new Category() { Name = "Monitor" };
            Category tablet = new Category() { Name = "Tablet" };
            db.AddRange(phone, monitor, tablet);
            await db.SaveChangesAsync();

            var lg = new Brand() { Name = "LG" };
            var samsung = new Brand() { Name = "Samsung" };
            var apple = new Brand() { Name = "Apple" };
            var huawei = new Brand() { Name = "Huawei" };
            db.AddRange(lg, samsung, apple, huawei);
            await db.SaveChangesAsync();

            Product[] products = {
                new Product() { Name = "LG UltraGear 27GN880-B 27'' 144Hz 1ms (HDMI+Display) QHD Nano IPS Monitor", Price = 429.99m, PictureUri = "1.jpg", Brand = lg, Category = monitor },
                new Product() { Name = "LG 22MK400H 21.5\" 1ms 75Hz (HDMI+Analog) FreeSync Full HD Gaming Monitor", Price = 159.99m, PictureUri = "2.jpg", Brand = lg, Category = monitor },
                new Product() { Name = "LG K61 128 GB", Price = 279.99m, PictureUri = "3.jpg", Brand = lg, Category = phone },
                new Product() { Name = "Samsung Galaxy M31s 128 GB", Price = 299.50m, PictureUri = "4.jpg", Brand = samsung, Category = phone },
                new Product() { Name = "Samsung LC24RG50FQRXUF 24\" 144Hz 4ms (Display+HDMI) FreeSync Full HD Curved LED Monitor", Price = 169.99m, PictureUri = "5.jpg", Brand = samsung, Category = monitor },
                new Product() { Name = "Samsung Galaxy Tab S7 FE LTE 64 GB", Price = 414.50m, PictureUri = "6.jpg", Brand = samsung, Category = tablet },
                new Product() { Name = "Samsung Galaxy S20 FE 128 GB Snapdragon", Price = 499.99m, PictureUri = "7.jpg", Brand = samsung, Category = phone },
                new Product() { Name = "iPhone 13 Pro Max 1 Tb", Price = 1599.99m, PictureUri = "8.jpg", Brand = apple, Category = phone },
                new Product() { Name = "iPhone 11 128 GB", Price = 450.00m, PictureUri = "9.jpg", Brand = apple, Category = phone },
                new Product() { Name = "Apple iPad 9 64GB 10.2\" WiFi Tablet - MK2L3TU/A", Price = 450.00m, PictureUri = "10.jpg", Brand = apple, Category = tablet },
                new Product() { Name = "Huawei P40 Lite 128 GB", Price = 250.00m, PictureUri = "11.jpg", Brand = huawei, Category = phone },
                new Product() { Name = "Huawei Matepad 11 128 GB", Price = 279.50m, PictureUri = "12.jpg", Brand = huawei, Category = tablet }
            };
            db.AddRange(products);
            await db.SaveChangesAsync();
        }
    }
}
