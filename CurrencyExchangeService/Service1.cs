using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CurrencyExchangeService
{
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return "You entered: " + value;
        }
        public double GetExchangeRate(string currencyCode)
        {
            if (currencyCode.ToUpper() == "PLN")
            {
                return 1.0;
            }
            string url = "http://api.nbp.pl/api/exchangerates/rates/A/"
                         + currencyCode
                         + "/?format=json";

            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                string jsonResponse = webClient.DownloadString(url);

                JObject parsedJson = JObject.Parse(jsonResponse);
                double rate = (double)parsedJson["rates"][0]["mid"];

                return rate;
            }
            catch (WebException)
            {
                throw new Exception(
                    "Could not fetch exchange rate for '" + currencyCode +
                    "'. Check that the code is valid (e.g. USD, EUR, GBP).");
            }
        }

        public double ExchangeCurrency(double amount, string fromCurrency, string toCurrency)
        {
            if (amount < 0)
            {
                throw new Exception("Amount cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(fromCurrency) || string.IsNullOrWhiteSpace(toCurrency))
            {
                throw new Exception("Both currency codes must be provided.");
            }

            double fromRate = GetExchangeRate(fromCurrency);
            double toRate = GetExchangeRate(toCurrency);

            double amountInPln = amount * fromRate;
            double result = amountInPln / toRate;

            return Math.Round(result, 2);
        }

        public string[] GetAvailableCurrencies()
        {
            string url = "http://api.nbp.pl/api/exchangerates/tables/A/?format=json";

            try
            {
                WebClient webClient = new WebClient();
                webClient.Encoding = System.Text.Encoding.UTF8;
                string jsonResponse = webClient.DownloadString(url);

                JArray parsedJson = JArray.Parse(jsonResponse);
                JArray rates = (JArray)parsedJson[0]["rates"];

                List<string> codes = new List<string>();
                foreach (JToken rate in rates)
                {
                    codes.Add((string)rate["code"]);
                }

                codes.Add("PLN");

                return codes.ToArray();
            }
            catch (WebException)
            {
                throw new Exception("Could not fetch the list of currencies from NBP.");
            }
        }
    }
}