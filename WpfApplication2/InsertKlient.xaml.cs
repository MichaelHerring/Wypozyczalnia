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
    /// Interaction logic for InsertKlient.xaml
    /// </summary>
    public partial class InsertKlient : Page
    {
        SqlConnection connection;

        string query = "insert into Klient values(@imie, @nazwisko, @data, @tel, @miasto, @kraj)";
        SqlCommand command;

        public InsertKlient()
        {
            InitializeComponent();
        }

        public InsertKlient(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "" || tB2.Text == "" || tB3.Text == "" || tB4.Text == "" || tB5.Text == "" || tB6.Text == "")
            {
                MessageBox.Show("Wprowadź wszystkie dane, żadne pole nie może pozostać puste.", "Uwaga!");
            }
            else
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@imie", tB1.Text);
                command.Parameters.AddWithValue("@nazwisko", tB2.Text);
                command.Parameters.AddWithValue("@data", tB3.Text);
                command.Parameters.AddWithValue("@tel", tB4.Text);
                command.Parameters.AddWithValue("@miasto", tB5.Text);
                command.Parameters.AddWithValue("@kraj", tB6.Text);

                try
                {
                    command.ExecuteNonQuery();
                    MessageBox.Show("Dodano rekord do tabeli Klient");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
