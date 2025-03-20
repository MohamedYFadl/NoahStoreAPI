using NoahStore.Core.Entities;
using NoahStore.Infrastructure.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NoahStore.Infrastructure.Data.Config
{
    public static class ApplicationDbContextSeed
    {
        public async static Task SeedAsync(ApplicationDbContext context)
        {
            if(!context.Categories.Any())
            {
                var categoriesJsonData = File.ReadAllText("../NoahStore.Infrastructure/Data/SeedData/Categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesJsonData);
                if(categories?.Count() > 0)
                {
                    foreach(var category in categories)
                    {
                        context.Categories.Add(category);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (!context.Products.Any())
            {
                var productsJsonData = File.ReadAllText("../NoahStore.Infrastructure/Data/SeedData/Products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsJsonData);
                if (products?.Count() > 0)
                {
                    foreach (var product in products)
                    {
                        context.Products.Add(product);
                    }
                    await context.SaveChangesAsync();
                }
            }
            if (!context.ProductImages.Any())
            {
                var ImagesJsonData = File.ReadAllText("../NoahStore.Infrastructure/Data/SeedData/ProductImages.json");
                var Images = JsonSerializer.Deserialize<List<ProductImage>>(ImagesJsonData);
                if (Images?.Count() > 0)
                {
                    foreach (var image in Images)
                    {
                        context.ProductImages.Add(image);
                    }
                    await context.SaveChangesAsync();
                }
            }
            //if (!context.ProductReviews.Any())
            //{
            //    var ReviewJsonData = File.ReadAllText("../NoahStore.Infrastructure/Data/SeedData/ProductReview.json");
            //    var ProductReviews = JsonSerializer.Deserialize<List<ProductReview>>(ReviewJsonData);
            //    if (ProductReviews?.Count() > 0)
            //    {
            //        foreach (var review in ProductReviews)
            //        {
            //            context.ProductReviews.Add(review);
            //        }
            //        await context.SaveChangesAsync();
            //    }
            //}


        }
    }
}
