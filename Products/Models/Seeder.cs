using Products.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Models
{
    public static class Seeder
    {
        public static void ItialSeed()
        {
            using (var context = new ProductsContext())
            {
                if (!context.Products.Any() && !context.Categories.Any())
                {
                    Category food = new Category()
                    {
                        Title = "Еда"
                    };
                    context.Categories.Add(food);
                    Category drinks = new Category()
                    {
                        Title = "Напитки"
                    };
                    context.Categories.Add(drinks);

                    Product milk = new Product()
                    {
                        Title = "Молоко",
                        Description = "5% жирности",
                        CategoryId = 2,
                        Count = 10,
                        ImagePath = "img/milk.png",
                        Price = 100,
                        Discount = 0.1,
                    };
                    context.Products.Add(milk);
                    Product cookie = new Product()
                    {
                        Title = "Печенье \"К чаю\"",
                        Description = "Хорошо подойдёт к чаю :)",
                        CategoryId = 1,
                        Count = 100,
                        ImagePath = "img/cookies_for_tea.png",
                        Price = 40,
                        Discount = 0,
                    };
                    context.Products.Add(cookie);
                    Product cocacola = new Product()
                    {
                        Title = "Coca cola",
                        Description = "Символ Нового года!",
                        CategoryId = 2,
                        Count = 150,
                        ImagePath = "img/cocacola.jpg",
                        Price = 80,
                        Discount = 0.15,
                    };
                    context.Products.Add(cocacola);
                    Product bread = new Product()
                    {
                        Title = "Хлеб ремесленный",
                        Description = "Хлеб ручной работы",
                        CategoryId = 1,
                        Count = 150,
                        ImagePath = "img/bread.jpg",
                        Price = 300,
                        Discount = 0.2,
                    };
                    context.Products.Add(bread);
                    context.SaveChanges();
                }
            }
        }
    }
}
