using System;
using System.Windows;
using CurrencyExchangeWpfClient.CurrencyService;

namespace CurrencyExchangeWpfClient
{
    public partial class LoginWindow : Window
    {
        private Service1Client serviceClient;

        public LoginWindow()
        {
            InitializeComponent();
            serviceClient = new Service1Client();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.",
                                "Missing input",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                bool ok = serviceClient.Login(username, password);

                if (ok)
                {
                    MainWindow mainWindow = new MainWindow(username);
                    mainWindow.Show();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.",
                                    "Login failed",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username)
                || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a username and password.",
                                "Missing input",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                return;
            }

            try
            {
                bool ok = serviceClient.Register(username, password);

                if (ok)
                {
                    MessageBox.Show("Account created! You can now log in.",
                                    "Success",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(
                        "Username already exists or input is invalid.",
                        "Registration failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message,
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
    }
}