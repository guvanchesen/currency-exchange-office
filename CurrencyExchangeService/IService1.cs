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

        [OperationContract]
        bool Register(string username, string password);

        [OperationContract]
        bool Login(string username, string password);

        [OperationContract]
        double GetBalance(string username);

        [OperationContract]
        double TopUp(string username, double amount);
    }
}
