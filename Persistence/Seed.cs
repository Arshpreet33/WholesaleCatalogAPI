using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Title = "BACI TUBES Milk Chocolate",
                        Code = "839456005525",
                        UnitPrice = 1.95,
                        UnitWeight = 37.5,
                        CaseQty = 14,
                        Image = ""
                    },
                    new Product
                    {
                        Title = "BACI TUBES Dark Chocolate",
                        Code = "839456005501",
                        UnitPrice = 1.95,
                        UnitWeight = 37.5,
                        CaseQty = 14,
                        Image = ""
                    },
                    new Product
                    {
                        Title = "BACI BULK",
                        Code = "000",
                        UnitPrice = 25,
                        UnitWeight = 750,
                        CaseQty = 4,
                        Image = ""
                    },
                    new Product
                    {
                        Title = "PERUGINA BACI ADVENT CALENDAR",
                        Code = "000",
                        UnitPrice = 15.65,
                        UnitWeight = 278,
                        CaseQty = 10,
                        Image = ""
                    },
                    new Product
                    {
                        Title = "PERUGINA DOLCE SCOPERTE",
                        Code = "000",
                        UnitPrice = 11.00,
                        UnitWeight = 200,
                        CaseQty = 8,
                        Image = ""
                    },
                    new Product
                    {
                        Title = "BALOCCO NOVELLINI",
                        Code = "8001100012384",
                        UnitPrice = 2.50,
                        UnitWeight = 350,
                        CaseQty = 10,
                        Image = ""
                    },
                };

                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
    }
}