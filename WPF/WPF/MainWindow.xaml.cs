using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            using HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5043/");

            var loginData = new { login = loginTextBox.Text, password = passwordTextBox.Text };
            var content = new StringContent(JsonSerializer.Serialize(loginData), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync("api/User/login", content);

            if (response.IsSuccessStatusCode)
            {
                MainPage mainPage = new MainPage();
                mainPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void LocalLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }
    }
}