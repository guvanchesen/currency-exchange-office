using System;
using CurrencyExchangeClient.CurrencyService;

namespace CurrencyExchangeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Currency Exchange Client ===");
            Console.WriteLine();

            Service1Client client = new Service1Client();

            try
            {
                Console.Write("Amount to exchange: ");
                double amount = double.Parse(Console.ReadLine());

                Console.Write("From currency (e.g. USD): ");
                string from = Console.ReadLine().ToUpper();

                Console.Write("To currency (e.g. EUR): ");
                string to = Console.ReadLine().ToUpper();

                double result = client.ExchangeCurrency(amount, from, to);

                Console.WriteLine();
                Console.WriteLine(amount + " " + from + " = " + result + " " + to);

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error: " + ex.Message);
                client.Abort();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}