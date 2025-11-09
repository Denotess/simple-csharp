# Inventory Manager

A console-based inventory management system built with Entity Framework Core and SQLite. This application allows you to manage products, track stock levels, and monitor stock movements with persistent data storage.

What it does
- Add products with details (name, description, price, quantity, reorder level, category).
- Remove products from the inventory with confirmation prompts.
- View all products with full details or search for specific products by name/description.
- Add and remove stock with automatic tracking of stock movements.
- View low stock products that need reordering based on reorder levels.
- Calculate total inventory value across all products.
- View stock movement history (all movements or filtered by product).
- Persist all data to SQLite database (`inventory.db`).

Data structure
The application uses Entity Framework Core with two main entities:
- **Product**: Stores product information (ID, name, description, price, quantity, reorder level, category).
- **StockMovement**: Tracks all stock changes (IN/OUT movements with timestamps and reasons).

Categories supported: Electronics, Toys, Books, Tools, Food.

Prerequisites
- .NET 9 SDK (or compatible .NET 9 runtime + SDK).
- Entity Framework Core packages (included in project dependencies).

Check your SDK version:

```bash
dotnet --version
```

Build & run
From the repository root you can build and run the project:

```bash
dotnet build inventoryManager
dotnet run --project inventoryManager
```

First-time setup
On first run, the application will create the SQLite database (`inventory.db`) automatically using Entity Framework Core migrations. The database file will be created in the project directory.

Notes & behaviour
- All data is persisted to `inventory.db` using SQLite and Entity Framework Core.
- Stock movements are automatically recorded when adding or removing stock.
- Products can be searched by name or description using partial text matching.
- Low stock alerts are based on comparing current quantity with the reorder level.
- Inventory value is calculated as sum of (price × quantity) for all products.
- When removing products, the app requests confirmation to prevent accidental deletions.

Quick menu reference
- 1 Add Product
- 2 Remove Product
- 3 View All Products
- 4 View Product Details
- 5 Search Products
- 6 Add Stock
- 7 Remove Stock
- 8 View Low Stock Products
- 9 View Inventory Value
- 10 View Stock Movements
- 0 Exit
