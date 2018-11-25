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

        //ukrywanie paneli
        private void StackPanel1_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel1.Visibility = Visibility.Hidden;
        }

        private void StackPanel2_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel2.Visibility = Visibility.Hidden;
        }

        private void StackPanel3_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel3.Visibility = Visibility.Hidden;
        }

        //Główne przyciski z menu
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            Connect win = new Connect();
            win.Show();
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel1.Visibility == Visibility.Visible)
            {
                StackPanel1.Visibility = Visibility.Hidden;                
            }
            else if (StackPanel1.Visibility == Visibility.Hidden)
            {
                StackPanel1.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 100;
                animation.Duration = TimeSpan.FromSeconds(0.20);
                StackPanel1.BeginAnimation(HeightProperty, animation);
            }
        }

        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel2.Visibility == Visibility.Visible)
            {
                StackPanel2.Visibility = Visibility.Hidden;
            }
            else if (StackPanel2.Visibility == Visibility.Hidden)
            {
                StackPanel2.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 100;
                animation.Duration = TimeSpan.FromSeconds(0.20);
                StackPanel2.BeginAnimation(HeightProperty, animation);
            }
        }

        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            if (StackPanel3.Visibility == Visibility.Visible)
            {
                StackPanel3.Visibility = Visibility.Hidden;
            }
            else if (StackPanel3.Visibility == Visibility.Hidden)
            {
                StackPanel3.Visibility = Visibility.Visible;
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = 100;
                animation.Duration = TimeSpan.FromSeconds(0.20);
                StackPanel3.BeginAnimation(HeightProperty, animation);
            }
        }

        //Przyciski do wyświetlania tabel
        private void TableBtn1_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new Table1(connection);
        }

        private void TableBtn2_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new Table2(connection);
        }

        private void TableBtn3_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new Table3(connection);
        }

        private void TableBtn4_Click(object sender, RoutedEventArgs e)
        {
            Container2.Content = new Table4(connection);
        }        

        //Przyciski do wstawiania rekordów do konkretnych tabel
        private void btnKlient_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertKlient(connection);
        }

        private void btnSprzet_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertSprzet(connection);
        }

        private void btnWypozyczenie_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertWypozyczenie(connection);
        }

        private void btnRodzaj_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new InsertRodzaj(connection);
        }

        //Przyciski do usuwania z konkretnych tabel
        private void btnKlientDel_Click(object sender, RoutedEventArgs e)
        {
            Container.Content = new DeleteKlient(connection);
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
