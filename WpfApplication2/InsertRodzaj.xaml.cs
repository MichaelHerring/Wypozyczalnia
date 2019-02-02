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

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for InsertRodzaj.xaml
    /// </summary>
    public partial class InsertRodzaj : Page
    {
        SqlConnection connection;

        string query = "insert into RodzajSprzetu values(@nazwa)";
        SqlCommand command;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public InsertRodzaj()
        {
            InitializeComponent();
        }

        public InsertRodzaj(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "")
            {
                wyslaneInfo("Wprowadź wszystkie dane.");
            }
            else
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nazwa", tB1.Text);

                try
                {
                    command.ExecuteNonQuery();
                    wyslaneInfo("Dodano rekord do tabeli RodzajSprzetu.");
                    tB0.Text = "";
                    tB1.Text = "";
                }
                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);
                }
            }
        }
    }
}
