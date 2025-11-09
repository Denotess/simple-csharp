using System.ComponentModel;
using System.Linq.Expressions;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

public class Program
{
    static void Main(string[] args)
    {
        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("\n=== Inventory Manager ===");
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Remove Product");
            Console.WriteLine("3. View All Products");
            Console.WriteLine("4. View Product Details");
            Console.WriteLine("5. Search Products");
            Console.WriteLine("6. Add Stock");
            Console.WriteLine("7. Remove Stock");
            Console.WriteLine("8. View Low Stock Products");
            Console.WriteLine("9. View Inventory Value");
            Console.WriteLine("10. View Stock Movements");
            Console.WriteLine("0. Exit");
            Console.Write("\nChoose option: ");

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    InventoryManager.AddProduct();
                    break;
                case "2":
                    InventoryManager.RemoveProduct();
                    break;
                case "3":
                    InventoryManager.ViewAllProducts();
                    break;
                case "4":
                    InventoryManager.ViewProductDetails();
                    break;
                case "5":
                    InventoryManager.SearchProducts();
                    break;
                case "6":
                    Console.Write("Enter Product ID: ");
                    if (int.TryParse(Console.ReadLine(), out int addProductId))
                    {
                        Console.Write("Enter quantity to add: ");
                        if (int.TryParse(Console.ReadLine(), out int addQty))
                        {
                            InventoryManager.AddStock(addProductId, addQty);
                        }
                    }
                    break;
                case "7":
                    Console.Write("Enter Product ID: ");
                    if (int.TryParse(Console.ReadLine(), out int removeProductId))
                    {
                        Console.Write("Enter quantity to remove: ");
                        if (int.TryParse(Console.ReadLine(), out int removeQty))
                        {
                            InventoryManager.RemoveStock(removeProductId, removeQty);
                        }
                    }
                    break;
                case "8":
                    InventoryManager.ViewLowStockProducts();
                    break;
                case "9":
                    InventoryManager.ViewInventoryValue();
                    break;
                case "10":
                    InventoryManager.ViewStockMovements();
                    break;
                case "0":
                    keepRunning = false;
                    Console.WriteLine("Goodbye!");
                    break;
                default:
                    Console.WriteLine("Invalid option!");
                    break;
            }
        }
    }
}

public class InventoryContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<StockMovement> StockMovements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=inventory.db");
    }
}

