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

                        Console.WriteLine("Sales agents and their corresponding invoice id's:");

                        foreach (var invoice in invoices)
                        {
                            Console.WriteLine($"Sales Agent Name: {invoice.Name}, Invoice ID: {invoice.InvoiceId}");
                        }
                        Console.ReadLine();
                        break;

                    case '2':
                        Console.Clear();
                        var invoiceInfoQuery2 = new InvoiceInfoQuery();
                        var invoiceTotals = invoiceInfoQuery2.GetInvoiceTotal();

                        Console.WriteLine("Invoice Total, customer name, country and sales agent names for all invoices:");

                        foreach (var invoice in invoiceTotals)
                        {
                            Console.WriteLine($"Invoice Total: {invoice.Total}, Sales Agent Name: {invoice.SalesAgent}, Customer Info: {invoice.CustomerName}, {invoice.BillingCountry}");
                        }
                        Console.ReadLine();
                        break;

                    case '3':
                        Console.Clear();
                        Console.WriteLine("Type invoice ID to view the amount of line items it has:");

                        var consoleUserInput = Console.ReadLine();
                        var invoiceInfoQuery3 = new InvoiceInfoQuery();
                        var lineItemTotals = invoiceInfoQuery3.GetLineItems(int.Parse(consoleUserInput));

                        Console.WriteLine($"This invoice has {lineItemTotals} line items.");
                        Console.ReadLine();
                        break;
                }
            }

            ConsoleKeyInfo MainMenu()
            {
                View mainMenu = new View()
                        .AddMenuOption("View the invoices associated with each sales agent")
                        .AddMenuOption("View the invoice total, customer name, country and sales agent name for all invoices")
                        .AddMenuOption("View the number of line items for an invoice")
                        .AddMenuText("Press 0 to exit.");

                Console.Write(mainMenu.GetFullMenu());
                ConsoleKeyInfo userOption = Console.ReadKey();
                return userOption;
            }
        }
    }
}





