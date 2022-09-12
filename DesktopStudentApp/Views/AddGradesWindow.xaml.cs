using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace DesktopStudentApp
{
    /// <summary>
    /// Interaction logic for AddGradesWindow.xaml
    /// </summary>
    public partial class AddGradesWindow : Window
    {
        public AddGradesWindow()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);

        private void SaveGrade_btn(object sender, RoutedEventArgs e)
        {
            if (TxtEntId.Text == string.Empty)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Replace(Grades, NULL, '2') from Student where id = " + TxtEntId.Text, con);
                try
                {
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show("You have got problem with inserted data.", "Database data insert Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else MessageBox.Show("You have entered ID that is not existed or just empty TextBox.", "Wrong ID", MessageBoxButton.OK, MessageBoxImage.Warning);

            this.Close();
        }
    }
}
