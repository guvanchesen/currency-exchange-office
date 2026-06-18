using System;
using CurrencyExchangeClient.CurrencyService;

namespace CurrencyExchangeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Currency Exchange Client starting...");
            Console.WriteLine();
            Service1Client client = new Service1Client();

            try
            {
                Console.Write("Enter a currency code (e.g. USD, EUR, GBP): ");
                string code = Console.ReadLine();

                double rate = client.GetExchangeRate(code);

                Console.WriteLine();
                Console.WriteLine("Current rate for " + code.ToUpper() + ": " + rate + " PLN");

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                client.Abort();
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}