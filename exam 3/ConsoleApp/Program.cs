
using MiniShop.Data;
using MiniShop.Services;

DatabaseInitializer.Initialize();

StockService stock = new StockService();

while (true)
{
    Console.WriteLine("\n===== MINISHOP =====");
    Console.WriteLine("1. Register Sale");
    Console.WriteLine("2. Restock Product");
    Console.WriteLine("0. Exit");

    Console.Write("Choose: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Enter Product ID: ");
            int.TryParse(Console.ReadLine(), out int saleId);

            Console.Write("Enter Quantity: ");
            int.TryParse(Console.ReadLine(), out int saleQuantity);

            stock.RegisterSale(saleId, saleQuantity);
            break;

        case "2":
            Console.Write("Enter Product ID: ");
            int.TryParse(Console.ReadLine(), out int restockId);

            Console.Write("Enter Quantity: ");
            int.TryParse(Console.ReadLine(), out int restockQuantity);

            stock.Restock(restockId, restockQuantity);
            break;

        case "0":
            Console.WriteLine("Goodbye!");
            return;

        default:
            Console.WriteLine("Invalid choice!");
            break;
    }
}
