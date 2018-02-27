using System;
using ChinookConsole.DataAccess;

namespace ChinookConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var run = true;
            while (run)
            {
                ConsoleKeyInfo userInput = MainMenu();

                switch (userInput.KeyChar)
                {
                    case '0':
                        run = false;
                        break;
                    case '1':
                        Console.Clear();
                        var invoiceInfoQuery = new InvoiceInfoQuery();
                        var invoices = invoiceInfoQuery.GetAgent();
                        //var invoiceDetails = invoiceInfoQuery.GetInvoiceInfo();

                        Console.WriteLine("Sales agents and their corresponding invoice id's:");

                        foreach (var invoice in invoices)
                        {
                            Console.WriteLine($"Sales Agent Name: {invoice.Name}, Invoice ID: {invoice.InvoiceId}");
                        }
                        Console.ReadLine();
                        break;
                }
            }

            ConsoleKeyInfo MainMenu()
            {
                View mainMenu = new View()
                        .AddMenuOption("Show the invoices associated with each sales agent")
                        .AddMenuText("Press 0 to exit.");

                Console.Write(mainMenu.GetFullMenu());
                ConsoleKeyInfo userOption = Console.ReadKey();
                return userOption;
            }
        }
    }
}





