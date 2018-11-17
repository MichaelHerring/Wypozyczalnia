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
    /// Interaction logic for InsertWypozyczenie.xaml
    /// </summary>
    public partial class InsertWypozyczenie : Page
    {
        SqlConnection connection;

        string query = "insert into Wypozyczenie values(@ID1, @ID2, @dataOd, @dataDo)";
        SqlCommand command;

        public InsertWypozyczenie()
        {
            InitializeComponent();
        }

        public InsertWypozyczenie(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (tB1.Text == "" || tB2.Text == "" || tB3.Text == "" || tB4.Text == "")
            {
                MessageBox.Show("Wprowadź wszystkie dane, żadne pole nie może pozostać puste.", "Uwaga!");
            }
            else
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ID1", tB1.Text);
                command.Parameters.AddWithValue("@ID2", tB2.Text);
                command.Parameters.AddWithValue("@dataOd", tB3.Text);
                command.Parameters.AddWithValue("@dataDo", tB4.Text);

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
