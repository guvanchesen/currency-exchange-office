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
            RefreshBalance();
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

                FromComboBox.SelectedItem = "USD";
                ToComboBox.SelectedItem = "EUR";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not load currencies: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void RefreshBalance()
        {
            try
            {
                double balance = serviceClient.GetBalance(currentUsername);
                BalanceTextBlock.Text = "Balance: " + balance + " PLN";
            }
            catch (Exception ex)
            {
                BalanceTextBlock.Text = "Balance: (error)";
                MessageBox.Show("Could not load balance: " + ex.Message,
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
                RefreshBalance();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Top up failed: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        private void ExchangeButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBlock.Text = "";

            double amount;
            bool isValidNumber = double.TryParse(AmountTextBox.Text, out amount);

            if (!isValidNumber || amount <= 0)
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

            try
            {
                double result = serviceClient.ExchangeCurrency(
                    amount, fromCurrency, toCurrency);

                ResultTextBlock.Text =
                    amount + " " + fromCurrency +
                    "  =  " +
                    result + " " + toCurrency;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exchange failed: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}