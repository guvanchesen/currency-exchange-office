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
    }
}
