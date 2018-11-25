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
    /// Interaction logic for InsertSprzet.xaml
    /// </summary>
    public partial class InsertSprzet : Page
    {
        SqlConnection connection;

        string query = "insert into Sprzet values(@idKat, @nazwa, @data, @opis, @md)";
        SqlCommand command;

        string fillID = "select IDKategorii from Rodzaj_Sprzetu order by IDKategorii";
        SqlCommand command1;
        SqlDataReader reader;

        string fillNazwa = "select distinct Nazwa_Sprzetu from Sprzet";

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public InsertSprzet()
        {
            InitializeComponent();
        }

        public InsertSprzet(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try //jeśli nie ma połączenia wykonuje sie finally, gdzie zamykany jest reader, który nie został utworzony, dlatego potrzebny jeszcze jeden try catch
            {
                try
                {
                    command1 = new SqlCommand(fillID, connection);
                    reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        cB1.Items.Add(reader.GetInt16(0));
                    }
                    reader.Close();

                    command1 = new SqlCommand(fillNazwa, connection);
                    reader = command1.ExecuteReader();
                    while (reader.Read())
                    {
                        cB2.Items.Add(reader.GetString(0));
                    }
                    reader.Close();
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(exc.Message);
                    wyslaneInfo(exc.Message);
                }
                finally
                {
                    reader.Close();
                }
            }
            catch(Exception exc)
            {
                //MessageBox.Show(exc.Message);
                wyslaneInfo(exc.Message);
            }
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            if (cB1.Text == "" || cB2.Text == "" || dP1.Text == "" || tB4.Text == "" || cB3.Text == "")
            {
                //MessageBox.Show("Wprowadź wszystkie dane, żadne pole nie może pozostać puste.", "Uwaga!");
                wyslaneInfo("Wprowadź wszystkie dane, żadne pole nie może pozostać puste.");
            }
            else
            {
                command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idKat", cB1.Text);
                command.Parameters.AddWithValue("@nazwa", cB2.Text);
                command.Parameters.AddWithValue("@data", dP1.Text);
                command.Parameters.AddWithValue("@opis", tB4.Text);
                command.Parameters.AddWithValue("@md", cB3.Text);

                try
                {
                    command.ExecuteNonQuery();
                    //MessageBox.Show("Dodano rekord do tabeli Sprzęt");
                    wyslaneInfo("Dodano rekord do tabeli Sprzęt");
                }
                catch (Exception exc)
                {
                    //MessageBox.Show(exc.Message);
                    wyslaneInfo(exc.Message);
                }
            }
        }

    }
}
