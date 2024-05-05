using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager)
        {
            if(!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Bob",
                        UserName = "bob",
                        Email = "bob@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com"
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com"
                    }
                };
                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }
            }

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

            if (!context.Clients.Any())
            {
                var clients = new List<Client>
                {
                    new Client
                    {
                        Code = "11",
                        Name = "Client ABC",
                        Email = "clientabc@gmail.com",
                        PhoneNumber = "123456789",
                        Address = "1234 100 Ave SW",
                        City = "Montreal",
                        Province = "QC",
                        PostalCode = "H4G5KT",
                        IsActive = true,
                        IsDeleted = false
                    },
                    new Client
                    {
                        Code = "22",
                        Name = "Client XYZ",
                        Email = "clientxyz@gmail.com",
                        PhoneNumber = "123456789",
                        Address = "526 120 Ave SW",
                        City = "London",
                        Province = "ON",
                        PostalCode = "K4G5SZ",
                        IsActive = true,
                        IsDeleted = false
                    },
                    new Client
                    {
                        Code = "33",
                        Name = "Client 123",
                        Email = "client123@gmail.com",
                        PhoneNumber = "123456789",
                        Address = "778 98 Ave SW",
                        City = "Vancouver",
                        Province = "BC",
                        PostalCode = "T4A9UD",
                        IsActive = true,
                        IsDeleted = false
                    }
                };

                await context.Clients.AddRangeAsync(clients);
                await context.SaveChangesAsync();
            }

            if(!context.Manufacturers.Any())
            {
                var manufacturers = new List<Manufacturer>
                {
                    new Manufacturer
                    {
                        Name = "Perugina",
                        Description = "Perugina is an Italian chocolate confectionery company based in Perugia, Italy that was founded in 1907. The company produces a wide array of chocolate products, including chocolate bars, hard candy, and chocolate-dragees.",
                        ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                        IsActive = true,
                        IsDeleted = false,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Chocolate",
                                Description = "Chocolate is a food product made from roasted and ground cacao seeds that is typically sweetened. It is commonly used as a flavoring ingredient in many foods, such as cakes, cookies, and ice cream.",
                                ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                IsActive = true,
                                IsDeleted = false
                            },
                            new Category
                            {
                                Name = "Candy",
                                Description = "Candy is a confection made from sugar, flavorings, and other ingredients. It is typically sweet and comes in a variety of forms, including hard candies, gummies, and chocolates.",
                                ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                IsActive = true,
                                IsDeleted = false
                            }
                        }
                    },
                    new Manufacturer
                    {
                        Name = "Balocco",
                        Description = "Balocco is an Italian company that produces a wide range of baked goods, including cookies, cakes, and pastries. The company was founded in 1927 and is known for its high-quality products and traditional recipes.",
                        ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                        IsActive = true,
                        IsDeleted = false,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Cookies",
                                Description = "Cookies are a type of baked good that is typically sweet and made from flour, sugar, and butter. They come in a variety of flavors and textures, including chocolate chip, oatmeal, and sugar cookies.",
                                ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                IsActive = true,
                                IsDeleted = false
                            },
                            new Category
                            {
                                Name = "Cakes",
                                Description = "Cakes are a type of baked good that is typically sweet and made from flour, sugar, and eggs. They come in a variety of flavors and styles, including chocolate, vanilla, and red velvet cakes.",
                                ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                IsActive = true,
                                IsDeleted = false
                            }
                        }
                    },
                    new Manufacturer
                    {
                        Name = "Ferrero",
                        Description = "Ferrero is an Italian manufacturer of chocolate and other confectionery products. The company was founded in 1946 and is known for its popular brands, including Nutella, Ferrero Rocher, and Kinder Chocolate.",
                        ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                        IsActive = true,
                        IsDeleted = false,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Nutella",
                                Description = "Nutella is a popular chocolate-hazelnut spread that is made by Ferrero. It is commonly used as a topping for toast, pancakes, and other baked goods.",
                                ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                IsActive = true,
                                IsDeleted = false
                            },
                            new Category
                            {
                                Name = "Ferrero Rocher",
                                Description = "Ferrero Rocher is a popular chocolate confection that is made by Ferrero. It consists of a whole hazelnut surrounded by a thin wafer shell filled with hazelnut cream and covered in milk chocolate and chopped hazelnuts.",
                                ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                IsActive = true,
                                IsDeleted = false
                            }
                        }
                    }
                };

                await context.Manufacturers.AddRangeAsync(manufacturers);
                await context.SaveChangesAsync();
            }
        }
    }
}