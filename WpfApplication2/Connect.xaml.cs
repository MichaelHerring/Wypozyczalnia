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
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                btn2.IsEnabled = true;
                btn1.IsEnabled = false;
            }
            else if (connection.State == System.Data.ConnectionState.Closed)
            {
                btn1.IsEnabled = true;
                btn2.IsEnabled = false;
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            builder.DataSource = "WIN-KOMPUTER\\SQLEXPRESS";
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

            //connection = new SqlConnection(builder.ConnectionString);
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
                //MessageBox.Show("Połączono");
                btn2.IsEnabled = true;
                btn1.IsEnabled = false;
                tB1.Text = "";
                passwordBox.Clear();
                chB1.IsChecked = false;
                MainWindow win = new MainWindow(connection);
                win.Show();
                this.Close();
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                builder.IntegratedSecurity = false;
                builder.UserID = "";
                builder.Password = "";
                tB1.Text = "";
                passwordBox.Clear();
                chB1.IsChecked = false;
                MessageBox.Show("Rozłączono");
                btn1.IsEnabled = true;
                btn2.IsEnabled = false;
            }
            catch (SqlException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
