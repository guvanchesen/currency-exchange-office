using System.Collections.Generic;

namespace CurrencyExchangeService
{
    public static class UserStore
    {
        private static Dictionary<string, string> users
            = new Dictionary<string, string>();

        private static Dictionary<string, Dictionary<string, double>> balances
            = new Dictionary<string, Dictionary<string, double>>();

        public static bool Register(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (users.ContainsKey(username))
            {
                return false;
            }

            users[username] = password;

            balances[username] = new Dictionary<string, double>();

            balances[username]["PLN"] = 0.0;

            return true;
        }

        public static bool Login(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                return false;
            }

            return users[username] == password;
        }

        public static double GetBalance(string username, string currency)
        {
            if (!balances.ContainsKey(username))
            {
                return 0.0;
            }

            if (!balances[username].ContainsKey(currency))
            {
                return 0.0;
            }

            return balances[username][currency];
        }

        public static double TopUp(string username, double amount)
        {
            if (!balances.ContainsKey(username) || amount <= 0)
            {
                return GetBalance(username, "PLN");
            }

            if (!balances[username].ContainsKey("PLN"))
            {
                balances[username]["PLN"] = 0.0;
            }

            balances[username]["PLN"] += amount;
            return balances[username]["PLN"];
        }

        public static BalanceInfo GetAllBalances(string username)
        {
            BalanceInfo result = new BalanceInfo();

            if (!balances.ContainsKey(username))
            {
                result.Currencies = new string[0];
                result.Amounts = new double[0];
                return result;
            }

            Dictionary<string, double> userBalances = balances[username];
            result.Currencies = new string[userBalances.Count];
            result.Amounts = new double[userBalances.Count];

            int i = 0;
            foreach (KeyValuePair<string, double> entry in userBalances)
            {
                result.Currencies[i] = entry.Key;
                result.Amounts[i] = entry.Value;
                i++;
            }

            return result;
        }

        public static bool ApplyExchange(
            string username,
            string fromCurrency,
            double amountToDeduct,
            string toCurrency,
            double amountToAdd)
        {
            if (!balances.ContainsKey(username))
            {
                return false;
            }

            Dictionary<string, double> userBalances = balances[username];

            if (!userBalances.ContainsKey(fromCurrency))
            {
                userBalances[fromCurrency] = 0.0;
            }

            if (userBalances[fromCurrency] < amountToDeduct)
            {
                return false;
            }

            if (!userBalances.ContainsKey(toCurrency))
            {
                userBalances[toCurrency] = 0.0;
            }

            userBalances[fromCurrency] -= amountToDeduct;
            userBalances[toCurrency] += amountToAdd;

            return true;
        }
    }
}