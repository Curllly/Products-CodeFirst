using Products.Database;
using Products.Models;
using Products.Services;

Seeder.ItialSeed();

while (true)
{
    ProductService.PrintMenu();
    int action = int.Parse(Console.ReadLine());
    if (action == 0)
        break;
    ProductService.DoAction(action);
}