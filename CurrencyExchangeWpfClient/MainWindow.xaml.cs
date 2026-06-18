using System;
using System.Windows;
using CurrencyExchangeWpfClient.CurrencyService;

namespace CurrencyExchangeWpfClient
{
    public partial class MainWindow : Window
    {
        private Service1Client serviceClient;
        private string currentUsername;

        public MainWindow(string username)
        {
            InitializeComponent();
            serviceClient = new Service1Client();
            currentUsername = username;

            UserInfoTextBlock.Text = "Welcome " + currentUsername;

            LoadCurrencies();
            RefreshBalances();
        }

        private void LoadCurrencies()
        {
            try
            {
                string[] currencies = serviceClient.GetAvailableCurrencies();
                Array.Sort(currencies);

                foreach (string code in currencies)
                {
                    FromComboBox.Items.Add(code);
                    ToComboBox.Items.Add(code);
                }

                FromComboBox.SelectedItem = "PLN";
                ToComboBox.SelectedItem = "USD";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load currencies: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void RefreshBalances()
        {
            try
            {
                BalanceInfo info = serviceClient.GetAllBalances(currentUsername);
                string[] currencies = info.Currencies;
                double[] amounts = info.Amounts;

                BalancesListBox.Items.Clear();

                if (currencies.Length == 0)
                {
                    BalancesListBox.Items.Add("(no balances yet)");
                    return;
                }

                for (int i = 0; i < currencies.Length; i++)
                {
                    string line = currencies[i] + ":  " + amounts[i];
                    BalancesListBox.Items.Add(line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load balances: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void TopUpButton_Click(object sender, RoutedEventArgs e)
        {
            double amount;
            bool isValid = double.TryParse(TopUpAmountTextBox.Text, out amount);

            if (!isValid || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.",
                                "Invalid input",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                serviceClient.TopUp(currentUsername, amount);
                TopUpAmountTextBox.Text = "";
                RefreshBalances();
                StatusTextBlock.Text = "Topped up " + amount + " PLN.";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Top up failed: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            StatusTextBlock.Text = "";

            double amount;
            bool isValid = double.TryParse(AmountTextBox.Text, out amount);

            if (!isValid || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.",
                                "Invalid input",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            if (FromComboBox.SelectedItem == null
                || ToComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please choose both currencies.",
                                "Missing input",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            string fromCurrency = FromComboBox.SelectedItem.ToString();
            string toCurrency = ToComboBox.SelectedItem.ToString();

            if (fromCurrency == toCurrency)
            {
                MessageBox.Show("Pick two different currencies.",
                                "Invalid input",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                bool success = serviceClient.BuyCurrency(
                    currentUsername, amount, fromCurrency, toCurrency);

                if (success)
                {
                    StatusTextBlock.Text =
                        "Bought currency: spent " + amount + " " + fromCurrency
                        + " for " + toCurrency + ".";
                    AmountTextBox.Text = "";
                    RefreshBalances();
                }
                else
                {
                    MessageBox.Show(
                        "Insufficient funds in " + fromCurrency + ".",
                        "Transaction failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Transaction failed: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}