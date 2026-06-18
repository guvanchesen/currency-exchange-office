using System.Collections.Generic;

namespace CurrencyExchangeService
{
    public static class UserStore
    {
        private static Dictionary<string, string> users
            = new Dictionary<string, string>();

        private static Dictionary<string, double> balances
            = new Dictionary<string, double>();

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
            balances[username] = 0.0; 
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

        public static double GetBalance(string username)
        {
            if (!balances.ContainsKey(username))
            {
                return 0.0;
            }

            return balances[username];
        }

        public static double TopUp(string username, double amount)
        {
            if (!balances.ContainsKey(username) || amount <= 0)
            {
                return GetBalance(username);
            }

            balances[username] += amount;
            return balances[username];
        }
    }
}