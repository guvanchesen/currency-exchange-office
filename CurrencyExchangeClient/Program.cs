using System;

// This is the namespace you typed when adding the service reference.
// It gives us access to the auto-generated client classes.
using CurrencyExchangeClient.CurrencyService;

namespace CurrencyExchangeClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Currency Exchange Client starting...");
            // Create a client object that knows how to talk to our WCF service.
            // Think of this as picking up the phone to call the service.
            Service1Client client = new Service1Client();

            try
            {
                // Call the GetData method on the service.
                // We pass the number 42 — the service will echo it back in a string.
                string response = client.GetData(42);

                // Print whatever the service returned.
                Console.WriteLine("Service responded with: " + response);

                // Always close the connection cleanly when done.
                client.Close();
            }
            catch (Exception ex)
            {
                // If something goes wrong (service not running, network issue, etc.)
                // we print the error instead of crashing silently.
                Console.WriteLine("Error calling the service: " + ex.Message);
                client.Abort();
            }

            // Keep the console window open so we can see the result.
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}