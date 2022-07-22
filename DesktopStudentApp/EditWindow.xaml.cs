using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;

namespace DesktopStudentApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        public EditWindow()
        {
            InitializeComponent();
        }

        public bool isValid()
        {
            if (TxtEditName.Text == string.Empty)
            {
                MessageBox.Show("Name is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtEditSurname.Text == string.Empty)
            {
                MessageBox.Show("Surname is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtEditIndex.Text == string.Empty)
            {
                MessageBox.Show("Index Number is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (TxtEditIndex.Text.Length != 6)
            {
                MessageBox.Show("Index Number has not 6 numbers.", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TxtEditDataUr.Text == string.Empty)
            {
                MessageBox.Show("Data urodzenia is required", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectString"].ConnectionString);
        int answer;
        int guess;
        public int Answer
        {
            get { return answer; }
            set { answer = value; }
        }
        public int Guess
        {
            get { return guess; }
            set { guess = value; }
        }

        private void SaveEdit(object sender, RoutedEventArgs e)
        {
            if (IDNameTxt.Text != string.Empty)
            {
                if (isValid())
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Student set Name = '" + TxtEditName.Text + "',Surname = '" + TxtEditSurname.Text + "',[Index] = '" + TxtEditIndex.Text + "', DateOfBirth = '" + TxtEditDataUr.Text + "'where Id = '" + IDNameTxt.Text + "' ", con);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        con.Close();
                        this.Close();
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show("You have got problem with sql connection." + ex.Message, "Database connection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    finally
                    {
                        con.Close();
                        this.Close();
                    }
                }
            }
            else MessageBox.Show("You have not entered ID, First of all write down ID of student.", "No ID", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //NameTxt.Document.Blocks.Add(new Paragraph(new Run("Text")));
            //NameTxt.Text = "asdasd";
        }

        private void Cancel_btn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
