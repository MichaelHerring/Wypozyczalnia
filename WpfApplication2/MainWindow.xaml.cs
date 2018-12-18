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
using System.Windows.Media.Animation;
using System.Data.SqlClient;
using System.Data;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        
        bool czy_otwarta_pierwsza = false;
        bool czy_otwarta_druga = false;
        bool czy_otwarta_trzecia = false;
        bool czy_otwarta_czwarta = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(SqlConnection conn)
        {
            InitializeComponent();
            this.connection = conn;
            DeleteKlient.wyslaneInfo += WyswietlKomunikat;
            InsertKlient.wyslaneInfo += WyswietlKomunikat;
            InsertRodzaj.wyslaneInfo += WyswietlKomunikat;
            InsertSprzet.wyslaneInfo += WyswietlKomunikat;
            DeleteSprzet.wyslaneInfo += WyswietlKomunikat;
            DeleteRodzajSprzetu.wyslaneInfo += WyswietlKomunikat;
            //DeleteRodzajSprzetu.wyslanaliczba += WyswietlKomunikaty;
            InsertWypozyczenie.wyslaneInfo += WyswietlKomunikat;
        } 
        
        //przekazywanie wiadomości       
        void WyswietlKomunikat(string komunikat)
        {
            MessageViewer.Content += komunikat + "\n";
            MessageViewer.ScrollToEnd();

            DoubleAnimation FadeIn = new DoubleAnimation();
            FadeIn.From = 0;
            FadeIn.To = 1;
            FadeIn.Duration = TimeSpan.FromSeconds(1);

            DoubleAnimation FadeOut = new DoubleAnimation();
            FadeOut.From = 1;
            FadeOut.To = 0;
            FadeOut.Duration = TimeSpan.FromSeconds(1);

            ImageBulbBlack.BeginAnimation(OpacityProperty, FadeOut);
            ImageBulb.BeginAnimation(OpacityProperty, FadeIn);

            ImageBulb.BeginAnimation(OpacityProperty, FadeOut);
            ImageBulbBlack.BeginAnimation(OpacityProperty, FadeIn);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //animacja menu
            btn1.Opacity = 0;
            btn2.Opacity = 0;
            btn3.Opacity = 0;
            btn4.Opacity = 0;
            btn5.Opacity = 0;

            DoubleAnimation NavAnimation = new DoubleAnimation();
            NavAnimation.BeginTime = TimeSpan.FromSeconds(0.4);
            NavAnimation.From = 0;
            NavAnimation.To = 1;
            NavAnimation.Duration = TimeSpan.FromSeconds(0.5);
            btn1.BeginAnimation(OpacityProperty, NavAnimation);

            NavAnimation.BeginTime = TimeSpan.FromSeconds(0.6);
            btn2.BeginAnimation(OpacityProperty, NavAnimation);

            NavAnimation.BeginTime = TimeSpan.FromSeconds(0.8);
            btn3.BeginAnimation(OpacityProperty, NavAnimation);

            NavAnimation.BeginTime = TimeSpan.FromSeconds(1);
            btn4.BeginAnimation(OpacityProperty, NavAnimation);

            NavAnimation.BeginTime = TimeSpan.FromSeconds(1.2);
            btn5.BeginAnimation(OpacityProperty, NavAnimation);
        }

       

        //Główne przyciski z menu
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Connect win = new Connect();
            win.Show();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (czy_otwarta_pierwsza == false)
            {
                StackPanel1.Visibility = Visibility.Visible;
                czy_otwarta_pierwsza = true;
            }
            else if (czy_otwarta_pierwsza == true)
            {
                StackPanel1.Visibility = Visibility.Hidden;
                czy_otwarta_pierwsza = false;
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (czy_otwarta_druga == false)
            {
                StackPanel2.Visibility = Visibility.Visible;
                czy_otwarta_druga = true;
            }
            else if (czy_otwarta_druga == true)
            {
                StackPanel2.Visibility = Visibility.Hidden;
                czy_otwarta_druga = false;
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (czy_otwarta_trzecia == false)
            {
                StackPanel3.Visibility = Visibility.Visible;
                czy_otwarta_trzecia = true;
            }
            else if (czy_otwarta_trzecia == true)
            {
                StackPanel3.Visibility = Visibility.Hidden;
                czy_otwarta_trzecia = false;
            }
        }

        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (StackPanel4.Visibility == Visibility.Visible)
            {
                StackPanel4.Visibility = Visibility.Hidden;
            }
            else if (StackPanel4.Visibility == Visibility.Hidden)
            {
                StackPanel4.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 100;
                animation.Duration = TimeSpan.FromSeconds(0.20);
                StackPanel4.BeginAnimation(HeightProperty, animation);
            }*/
            if (czy_otwarta_czwarta == false)
            {
                StackPanel4.Visibility = Visibility.Visible;
                czy_otwarta_czwarta = true;
            }
            else if (czy_otwarta_czwarta == true)
            {
                StackPanel4.Visibility = Visibility.Hidden;
                czy_otwarta_czwarta = false;
            }
        }

        private void btn2_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Visible;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Hidden;
            czy_otwarta_pierwsza = true;
            czy_otwarta_druga = false;
            czy_otwarta_trzecia = false;
            czy_otwarta_czwarta = false;
        }

        private void btn3_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Visible;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Hidden;
            czy_otwarta_pierwsza = false;
            czy_otwarta_druga = true;
            czy_otwarta_trzecia = false;
            czy_otwarta_czwarta = false;
        }

        private void btn4_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Visible;
            StackPanel4.Visibility = Visibility.Hidden;
            czy_otwarta_pierwsza = false;
            czy_otwarta_druga = false;
            czy_otwarta_trzecia = true;
            czy_otwarta_czwarta = false;
        }
        private void btn5_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
            StackPanel2.Visibility = Visibility.Hidden;
            StackPanel3.Visibility = Visibility.Hidden;
            StackPanel4.Visibility = Visibility.Visible;
            czy_otwarta_pierwsza = false;
            czy_otwarta_druga = false;
            czy_otwarta_trzecia = false;
            czy_otwarta_czwarta = true;
        }

        //Przyciski do wyświetlania tabel
        private void TableBtn1_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Klient");
            StackPanel3.Visibility = Visibility.Hidden;
            czy_otwarta_trzecia = false;            
        }

        private void TableBtn2_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Sprzet");
            StackPanel3.Visibility = Visibility.Hidden;
            czy_otwarta_trzecia = false;
        }

        private void TableBtn3_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Wypozyczenie");
            StackPanel3.Visibility = Visibility.Hidden;
            czy_otwarta_trzecia = false;
        }

        private void TableBtn4_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new ShowTable(connection, "Rodzaj_Sprzetu");
            StackPanel3.Visibility = Visibility.Hidden;
            czy_otwarta_trzecia = false;
        }        

        //Przyciski do wyswietlania formularzy do wstawiania rekordów do konkretnych tabel
        private void btnKlient_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertKlient(connection);
            StackPanel1.Visibility = Visibility.Hidden;
            czy_otwarta_pierwsza = false;
        }

        private void btnSprzet_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertSprzet(connection);
            StackPanel1.Visibility = Visibility.Hidden;
            czy_otwarta_pierwsza = false;
        }

        private void btnWypozyczenie_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertWypozyczenie(connection);
            StackPanel1.Visibility = Visibility.Hidden;
            czy_otwarta_pierwsza = false;
        }

        private void btnRodzaj_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertRodzaj(connection);
            StackPanel1.Visibility = Visibility.Hidden;
            czy_otwarta_pierwsza = false;
        }

        //Przyciski do wyswietlania formularzy do usuwania z konkretnych tabel
        private void btnKlientDel_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteKlient(connection);
            StackPanel2.Visibility = Visibility.Hidden;
            czy_otwarta_druga = false;
            
        }

        private void btnSprzetDel_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteSprzet(connection);
            StackPanel2.Visibility = Visibility.Hidden;
            czy_otwarta_druga = false;
        }

        private void btnWypozyczenieDel_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteWypozyczenie(connection);
            StackPanel2.Visibility = Visibility.Hidden;
            czy_otwarta_druga = false;
        }

        private void btnRodzajDel_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteRodzajSprzetu(connection);
            StackPanel2.Visibility = Visibility.Hidden;
            czy_otwarta_druga = false;
        }

        //Przyciski do wyswietlania formularzy do updatowania konkretnych tabel
        private void btnKlientUpdate_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateKlient();
            StackPanel4.Visibility = Visibility.Hidden;
            czy_otwarta_czwarta = false;
        }

        private void btnSprzetUpdate_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateSprzet();
            StackPanel4.Visibility = Visibility.Hidden;
            czy_otwarta_czwarta = false;
        }

        private void btnWypozyczenieUpdate_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateWypozyczenie();
            StackPanel4.Visibility = Visibility.Hidden;
            czy_otwarta_czwarta = false;
        }

        private void btnRodzajUpdate_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new UpdateRodzaj();
            StackPanel4.Visibility = Visibility.Hidden;
            czy_otwarta_czwarta = false;
        }

        //przycisk do wyświetlania bocznego panelu
        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            if (SideStackPannel.Visibility == Visibility.Visible)
            {
                SideStackPannel.Visibility = Visibility.Hidden;
            }
            else if (SideStackPannel.Visibility == Visibility.Hidden)
            {
                SideStackPannel.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 160;
                animation.Duration = TimeSpan.FromSeconds(0.20);
                SideStackPannel.BeginAnimation(WidthProperty, animation);
            }
        }

        private void KliKniecie_w_oknie(object sender, MouseButtonEventArgs e)
        {
            if(e.ClickCount==1)
            {
                StackPanel1.Visibility = Visibility.Hidden;
                StackPanel2.Visibility = Visibility.Hidden;
                StackPanel3.Visibility = Visibility.Hidden;
                StackPanel4.Visibility = Visibility.Hidden;
            }
        }

        //Wyszukiwanie
        private void btnWyszukaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string tabela = ComboBoxTabela.Text;
                string kolumna = ComboBoxKolumna.Text;
                string wartosc = TextBoxWartosc.Text;
                if (ComboBoxTabela.SelectedItem == null || ComboBoxKolumna.SelectedItem == null || wartosc == String.Empty)
                {
                    WyswietlKomunikat("Wybierz tabelę i kolumnę oraz wprowadź wartość.");
                }
                else
                {
                    string query = $"select * from {tabela} where {kolumna} like '{wartosc}'";
                    Container2.Content = new WynikiWyszukania(query, connection);
                }
            }
            catch (Exception exc)
            {
                WyswietlKomunikat(exc.Message);
            }
        }

        //Wyszukiwanie po kliknięciu entera
        private void TextBoxWartosc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnWyszukaj_Click(this, new RoutedEventArgs());
            }
        }

        private void ComboBoxTabela_DropDownClosed(object sender, EventArgs e)
        {
            try
            {
                //czyszczenie comboboxa z kolumnami
                ComboBoxKolumna.Items.Clear();
                //dodawanie nazw kolumn
                if (ComboBoxTabela.SelectedItem != null)
                {
                    string selectedTable = ComboBoxTabela.Text;
                    string query = $"select * from {selectedTable}";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    foreach (DataColumn c in table.Columns)
                    {
                        ComboBoxKolumna.Items.Add(c.ToString());
                    }
                }
            }
            catch (Exception exc)
            {
                WyswietlKomunikat(exc.Message);
            }
        }
    }
}