public enum Category
{
    Electronics,
    Toys,
    Books,
    Tools,
    Food
}
public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public int ReorderLevel { get; set; }
    public Category Category { get; set; }

    public Product() { }

    public Product(int productId, string name, string description, decimal price, int quantity, int reorderLevel, Category category)
    {
        ProductId = productId;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
        ReorderLevel = reorderLevel;
        Category = category;

    }

    public bool IsLowStock()
    {
        return Quantity <= ReorderLevel;
    }
}
public class StockMovement
{
    public int StockMovementId { get; set; }
    public int ProductId { get; set; }
    public string Type { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public DateTime Date { get; set; }
    public string Reason { get; set; } = string.Empty;

    public StockMovement() { }

    public StockMovement(int stockMovementId, int productId, string type, int quantity, DateTime date, string reason)
    {
        StockMovementId = stockMovementId;
        ProductId = productId;
        Type = type;
        Quantity = quantity;
        Date = date;
        Reason = reason;
    }

}

class InventoryManager
{
    public static void AddProduct()
    {
        Console.WriteLine("\n=== Add New Product ===");

        Console.Write("Product name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Description: ");
        string description = Console.ReadLine() ?? "";

        Console.Write("Price: ");
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
        {
            Console.WriteLine("Invalid price!");
            return;
        }

        Console.Write("Quantity: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
        {
            Console.WriteLine("Invalid quantity!");
            return;
        }

        Console.Write("Reorder Level: ");
        if (!int.TryParse(Console.ReadLine(), out int reorderLevel) || reorderLevel < 0)
        {
            Console.WriteLine("Invalid reorder level!");
            return;
        }

        Console.WriteLine("\nCategory:");
        Console.WriteLine("1. Electronics\n2. Toys\n3. Books\n4. Tools\n5. Food");
        Console.Write("Choose category (1-5): ");
        if (!int.TryParse(Console.ReadLine(), out int categoryChoice) ||
            categoryChoice < 1 || categoryChoice > 5)
        {
            Console.WriteLine("Invalid category!");
            return;
        }

        Category category = (Category)(categoryChoice - 1);

        var newProduct = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            Quantity = quantity,
            ReorderLevel = reorderLevel,
            Category = category
        };

        try
        {
            using (var context = new InventoryContext())
            {
                context.Products.Add(newProduct);
                context.SaveChanges();
                Console.WriteLine($"\nProduct added successfully! ID: {newProduct.ProductId}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
    public static void RemoveProduct()
    {
        Console.WriteLine("\n=== Remove Product ===");

        Console.Write("Enter Product ID to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        using (var context = new InventoryContext())
        {
            var product = context.Products.Find(productId);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            Console.WriteLine($"\nAbout to delete: {product.Name}");
            Console.Write("Are you sure? (y/n): ");
            string confirm = Console.ReadLine()?.ToLower() ?? "";

            if (confirm == "y")
            {
                context.Products.Remove(product);
                context.SaveChanges();
                Console.WriteLine("Product removed successfully!");
            }
            else
            {
                Console.WriteLine("Deletion cancelled.");
            }
        }
    }
    public static void ViewAllProducts()
    {
        using (var context = new InventoryContext())
        {
            var allProducts = context.Products
            .OrderBy(products => products.Name)
            .ToList();

            if (allProducts.Count == 0)
            {
                Console.WriteLine("No products in inventory");
                return;
            }

            foreach (Product product in allProducts)
            {
                Console.WriteLine($"Id: {product.ProductId}");
                Console.WriteLine($"Name: {product.Name}");
                Console.WriteLine($"Description: {product.Description}");
                Console.WriteLine($"Quantity: {product.Quantity}");
                Console.WriteLine($"Price: {product.Price:F2}");
                Console.WriteLine($"Reorder level: {product.ReorderLevel}");
                Console.WriteLine($"Category: {product.Category}");

            }
        }
    }
    public static void ViewProductDetails()
    {
        Console.WriteLine("\n=== View Product Details ===");

        Console.Write("Enter Product ID: ");
        if (!int.TryParse(Console.ReadLine(), out int productId))
        {
            Console.WriteLine("Invalid ID!");
            return;
        }

        using (var context = new InventoryContext())
        {
            var product = context.Products.Find(productId);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            Console.WriteLine($"\n--- Product Details ---");
            Console.WriteLine($"ID: {product.ProductId}");
            Console.WriteLine($"Name: {product.Name}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine($"Price: ${product.Price:F2}");
            Console.WriteLine($"Quantity: {product.Quantity}");
            Console.WriteLine($"Reorder Level: {product.ReorderLevel}");
            Console.WriteLine($"Category: {product.Category}");
        }
    }
    public static void SearchProducts()
    {
        Console.Write("Search: ");
        string searchTerm = Console.ReadLine() ?? "";
        using (var context = new InventoryContext())
        {
            var search = context.Products
            .Where(p => p.Name.Contains(searchTerm) ||
                        p.Description.Contains(searchTerm))
            .ToList();
            Console.WriteLine($"Found {search.Count} products: ");
            foreach (var product in search)
            {
                Console.WriteLine($"- {product.Name} (${product.Price})");
            }
        }

    }
    public static void AddStock(int productId, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Quantity must be positive!");
            return;
        }

        using (var context = new InventoryContext())
        {
            var product = context.Products.Find(productId);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            product.Quantity += quantity;
            var movement = new StockMovement
            {
                ProductId = productId,
                Type = "IN",
                Quantity = quantity,
                Date = DateTime.Now,
                Reason = "Stock added"
            };

            context.StockMovements.Add(movement);
            context.SaveChanges();

            Console.WriteLine($"Added {quantity} units to {product.Name}");
            Console.WriteLine($"New quantity: {product.Quantity}");
        }
    }
    public static void RemoveStock(int productId, int quantity)
    {
        if (quantity <= 0)
        {
            Console.WriteLine("Quantity must be positive!");
            return;
        }

        using (var context = new InventoryContext())
        {
            var product = context.Products.Find(productId);

            if (product == null)
            {
                Console.WriteLine("Product not found!");
                return;
            }

            if (product.Quantity < quantity)
            {
                Console.WriteLine($"Insufficient stock! Available: {product.Quantity}");
                return;
            }

            product.Quantity -= quantity;
            var movement = new StockMovement
            {
                ProductId = productId,
                Type = "OUT",
                Quantity = quantity,
                Date = DateTime.Now,
                Reason = "Stock removed"
            };

            context.StockMovements.Add(movement);
            context.SaveChanges();

            Console.WriteLine($"Removed {quantity} units from {product.Name}");
            Console.WriteLine($"New quantity: {product.Quantity}");
        }
    }
    public static void ViewLowStockProducts()
    {
        using (var context = new InventoryContext())
        {
            var lowStock = context.Products
            .Where(p => p.Quantity <= p.ReorderLevel)
            .OrderBy(p => p.Quantity)
            .ToList();

            if (lowStock.Count == 0)
            {
                Console.WriteLine("No items with low stock");
            }
            else
            {
                Console.WriteLine($"\n=== Low Stock Products ({lowStock.Count}) ===");
                foreach (var product in lowStock)
                {
                    Console.WriteLine($"{product.Name}: {product.Quantity} (Reorder at: {product.ReorderLevel})");
                }
            }

        }

    }
    public static void ViewInventoryValue()
    {
        using (var context = new InventoryContext())
        {
            decimal totalValue = context.Products
            .Sum(p => p.Price * p.Quantity);

            int productCount = context.Products.Count();

            Console.WriteLine($"\n=== Inventory Summary ===");
            Console.WriteLine($"Total Products: {productCount}");
            Console.WriteLine($"Total Value: ${totalValue:F2}");
        }
    }
    public static void ViewStockMovements()
    {
        Console.WriteLine("\n=== Stock Movement History ===");
        Console.WriteLine("1. View all movements");
        Console.WriteLine("2. View movements for specific product");
        Console.Write("Choose option: ");

        string choice = Console.ReadLine() ?? "";

        using (var context = new InventoryContext()){
            List<StockMovement> movements;
            if (choice == "2")
            {
                Console.Write("Enter product ID: ");
                if (!int.TryParse(Console.ReadLine(), out int productId))
                {
                    Console.WriteLine("Invalid ID!");
                    return;
                }

                movements = context.StockMovements
                .Where(sm => sm.ProductId == productId)
                .OrderByDescending(sm => sm.Date)
                .ToList();
            }
            else
            {
                movements = context.StockMovements
                    .OrderByDescending(sm => sm.Date)
                    .ToList();
            }
            if (movements.Count == 0)
            {
                Console.WriteLine("No movements found.");
                return;
            }
                    foreach (var movement in movements)
            {
                var product = context.Products.Find(movement.ProductId);
                string productName = product?.Name ?? "Unknown";

                Console.WriteLine($"\n[{movement.Date:yyyy-MM-dd HH:mm}]");
                Console.WriteLine($"Product: {productName} (ID: {movement.ProductId})");
                Console.WriteLine($"Type: {movement.Type}");
                Console.WriteLine($"Quantity: {movement.Quantity}");
                Console.WriteLine($"Reason: {movement.Reason}");
            }
        }

    }
}