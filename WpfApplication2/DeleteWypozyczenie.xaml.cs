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
    /// Logika interakcji dla klasy DeleteWypozyczenie.xaml
    /// </summary>
    public partial class DeleteWypozyczenie : Page
    {
        SqlConnection conn;

        SqlCommand delcmd;
        SqlCommand cmd;
        SqlDataReader reader;

        string query = "select * from Wypozyczenie where IDWypozyczenia = @ID";
        string delete = "delete from Wypozyczenie where IDWypozyczenia = @ID";

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public DeleteWypozyczenie()
        {
            InitializeComponent();
        }
        public DeleteWypozyczenie(SqlConnection connection)
        {
            InitializeComponent();
            this.conn = connection;
        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ID",Tb_Wypozyczenie.Text);
                    reader = cmd.ExecuteReader();
                    reader.Read();

                    Tb_IDKlienta.Text = reader.GetInt16(2).ToString();
                    Tb_IDSprzetu.Text = reader.GetInt16(1).ToString();
                    Tb_DataOD.Text = reader.GetDateTime(3).ToString();
                    Tb_DataDO.Text = reader.GetDateTime(4).ToString();
                    reader.Close();



                }
                catch (Exception exc)
                {
                    wyslaneInfo(exc.Message);
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
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

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 1)
            {
                if (MessageBox.Show("Czy na pewno chcesz usunąć ten rekord?", "Uwaga", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    try
                    {
                        int i = 0;
                        delcmd = new SqlCommand(delete, conn);
                        delcmd.Parameters.AddWithValue("@ID", Tb_Wypozyczenie.Text);
                        i = delcmd.ExecuteNonQuery();
                        Tb_IDSprzetu.Text = "";
                        Tb_IDKlienta.Text= "";
                        Tb_DataOD.Text = "";
                        Tb_DataDO.Text = "";
                        Tb_Wypozyczenie.Text= "";
                        

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
}
