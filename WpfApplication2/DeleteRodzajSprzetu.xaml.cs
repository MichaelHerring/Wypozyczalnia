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
    /// Logika interakcji dla klasy DeleteRodzajSprzetu.xaml
    /// </summary>
    public partial class DeleteRodzajSprzetu : Page
    {
        SqlConnection connection;

        SqlCommand cmd;
        SqlCommand delcmd;
        SqlDataReader reader;

        string query = "select * from Rodzaj_Sprzetu where IDKategorii = @ID";
        string delete = "delete from Rodzaj_Sprzetu where IDKategorii= @ID";

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        //public delegate void WyslijLiczbe(int liczba);
        //public static event WyslijLiczbe wyslanaliczba;

        public DeleteRodzajSprzetu()
        {
            InitializeComponent();
        }
        public DeleteRodzajSprzetu(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
            //wyslanaliczba(10);

        }

        private void btn_Pokaz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ID", TBID.Text);
                    reader=cmd.ExecuteReader();
                    reader.Read();
                    TBTEXT.Text = reader.GetString(1);
                    reader.Close();
                }
                catch(Exception exc)
                {
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
                //MessageBox.Show(exc.Message);
                wyslaneInfo(exc.Message);
            }
        }

        private void btn_Usun_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?","Uwaga",MessageBoxButton.YesNo)==MessageBoxResult.Yes)
            {
                try
                {
                    int i = 0;
                    delcmd = new SqlCommand(delete, connection);
                    delcmd.Parameters.AddWithValue("@ID", TBID.Text);
                    i = delcmd.ExecuteNonQuery();
                    TBID.Text = "";
                    if (i != 0)
                    {
                        //MessageBox.Show("Usunięto rekord");
                        wyslaneInfo("Usunięto rekord");
                    }
                    else
                    {
                        //MessageBox.Show("Błąd podczas usuwania.", "Error");
                        wyslaneInfo("Błąd podczas usuwania.");
                    }
                }

                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);
                }
            }            
        }
    }
}
