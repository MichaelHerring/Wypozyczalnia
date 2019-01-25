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
    /// Interaction logic for UpdateWypozyczenie.xaml
    /// </summary>
    public partial class UpdateWypozyczenie : Page
    {
        SqlConnection connection;

        string query = "select * from Wypozyczenie where IDWypozyczenia = @ID";
        string update;
        SqlCommand command;
        SqlCommand updateCommand;
        SqlDataReader reader;

        //delegat i zdarzenie do przekazywania wiadomości
        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public UpdateWypozyczenie()
        {
            InitializeComponent();
        }

        public UpdateWypozyczenie(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn2.IsEnabled = true;
            try //jeśli nie ma połączenia wykonuje sie finally, gdzie zamykany jest reader, który nie został utworzony, dlatego potrzebny jeszcze jeden try catch
            {
                try
                {
                    command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ID", Tb_Wypozyczenie.Text);
                    reader = command.ExecuteReader();
                    reader.Read();
                    Tb_IDSprzetu.Text = reader.GetInt16(1).ToString();
                    Tb_IDKlienta.Text = reader.GetInt16(2).ToString();
                    Tb_DataOD.Text = reader.GetDateTime(3).ToString();
                    Tb_DataDO.Text = reader.GetDateTime(4).ToString();
                    reader.Close(); //zamknięcie readera
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(exc.Message);
                    btn2.IsEnabled = false;
                    wyslaneInfo(exc.Message);
                    Tb_IDSprzetu.Text = "";
                    Tb_IDKlienta.Text = "";
                    Tb_DataOD.Text = "";
                    Tb_DataDO.Text = "";
                }
                finally
                {
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        reader.Close(); //zamknięcie readera jeśli wystąpi błąd
                    }
                }
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                wyslaneInfo(exc.Message);
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            update = "update Wypozyczenie set IDSprzetu = @sprzet, IDKlienta = @klient, Data_Od = @dataOd, Data_Do = @dataDo where IDWypozyczenia = @ID";
            updateCommand = new SqlCommand(update, connection);
            updateCommand.Parameters.AddWithValue("@ID", Tb_Wypozyczenie.Text);
            updateCommand.Parameters.AddWithValue("@sprzet", Tb_IDSprzetu.Text);
            updateCommand.Parameters.AddWithValue("@klient", Tb_IDKlienta.Text);
            updateCommand.Parameters.AddWithValue("@dataOd", Tb_DataOD.Text);
            updateCommand.Parameters.AddWithValue("@dataDo", Tb_DataDO.Text);
            if (Tb_Wypozyczenie.Text != String.Empty)
            {
                updateCommand.ExecuteNonQuery();
                wyslaneInfo("Zaktualizowano rekord");
            }
            else
            {
                wyslaneInfo("Wprowadź ID");
            }
        }

        private void Tb_Wypozyczenie_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn2.IsEnabled = false;
        }
    }
}
