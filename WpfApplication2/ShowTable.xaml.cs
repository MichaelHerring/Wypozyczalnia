﻿using System;
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
using System.Data;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Tables.xaml
    /// </summary>
    public partial class ShowTable : Page
    {
        SqlConnection connection;
        string tableName;

        public delegate void WyslijInfo(string komunikat);
        public static event WyslijInfo wyslaneInfo;

        public ShowTable()
        {
            InitializeComponent();
        }

        public ShowTable(SqlConnection conn, string x)
        {
            InitializeComponent();
            this.connection = conn;
            this.tableName = x;
        }

        SqlCommand command;
        SqlDataAdapter adapter;
        DataTable table;
        string query;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            query = $"select * from {tableName}";
            command = new SqlCommand(query, connection);
            adapter = new SqlDataAdapter(command);
            table = new DataTable();

            try
            {
                adapter.Fill(table);
                DataGr.ItemsSource = table.DefaultView;
            }
            catch(Exception exc)
            {
                wyslaneInfo(exc.Message);
            }
        }
    }
}
