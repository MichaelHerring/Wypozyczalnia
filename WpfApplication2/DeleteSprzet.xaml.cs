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
    /// Logika interakcji dla klasy DeleteSprzet.xaml
    /// </summary>
    public partial class DeleteSprzet : Page
    {
        SqlConnection conn;

        SqlCommand delcmd;
        SqlCommand cmd;
        SqlDataReader reader;

        string query = "select * from Sprzet where IDSprzetu = @ID";
        string delete = "delete from Sprzet where IDSprzetu = @ID";

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;


        public DeleteSprzet()
        {
            InitializeComponent();
        }

        public DeleteSprzet(SqlConnection connection)
        {
            InitializeComponent();
            this.conn = connection;
        }


        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            btn2.IsEnabled = true;
            try
            {
                try
                {
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID", Tb_ID.Text);
                    reader = cmd.ExecuteReader();
                    reader.Read();

                    Tb_Kategoria.Text = reader.GetInt16(1).ToString();
                    Tb_Sprzet.Text = reader.GetString(2);
                    Tb_Data.Text = reader.GetSqlDateTime(3).ToString();
                    Tb_Opis.Text = reader.GetString(4);
                    Tb_Plec.Text = reader.GetString(5);
                    Tb_Cena.Text = reader.GetDouble(6).ToString();
                    reader.Close();
                }
                catch(Exception exc)
                {
                    btn2.IsEnabled = false;
                    wyslaneInfo(exc.Message);
                    Tb_Kategoria.Text = "";
                    Tb_Sprzet.Text = "";
                    Tb_Data.Text = "";
                    Tb_Opis.Text = "";
                    Tb_Plec.Text = "";
                }
                finally
                {
                    if(conn.State== System.Data.ConnectionState.Open)
                    {
                        reader.Close();
                    }

                }
            }
            catch (Exception x)
            {
                wyslaneInfo(x.Message);
            }
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
                if (MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Uwaga", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        int i = 0;
                        string id = Tb_Sprzet.Text;
                        delcmd = new SqlCommand(delete, conn);
                        delcmd.Parameters.AddWithValue("@ID", Tb_Sprzet.Text);
                        i = delcmd.ExecuteNonQuery();
                        Tb_Sprzet.Text = "";
                        Tb_Kategoria.Text = "";
                        Tb_Sprzet.Text = "";
                        Tb_Opis.Text = "";
                        Tb_Data.Text = "";
                        Tb_Plec.Text = "";

                        if (i != 0)
                        {
                            wyslaneInfo($"Usunięto rekord o numerze ID = {id} w tabeli Sprzet.");
                        }
                        else
                        {
                            wyslaneInfo("Błąd podczas usuwania.");
                        }
                    }

                    catch (Exception exc)
                    {
                        wyslaneInfo(exc.Message);
                    }
                }
        }

        private void Tb_ID_TextChanged(object sender, TextChangedEventArgs e)
        {
            btn2.IsEnabled = false;
        }
    }
}
