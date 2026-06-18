using System.Runtime.Serialization;

namespace CurrencyExchangeService
{
    [DataContract]
    public class BalanceInfo
    {
        [DataMember]
        public string[] Currencies { get; set; }

        [DataMember]
        public double[] Amounts { get; set; }
    }
}