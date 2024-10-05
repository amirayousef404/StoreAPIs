using Store.G04.Core.Entities;
using Store.G04.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync( StoreDbContext _context)
        {
            if (_context.Brands.Count() == 0)
            {

                // brand

                //1. read data from json file

                var brandsData = File.ReadAllText(@"..\Store.G04.Repository\Data\DataSeed\brands.json");

                // 2. convert json string to list<T>

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                // 3. seed data to db
                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }

            }

            if (_context.Types.Count() == 0)
            {


                //1. read data from json file

                var typesData = File.ReadAllText(@"..\Store.G04.Repository\Data\DataSeed\types.json");

                // 2. convert json string to list<T>

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                // 3. seed data to db
                if (types is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                }

            }

            if (_context.Products.Count() == 0)
            {

                

                //1. read data from json file

                var productsData = File.ReadAllText(@"..\Store.G04.Repository\Data\DataSeed\products.json");

                // 2. convert json string to list<T>

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                // 3. seed data to db
                if (products is not null && products.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }

            }

        }
    }
}
