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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Table4.xaml
    /// </summary>
    public partial class Table4 : Page
    {
        SqlConnection connection;

        public Table4()
        {
            InitializeComponent();
        }

        public Table4(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
        }

        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable table;
        string query = "select * from Rodzaj_Sprzetu";

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand(query, connection);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();

            try { 
                adapter.Fill(table);
                DataGr.ItemsSource = table.DefaultView;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
