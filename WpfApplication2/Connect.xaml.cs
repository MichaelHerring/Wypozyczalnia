using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Windows.Media.Animation;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Connect.xaml
    /// </summary>
    public partial class Connect : Window
    {
        SqlConnection connection = new SqlConnection();
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
        decimal i = 0;

        public Connect()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation FadeIn = new DoubleAnimation();
            FadeIn.From = 0;
            FadeIn.To = 1;
            FadeIn.Duration = TimeSpan.FromSeconds(0.8);
            MainGrid.BeginAnimation(OpacityProperty, FadeIn);

            HintLogin.Width = 0;
            HintHaslo.Width = 0;
            DoubleAnimation WidthAnimation = new DoubleAnimation();
            WidthAnimation.From = 0;
            WidthAnimation.To = 165;
            WidthAnimation.BeginTime = TimeSpan.FromSeconds(0.3);
            WidthAnimation.Duration = TimeSpan.FromSeconds(1.8);

            HintLogin.BeginAnimation(WidthProperty, WidthAnimation);
            HintHaslo.BeginAnimation(WidthProperty, WidthAnimation);

            FadeIn.BeginTime = TimeSpan.FromSeconds(0.3);
            FadeIn.Duration = TimeSpan.FromSeconds(0.3);
            HintLogin.BeginAnimation(OpacityProperty, FadeIn);
            HintHaslo.BeginAnimation(OpacityProperty, FadeIn);
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            builder.DataSource = "localhost";
            //builder.DataSource = "WIN-KOMPUTER\\SQLEXPRESS";
            builder.InitialCatalog = "Wypozyczalnia";
            if (chB1.IsChecked == true)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.UserID = tB1.Text;
                builder.Password = passwordBox.Password;
            }

            connection.ConnectionString = builder.ConnectionString;

            try
            {
                connection.Open();
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
                i++;
                if (i == 3)
                {
                    btn1.IsEnabled = false;
                }
            }

            if (connection.State == System.Data.ConnectionState.Open)
            {
                tB1.Text = "";
                passwordBox.Clear();
                chB1.IsChecked = false;
                MainWindow win = new MainWindow(connection);
                win.Show();
                this.Close();
            }
        }

        //ukrywanie hintów
        private void tB1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tB1.Background = Brushes.White;
        }

        private void passwordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Background = Brushes.White;
        }

        //kliknięcie entera
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btn1_Click(this, new RoutedEventArgs());
            }
        }
    }
}
