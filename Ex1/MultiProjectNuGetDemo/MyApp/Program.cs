using System;

using MyLibrary;
using Newtonsoft.Json;

using Microsoft.Extensions.DependencyInjection;
using MyServices;
using MyBusiness;
using MyDataAccess;

class Program
{
    static void Main(string[] args)
    {
        //Czesc 1
        int sum = Calculator.Add(5, 3);
        int diff = Calculator.Subtract(5, 3);

        Console.WriteLine($"5 + 3 = {sum}");
        Console.WriteLine($"5 - 3 = {diff}");

        //Czesc 2
        sum = Calculator.Add(5, 3);
        var result = new { Operation = "Add", A = 5, B = 3, Result = sum };
        string jsonResult = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine(jsonResult);

        //Czesc 3

        // Konfiguracja kontenera DI
        var serviceProvider = new ServiceCollection()
            .AddSingleton<ILoggerService, ConsoleLogger>()
            .AddSingleton<Repository<Order>>()
            .AddSingleton<OrderService>()
            .BuildServiceProvider();

        // Uzyskanie instancji loggera
        var logger = serviceProvider.GetService<ILoggerService>();
        logger!.Log("Aplikacja uruchomiona.");

        // Przykładowe użycie kalkulatora
        sum = Calculator.Add(10, 15);
        logger.Log($"Wynik dodawania: {sum}");

        // Użycie OrderService z MyBusiness
        var orderService = serviceProvider.GetService<OrderService>();
        var order = new Order
        {
            OrderId = 1,
            CustomerName = "Jan Kowalski",
            OrderDate = DateTime.Now
        };
        orderService!.ProcessOrder(order);
    }
}
