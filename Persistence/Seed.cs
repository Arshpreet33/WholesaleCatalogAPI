using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        DisplayName = "Tony",
                        UserName = "admin",
                        Email = "Tbadolato@sympatico.ca",
                        Role = "Admin",
                        IsActive = true
                    },
                    new AppUser
                    {
                        DisplayName = "Tom",
                        UserName = "tom",
                        Email = "tom@test.com",
                        Role = "User",
                        IsActive = true
                    },
                    new AppUser
                    {
                        DisplayName = "Jane",
                        UserName = "jane",
                        Email = "jane@test.com",
                        Role = "User",
                        IsActive = true
                    }
                };

                var roles = new List<string> { "Admin", "User" };

                foreach (var role in roles)
                {
                    var roleExists = await roleManager.RoleExistsAsync(role);
                    if (!roleExists)
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }

                foreach (var user in users)
                {
                    var result = await userManager.CreateAsync(user, user.UserName == "admin" ? "Tony@123" : "Pa$$w0rd");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, user.Role);
                    }
                }
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
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
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
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
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
                        IsDeleted = false,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    }
                };

                await context.Clients.AddRangeAsync(clients);
                await context.SaveChangesAsync();
            }

            if (!context.Manufacturers.Any())
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
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Chocolate",
                                Description = "Chocolate is a food product made from roasted and ground cacao seeds that is typically sweetened. It is commonly used as a flavoring ingredient in many foods, such as cakes, cookies, and ice cream.",
                                ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Products = new List<Product>
                                {
                                    new Product
                                    {
                                        Name = "PERUGINA BACI TUBES Milk Chocolate",
                                        Code = "839456005525",
                                        UnitPrice = 1.95m,
                                        UnitWeight = 37.5,
                                        ItemsInCase = 14,
                                        CasePrice = 27.30m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "PERUGINA BACI TUBES Dark Chocolate",
                                        Code = "839456005501",
                                        UnitPrice = 1.95m,
                                        UnitWeight = 37.5,
                                        ItemsInCase = 14,
                                        CasePrice = 27.30m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "PERUGINA BACI BULK",
                                        Code = "839456005551",
                                        UnitPrice = 25.00m,
                                        UnitWeight = 750,
                                        ItemsInCase = 4,
                                        CasePrice = 100.00m,
                                        ItemsInStock = 50,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "PERUGINA BACI ADVENT CALENDAR",
                                        Code = "839456005581",
                                        UnitPrice = 15.65m,
                                        UnitWeight = 278,
                                        ItemsInCase = 10,
                                        CasePrice = 156.50m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.perugina.com/wp-content/uploads"
                                    }
                                }
                            },
                            new Category
                            {
                                Name = "Candy",
                                Description = "Candy is a confection made from sugar, flavorings, and other ingredients. It is typically sweet and comes in a variety of forms, including hard candies, gummies, and chocolates.",
                                ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Products = new List<Product>
                                {
                                    new Product
                                    {
                                        Name = "DOLCE SCOPERTE Candy",
                                        Code = "839456005591",
                                        UnitPrice = 11.00m,
                                        UnitWeight = 200,
                                        ItemsInCase = 8,
                                        CasePrice = 88.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.perugina.com/wp-content/uploads/2019/11/Perugina-Logo.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    }
                                }
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
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Cookies",
                                Description = "Cookies are a type of baked good that is typically sweet and made from flour, sugar, and butter. They come in a variety of flavors and textures, including chocolate chip, oatmeal, and sugar cookies.",
                                ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Products = new List<Product>
                                {
                                    new Product
                                    {
                                        Name = "BALOCCO NOVELLINI Milk Cookies",
                                        Code = "8001100012384",
                                        UnitPrice = 2.50m,
                                        UnitWeight = 350,
                                        ItemsInCase = 10,
                                        CasePrice = 25.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "BALOCCO NOVELLINI Chocolate Cookies",
                                        Code = "8001100012386",
                                        UnitPrice = 2.50m,
                                        UnitWeight = 350,
                                        ItemsInCase = 10,
                                        CasePrice = 25.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "BALOCCO NOVELLINI Vanilla Cookies",
                                        Code = "8001100012387",
                                        UnitPrice = 2.50m,
                                        UnitWeight = 350,
                                        ItemsInCase = 10,
                                        CasePrice = 25.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    }
                                }
                            },
                            new Category
                            {
                                Name = "Cakes",
                                Description = "Cakes are a type of baked good that is typically sweet and made from flour, sugar, and eggs. They come in a variety of flavors and styles, including chocolate, vanilla, and red velvet cakes.",
                                ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Products = new List<Product>
                                {
                                    new Product
                                    {
                                        Name = "BALOCCO PANETTONE Cake",
                                        Code = "8001100012385",
                                        UnitPrice = 10.00m,
                                        UnitWeight = 750,
                                        ItemsInCase = 4,
                                        CasePrice = 40.00m,
                                        ItemsInStock = 50,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.balocco.it/wp-content/uploads/2019/11/logo-balocco.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    }
                                }
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
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        Categories = new List<Category>
                        {
                            new Category
                            {
                                Name = "Nutella",
                                Description = "Nutella is a popular chocolate-hazelnut spread that is made by Ferrero. It is commonly used as a topping for toast, pancakes, and other baked goods.",
                                ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Products = new List<Product>
                                {
                                    new Product
                                    {
                                        Name = "FERRERO NUTELLA Spread",
                                        Code = "8001100012785",
                                        UnitPrice = 5.00m,
                                        UnitWeight = 750,
                                        ItemsInCase = 6,
                                        CasePrice = 30.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "FERRERO NUTELLA B-READY",
                                        Code = "8001100012795",
                                        UnitPrice = 2.50m,
                                        UnitWeight = 132,
                                        ItemsInCase = 12,
                                        CasePrice = 30.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    }
                                }
                            },
                            new Category
                            {
                                Name = "Ferrero Rocher",
                                Description = "Ferrero Rocher is a popular chocolate confection that is made by Ferrero. It consists of a whole hazelnut surrounded by a thin wafer shell filled with hazelnut cream and covered in milk chocolate and chopped hazelnuts.",
                                ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                IsActive = true,
                                IsDeleted = false,
                                CreatedAt = DateTime.Now,
                                UpdatedAt = DateTime.Now,
                                Products = new List<Product>
                                {
                                    new Product
                                    {
                                        Name = "FERRERO ROCHER Chocolate",
                                        Code = "8001100012786",
                                        UnitPrice = 1.00m,
                                        UnitWeight = 12,
                                        ItemsInCase = 24,
                                        CasePrice = 24.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "FERRERO ROCHER Gift Box",
                                        Code = "8001100012796",
                                        UnitPrice = 10.00m,
                                        UnitWeight = 200,
                                        ItemsInCase = 12,
                                        CasePrice = 120.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    },
                                    new Product
                                    {
                                        Name = "FERRERO ROCHER T24",
                                        Code = "8001100012797",
                                        UnitPrice = 20.00m,
                                        UnitWeight = 300,
                                        ItemsInCase = 24,
                                        CasePrice = 480.00m,
                                        ItemsInStock = 100,
                                        CasesInStock = 10,
                                        ImageUrl = "https://www.ferrero.com/wp-content/uploads/2020/03/logo-ferrero.png",
                                        IsActive = true,
                                        IsDeleted = false,
                                        CreatedAt = DateTime.Now,
                                        UpdatedAt = DateTime.Now
                                    }
                                }
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