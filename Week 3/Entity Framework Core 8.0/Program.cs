/*using RetailInventory;
using RetailInventory.Models;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        // Create categories
        var electronics = new Category { Name = "Electronics" };
        var groceries = new Category { Name = "Groceries" };

        await context.Categories.AddRangeAsync(electronics, groceries);

        // Create products
        var product1 = new Product { Name = "Laptop", Price = 75000, Category = electronics };
        var product2 = new Product { Name = "Rice Bag", Price = 1200, Category = groceries };

        await context.Products.AddRangeAsync(product1, product2);

        // Save to database
        await context.SaveChangesAsync();

        Console.WriteLine("Data inserted successfully.");
    }
}
*/

/*using RetailInventory;
using RetailInventory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        Console.WriteLine("=== All Products ===");
        var products = await context.Products.ToListAsync();
        foreach (var p in products)
            Console.WriteLine($"{p.Name} - ₹{p.Price}");

        Console.WriteLine("\n=== Find Product by ID (1) ===");
        var product = await context.Products.FindAsync(1);
        Console.WriteLine($"Found: {product?.Name}");

        Console.WriteLine("\n=== First Product > ₹50,000 ===");
        var expensive = await context.Products.FirstOrDefaultAsync(p => p.Price > 50000);
        Console.WriteLine($"Expensive: {expensive?.Name}");
    }
}
*/

using RetailInventory;
using RetailInventory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        using var context = new AppDbContext();

        // Update a Product (Laptop)
        var product = await context.Products.FirstOrDefaultAsync(p => p.Name == "Laptop");
        if (product != null)
        {
            product.Price = 70000;
            await context.SaveChangesAsync();
            Console.WriteLine($"Updated {product.Name} to ₹{product.Price}");
        }

        //Delete a Product (Rice Bag)
        var toDelete = await context.Products.FirstOrDefaultAsync(p => p.Name == "Rice Bag");
        if (toDelete != null)
        {
            context.Products.Remove(toDelete);
            await context.SaveChangesAsync();
            Console.WriteLine($"Deleted: {toDelete.Name}");
        }

        //Show updated list
        var products = await context.Products.Include(p => p.Category).ToListAsync();
        Console.WriteLine("\n=== Products after update and delete ===");
        foreach (var p in products)
            Console.WriteLine($"{p.Name} ({p.Category.Name}) - ₹{p.Price}");

        Console.WriteLine("\n=== Filtered & Sorted Products (Price > ₹1000) ===");

        var filtered = await context.Products
            .Where(p => p.Price > 1000)
            .OrderByDescending(p => p.Price)
            .ToListAsync();

        foreach (var p in filtered)
            Console.WriteLine($"{p.Name} - ₹{p.Price}");


        Console.WriteLine("\n=== Product DTOs (Name & Price Only) ===");

        var productDTOs = await context.Products
            .Select(p => new { p.Name, p.Price })  // Anonymous DTO
            .ToListAsync();

        foreach (var dto in productDTOs)
            Console.WriteLine($"{dto.Name} - ₹{dto.Price}");

    }
}
