using System;
using System.Windows;
using CurrencyExchangeWpfClient.CurrencyService;

namespace CurrencyExchangeWpfClient
{
    public partial class MainWindow : Window
    {
        private Service1Client serviceClient;

        public MainWindow()
        {
            InitializeComponent();

            serviceClient = new Service1Client();

            LoadCurrencies();
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
                MessageBox.Show(
                    "Could not load currencies: " + ex.Message,
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
                MessageBox.Show(
                    "Please enter a valid positive amount.",
                    "Invalid input",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (FromComboBox.SelectedItem == null || ToComboBox.SelectedItem == null)
            {
                MessageBox.Show(
                    "Please choose both currencies.",
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
                MessageBox.Show(
                    "Exchange failed: " + ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}