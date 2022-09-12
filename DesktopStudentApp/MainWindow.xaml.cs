using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopStudentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTable();
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        public void LoadTable()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("select * from Student", con);
                DataTable dt = new DataTable();
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                dt.Load(sdr);
                con.Close();
                Dgrd.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool isValid()
        {
            if (TxtName.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtSurname.Text == string.Empty)
            {
                MessageBox.Show("Surname is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtIndex.Text == string.Empty)
            {
                MessageBox.Show("Index Number is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (TxtIndex.Text.Length != 6)
            {
                MessageBox.Show("Index Number has not 6 numbers.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtDataUr.Text == string.Empty)
            {
                MessageBox.Show("Data urodzenia is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    using (SqlCommand dataCommand = con.CreateCommand())
                    {
                        SqlCommand cmd = new SqlCommand("INSERT INTO Student (Name, Surname, [Index], DateOfBirth) VALUES (@Name, @Surname, @NrIndex, @StudDataTime)", con);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@Name", TxtName.Text);
                        cmd.Parameters.AddWithValue("@Surname", TxtSurname.Text);
                        cmd.Parameters.AddWithValue("@NrIndex", TxtIndex.Text);
                        cmd.Parameters.AddWithValue("@StudDataTime", TxtDataUr.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                        LoadTable();
                        MessageBox.Show("Success", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
          
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadTable();
        }

        private void TextBox_TextChangedName(object sender, TextChangedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand("select * from Student where Name like '" + txt_SearchName.Text + "%'", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            Dgrd.ItemsSource = dt.DefaultView;   
        }

        private void TextBox_TextChangedSurname(object sender, TextChangedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student where Surname like '" + txt_SearchSurname.Text + "%'", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            Dgrd.ItemsSource = dt.DefaultView;
            con.Close();
        }
        // public ObservableCollection<MStudent> MStudents { get; set; }

        private void Delete_btn(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteWindow delwin = new DeleteWindow();
                delwin.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Edit_btn(object sender, RoutedEventArgs e)
        {
            EditWindow editwin = new EditWindow();
            editwin.Show();
        }

        private void Add_Grades_btn(object sender, RoutedEventArgs e)
        {
            AddGradesWindow addgrwin = new AddGradesWindow();
            addgrwin.Show();
        }

        private void TextBox_TextChangedIndex(object sender, TextChangedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student where [Index] like '" + txt_SearchIndex.Text + "%'", con);
            SqlDataReader ser = cmd.ExecuteReader();
            DataTable dt = new DataTable();

            dt.Load(ser);
            con.Close();
            Dgrd.ItemsSource = dt.DefaultView;
        }

        private void TextBox_TextChangedDataOfBirth(object sender, TextChangedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from Student where DateOfBirth like '" + txt_SearchDataOfBirth.Text + "%'", con);
            DataTable dt = new DataTable();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            Dgrd.ItemsSource = dt.DefaultView;
            con.Close();
        }
    }
}
 