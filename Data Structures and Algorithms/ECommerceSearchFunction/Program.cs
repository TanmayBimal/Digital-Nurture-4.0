using System;

namespace ECommerceSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample products
            Product[] products = new Product[]
            {
                new Product(101, "Shoes", "Footwear"),
                new Product(102, "T-shirt", "Clothing"),
                new Product(103, "Laptop", "Electronics"),
                new Product(104, "Phone", "Electronics"),
                new Product(105, "Book", "Stationery")
            };

            // Sort products by ProductName for binary search
            Array.Sort(products, (p1, p2) => string.Compare(p1.ProductName, p2.ProductName, StringComparison.OrdinalIgnoreCase));

            Console.WriteLine("🛍️ Welcome to the E-Commerce Product Search System!");

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Linear Search");
                Console.WriteLine("2. Binary Search");
                Console.WriteLine("3. Exit");

                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Write("Enter product name to search (Linear): ");
                        string linearSearchTerm = Console.ReadLine();
                        int linearIndex = LinearSearch(products, linearSearchTerm);
                        if (linearIndex != -1)
                            Console.WriteLine($"✅ Product found: {products[linearIndex]}");
                        else
                            Console.WriteLine("❌ Product not found using linear search.");
                        break;

                    case "2":
                        Console.Write("Enter product name to search (Binary): ");
                        string binarySearchTerm = Console.ReadLine();
                        int binaryIndex = BinarySearch(products, binarySearchTerm);
                        if (binaryIndex != -1)
                            Console.WriteLine($"✅ Product found: {products[binaryIndex]}");
                        else
                            Console.WriteLine("❌ Product not found using binary search.");
                        break;

                    case "3":
                        Console.WriteLine("👋 Exiting the search system. Have a nice day!");
                        running = false;
                        break;

                    default:
                        Console.WriteLine("⚠️ Invalid choice! Please select 1, 2 or 3.");
                        break;
                }
            }
        }

        public static int LinearSearch(Product[] products, string targetName)
        {
            for (int i = 0; i < products.Length; i++)
            {
                if (products[i].ProductName.Equals(targetName, StringComparison.OrdinalIgnoreCase))
                    return i;
            }
            return -1;
        }

        public static int BinarySearch(Product[] products, string targetName)
        {
            int left = 0;
            int right = products.Length - 1;

            while (left <= right)
            {
                int mid = left + (right - left) / 2;
                int comparison = string.Compare(products[mid].ProductName, targetName, StringComparison.OrdinalIgnoreCase);

                if (comparison == 0)
                    return mid;
                else if (comparison < 0)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return -1;
        }
    }

    public class Product
    {
        public int ProductId { get; }
        public string ProductName { get; }
        public string Category { get; }

        public Product(int productId, string productName, string category)
        {
            ProductId = productId;
            ProductName = productName;
            Category = category;
        }

        public override string ToString()
        {
            return $"ID: {ProductId}, Name: {ProductName}, Category: {Category}";
        }
    }
}
