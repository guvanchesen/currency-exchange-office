using System.ServiceModel;

namespace CurrencyExchangeService
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        double GetExchangeRate(string currencyCode);

        [OperationContract]
        double ExchangeCurrency(double amount, string fromCurrency,string toCurrency);

        [OperationContract]
        string[] GetAvailableCurrencies();
    }
}
