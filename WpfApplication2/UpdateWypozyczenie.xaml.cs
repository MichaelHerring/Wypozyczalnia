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

        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
