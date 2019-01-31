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
    /// Interaction logic for UpdateRodzaj.xaml
    /// </summary>
    public partial class UpdateRodzaj : Page
    {
        SqlConnection connection;

        string query = "select * from Rodzaj_Sprzetu where IDKategorii = @ID";
        string update;
        SqlCommand command;
        SqlCommand updateCommand;
        SqlDataReader reader;

        //delegat i zdarzenie do przekazywania wiadomości
        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public UpdateRodzaj()
        {
            InitializeComponent();
        }

        public UpdateRodzaj(SqlConnection conn)
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
                    command.Parameters.AddWithValue("@ID", TBID.Text);
                    reader = command.ExecuteReader();
                    reader.Read();
                    TBTEXT.Text = reader.GetString(1);
                    reader.Close(); //zamknięcie readera
                }
                catch (Exception exc)
                {
                    btn2.IsEnabled = false;
                    wyslaneInfo(exc.Message);
                    TBTEXT.Text = "";
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
                wyslaneInfo(exc.Message);
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                update = "update Rodzaj_Sprzetu set Nazwa_Kategorii = @nazwa where IDKategorii = @ID";
                updateCommand = new SqlCommand(update, connection);
                updateCommand.Parameters.AddWithValue("@ID", TBID.Text);
                updateCommand.Parameters.AddWithValue("@nazwa", TBTEXT.Text);
                if (TBID.Text != String.Empty)
                {
                    updateCommand.ExecuteNonQuery();
                    wyslaneInfo($"Zaktualizowano rekord o numerze ID = {TBID.Text} w tabeli Rodzaj Sprzetu.");
                }
                else
                {
                    wyslaneInfo("Wprowadź ID");
                }
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void TBID_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn2.IsEnabled = false;
        }
    }
}
