using Microsoft.EntityFrameworkCore;
using Products.Database;
using Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Products.Services
{
    public static class ProductService
    {
        public static void PrintMenu()
        {
            Console.WriteLine("==================");
            Console.WriteLine("1. Получить список товаров");
            Console.WriteLine("2. Получить список самых дорогих товаров");
            Console.WriteLine("3. Поиск товара");
            Console.WriteLine("4. Получит список товаров по категории");
            Console.WriteLine("5. Добавить товар");
            Console.WriteLine("6. Удалить товар");
            Console.WriteLine("7. Изменить товар");
            Console.WriteLine("8. Изменить количество товара");
            Console.WriteLine("0. Выход");
            Console.WriteLine("==================");
        }
        public static void DoAction(int actionId)
        {
            var context = new ProductsContext();
            switch (actionId)
            {
                case 1:
                    foreach (var product in context.Products.Include(p => p.Category))
                    {
                        if (product.Count == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"{product.Title} - {product.Category.Title}");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                            Console.WriteLine($"{product.Title} - {product.Category.Title}");
                        Console.WriteLine($"Описание - {product.Description}");
                        Console.WriteLine($"Цена - {product.Price}руб.");
                        if (product.Discount > 0)
                        {
                            Console.WriteLine($"Скидка - {product.Discount * 100}%");
                            Console.WriteLine($"Цена со скидкой - {product.Price - (decimal)product.Discount * product.Price}руб.");
                        }
                        Console.WriteLine($"Количество - {product.Count}");
                        Console.WriteLine($"Изображение - {product.ImagePath}");
                        Console.WriteLine("------------");
                    }
                    break;
                case 2:
                    Console.WriteLine("Топ 3 самых дорогих товаров:");
                    foreach (var product in context.Products.OrderByDescending(p => p.Price).Take(3))
                    {
                        Console.WriteLine($"{product.Title} - {product.Price}руб");
                    }
                    break;
                case 3:
                    Console.WriteLine("Введите название товара:");
                    string search = Console.ReadLine();
                    int answerCount = 0;
                    Console.WriteLine("Результат поиска:\n");
                    foreach (var product in context.Products)
                    {
                        if (product.Title.ToLower().Contains(search.ToLower()))
                        {
                            answerCount++;
                            Console.WriteLine($"{answerCount}. {product.Title}");
                        }
                    }
                    if (answerCount == 0)
                        Console.WriteLine("Ничего не найдено :(\nПопробуйте ещё раз");
                    break;
                case 4:
                    Console.WriteLine("Выберите категорию: ");
                    int categoriesCount = 0;
                    foreach (var category in context.Categories)
                    {
                        Console.WriteLine(category.Id + ". " + category.Title);
                    }
                    try
                    {
                        int choosed = int.Parse(Console.ReadLine());
                        foreach (var product in context.Products.Where(p => p.CategoryId == choosed))
                        {
                            Console.WriteLine(product.Title);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Что-то пошло не так :(");
                    }                    
                    break;
                case 5:
                    try
                    {
                        Console.WriteLine("Введите название товара:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Введите описание товара:");
                        string description = Console.ReadLine();
                        Console.WriteLine("Введите количество товара:");
                        int count = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите цену товара:");
                        decimal price = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Введите скидку в процентах:");
                        double discount = double.Parse(Console.ReadLine()) / 100.0;
                        Console.WriteLine("Введите путь до изображения:");
                        string imagePath = Console.ReadLine();
                        Console.WriteLine("Выберите категорию: ");
                        foreach (var category in context.Categories)
                        {
                            Console.WriteLine($"{category.Id} - {category.Title}");
                        }
                        int categoryId = int.Parse(Console.ReadLine());
                        var productToAdd = new Product
                        {
                            Title = title,
                            Description = description,
                            Count = count,
                            Price = price,
                            Discount = discount,
                            ImagePath = imagePath,
                            CategoryId = categoryId
                        };
                        context.Products.Add(productToAdd);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch 
                        {
                            context.Products.Remove(productToAdd);
                            Console.WriteLine("Что-то пошло не fтак :(");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Что-то пошло не так!");
                    }
                    break;
                case 6:
                    Console.WriteLine("Выберите товар для удаления:");
                    foreach (var product in context.Products)
                    {
                        Console.WriteLine($"{product.Id}. {product.Title}");
                    }
                    int choosen = int.Parse(Console.ReadLine());
                    try
                    {
                        Product productToDelete = context.Products.First(p => p.Id == choosen);
                        context.Products.Remove(productToDelete);
                        context.SaveChanges();
                    }
                    catch 
                    {
                        Console.WriteLine("Что-то пошло не так!");
                    }
                    break;
                case 7:
                    Console.WriteLine("Выберите продукт для изменения:");
                    foreach (var product in context.Products)
                    {
                        Console.WriteLine($"{product.Id}. {product.Title}");
                    }
                    int choose = int.Parse(Console.ReadLine());
                    Console.WriteLine("Создайте новый продукт:");
                    try
                    {
                        Console.WriteLine("Введите название товара:");
                        string title = Console.ReadLine();
                        Console.WriteLine("Введите описание товара:");
                        string description = Console.ReadLine();
                        Console.WriteLine("Введите количество товара:");
                        int count = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите цену товара:");
                        decimal price = decimal.Parse(Console.ReadLine());
                        Console.WriteLine("Введите скидку в процентах:");
                        double discount = double.Parse(Console.ReadLine()) / 100.0;
                        Console.WriteLine("Введите путь до изображения:");
                        string imagePath = Console.ReadLine();
                        Console.WriteLine("Выберите категорию: ");
                        foreach (var category in context.Categories)
                        {
                            Console.WriteLine($"{category.Id} - {category.Title}");
                        }
                        int categoryId = int.Parse(Console.ReadLine());
                        var productToUpdate = new Product
                        {
                            Id = choose,
                            Title = title,
                            Description = description,
                            Count = count,
                            Price = price,
                            Discount = discount,
                            ImagePath = imagePath,
                            CategoryId = categoryId
                        };
                        var toUpdate = context.Products.FirstOrDefault(p => p.Id == choose);
                        context.Entry(toUpdate).State = EntityState.Detached;
                        toUpdate = productToUpdate;
                        context.Entry(toUpdate).State = EntityState.Modified;
                        context.Products.Update(toUpdate);
                        try
                        {
                            context.SaveChanges();
                        }
                        catch
                        {
                            Console.WriteLine("Что-то пошло не так :(");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Что-то пошло не так!");
                    }
                    break;
                case 8:
                    Console.WriteLine("Выберите продукт для изменения:");
                    foreach (var product in context.Products)
                    {
                        Console.WriteLine($"{product.Id}. {product.Title} {product.Count}шт.");
                    }
                    int productToChangeCountId = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите новое количество:");
                    int newCount = int.Parse(Console.ReadLine());
                    var toUpdateProduct = context.Products.FirstOrDefault(p => p.Id == productToChangeCountId);
                    var updatedCountProduct = toUpdateProduct;
                    updatedCountProduct.Count = newCount;
                    context.Entry(toUpdateProduct).State = EntityState.Detached;
                    toUpdateProduct = updatedCountProduct;
                    context.Entry(toUpdateProduct).State = EntityState.Modified;
                    context.Products.Update(toUpdateProduct);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch 
                    {

                        Console.WriteLine("Что-то пошло не так");
                    }
                    break;
                default:
                    Console.WriteLine("Неправильное действие!!!");
                    break;
            }
        }
    }
}
