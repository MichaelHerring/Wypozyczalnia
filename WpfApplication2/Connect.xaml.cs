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

        private void tB1_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            tB1.Background = Brushes.White;
        }

        private void passwordBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            passwordBox.Background = Brushes.White;
        }
    }
}
