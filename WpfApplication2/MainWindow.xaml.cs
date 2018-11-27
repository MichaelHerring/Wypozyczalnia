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
            InsertWypozyczenie.wyslaneInfo += WyswietlKomunikat;
        } 
        
        //przekazywanie wiadomości       
        void WyswietlKomunikat(string komunikat)
        {
            MessageViewer.Content += komunikat + "\n";
            MessageViewer.ScrollToEnd();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.Opacity = 0;
            DoubleAnimation NavAnimation = new DoubleAnimation();
            NavAnimation.BeginTime = TimeSpan.FromSeconds(1);
            NavAnimation.From = 0;
            NavAnimation.To = 1;
            NavAnimation.Duration = TimeSpan.FromSeconds(0.5);
            Navigation.BeginAnimation(OpacityProperty, NavAnimation);
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
            Container2.Content = new Table1(connection);
            StackPanel3.Visibility = Visibility.Hidden;
            czy_otwarta_trzecia = false;            
        }

        private void TableBtn2_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new Table2(connection);
            StackPanel3.Visibility = Visibility.Hidden;
            czy_otwarta_trzecia = false;
        }

        private void TableBtn3_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new Table3(connection);
            StackPanel3.Visibility = Visibility.Hidden;
            czy_otwarta_trzecia = false;
        }

        private void TableBtn4_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new Table4(connection);
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

        }

        private void btnWypozyczenieDel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRodzajDel_Click(object sender, RoutedEventArgs e)
        {

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
    }
}
