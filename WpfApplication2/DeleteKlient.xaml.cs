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
    /// Interaction logic for DeleteKlient.xaml
    /// </summary>
    public partial class DeleteKlient : Page
    {
        SqlConnection connection;

        string query = "select * from Klient where IDKlienta = @ID";
        string delete = "delete from Klient where IDKlienta = @ID";
        SqlCommand command;
        SqlCommand delCommand;
        SqlDataReader reader;

        public DeleteKlient()
        {
            InitializeComponent();
        }

        public DeleteKlient(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            try //jeśli nie ma połączenia wykonuje sie finally, gdzie zamykany jest reader, który nie został utworzony, dlatego potrzebny jeszcze jeden try catch
            {
                try
                {
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", tB0.Text);
                    reader = command.ExecuteReader();
                    reader.Read();
                    tB1.Text = reader.GetString(1);
                    tB2.Text = reader.GetString(2);
                    tB3.Text = reader.GetDateTime(3).ToString();
                    tB4.Text = reader.GetString(4);
                    tB5.Text = reader.GetString(5);
                    tB6.Text = reader.GetString(6);
                    reader.Close(); //zamknięcie readera
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        reader.Close(); //zamknięcie readera jeśli wystąpi błąd
                    }
                }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Uwaga", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    int i = 0;
                    delCommand = new SqlCommand(delete, connection);
                    delCommand.Parameters.AddWithValue("@ID", tB0.Text);
                    i = delCommand.ExecuteNonQuery(); //number of affected rows
                    tB0.Text = "";
                    tB1.Text = "";
                    tB2.Text = "";
                    tB3.Text = "";
                    tB4.Text = "";
                    tB5.Text = "";
                    tB6.Text = "";
                    if (i != 0)
                    {
                        MessageBox.Show("Usunięto rekord");
                    }
                    else
                    {
                        MessageBox.Show("Błąd podczas usuwania.", "Error");
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }
    }
}
