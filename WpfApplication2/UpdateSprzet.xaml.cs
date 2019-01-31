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
    /// Interaction logic for UpdateSprzet.xaml
    /// </summary>
    public partial class UpdateSprzet : Page
    {
        SqlConnection connection;

        string query = "select * from Sprzet where IDSprzetu = @ID";
        string update;
        SqlCommand command;
        SqlCommand updateCommand;
        SqlDataReader reader;

        string fillID = "select IDKategorii from Rodzaj_Sprzetu order by IDKategorii";
        SqlCommand command1;
        SqlDataReader reader1;

        string fillNazwa = "select distinct Nazwa_Sprzetu from Sprzet";

        //delegat i zdarzenie do przekazywania wiadomości
        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public UpdateSprzet()
        {
            InitializeComponent();
        }

        public UpdateSprzet(SqlConnection conn)
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
                    command.Parameters.AddWithValue("@ID", Tb_ID.Text);
                    reader = command.ExecuteReader();
                    reader.Read();
                    Tb_Kategoria.Text = reader.GetInt16(1).ToString();
                    Tb_Sprzet.Text = reader.GetString(2);
                    Tb_Data.Text = reader.GetDateTime(3).ToString();
                    Tb_Opis.Text = reader.GetString(4);
                    Tb_Plec.Text = reader.GetString(5);
                    Tb_Cena.Text = reader.GetDouble(6).ToString();
                    reader.Close(); //zamknięcie readera
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(exc.Message);
                    btn2.IsEnabled = false;
                    wyslaneInfo(exc.Message);
                    Tb_Kategoria.Text = "";
                    Tb_Sprzet.Text = "";
                    Tb_Data.Text = "";
                    Tb_Opis.Text = "";
                    Tb_Plec.Text = "";
                    Tb_Cena.Text = "";
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
                update = "update Sprzet set IDKategorii = @kategoria, Nazwa_Sprzetu = @nazwa, Data_Zakupu = @data, Opis = @opis, Meski_Damski = @plec, CenaZaGodzine = @cena where IDSprzetu = @ID";
                updateCommand = new SqlCommand(update, connection);
                updateCommand.Parameters.AddWithValue("@ID", Tb_ID.Text);
                updateCommand.Parameters.AddWithValue("@kategoria", Tb_Kategoria.Text);
                updateCommand.Parameters.AddWithValue("@nazwa", Tb_Sprzet.Text);
                updateCommand.Parameters.AddWithValue("@data", Tb_Data.Text);
                updateCommand.Parameters.AddWithValue("@opis", Tb_Opis.Text);
                updateCommand.Parameters.AddWithValue("@plec", Tb_Plec.Text);
                updateCommand.Parameters.AddWithValue("@cena", Tb_Cena.Text);
                if (Tb_ID.Text != String.Empty)
                {
                    updateCommand.ExecuteNonQuery();
                    wyslaneInfo($"Zaktualizowano rekord o numerze ID = {Tb_ID.Text} w tabeli Sprzet.");
                }
                else
                {
                    wyslaneInfo("Wprowadź ID");
                }
            }
            catch(Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }

        private void Tb_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn2.IsEnabled = false;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try //jeśli nie ma połączenia wykonuje sie finally, gdzie zamykany jest reader, który nie został utworzony, dlatego potrzebny jeszcze jeden try catch
            {
                try
                {
                    command1 = new SqlCommand(fillID, connection);
                    reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        Tb_Kategoria.Items.Add(reader1.GetInt16(0));
                    }
                    reader1.Close();

                    command1 = new SqlCommand(fillNazwa, connection);
                    reader1 = command1.ExecuteReader();
                    while (reader1.Read())
                    {
                        Tb_Sprzet.Items.Add(reader1.GetString(0));
                    }
                    reader1.Close();
                }
                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);
                }
                finally
                {
                    reader1.Close();
                }
            }
            catch (Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }
    }
}
