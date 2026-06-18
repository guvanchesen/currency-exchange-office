using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrencyExchangeService
{
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public double GetExchangeRate(string currencyCode)
        {
            string url = "http://api.nbp.pl/api/exchangerates/rates/A/"
                         + currencyCode
                         + "/?format=json";
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string jsonResponse = webClient.DownloadString(url);
            JObject parsedJson = JObject.Parse(jsonResponse);
            double rate = (double)parsedJson["rates"][0]["mid"];
            return rate;
        }

    }
}

