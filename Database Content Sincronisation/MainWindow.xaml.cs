using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Database_Content_Sincronisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            myne();
        }

        public void myne()
        {
            string tbl = "HumanResources.Department";
            MyDatabase AdventureWorks = new MyDatabase("AdventureWorks", @"dan-pc\acerextensa", "sa", "sa");
            AdventureWorks.Fill("HumanResources.Department");
            //AdventureWorks.Fill();
            dataGrid1.ItemsSource = new DataTable().AsDataView();

            int db1 = AdventureWorks.Tables[0].GetHashCode();

            MyDatabase AdventureWorks2 = new MyDatabase("AdventureWorks2", @"dan-pc\acerextensa", "sa", "sa");

            AdventureWorks2.Fill("HumanResources.Department");

            int db22 = AdventureWorks2.Tables[0].GetHashCode();
            DatabaseComparer cmp = new DatabaseComparer();
            DataTable mytbl = new DataTable();
            DataTable mytbl2 = new DataTable();

            mytbl = AdventureWorks.Tables[0];
            mytbl2 = AdventureWorks2.Tables[0];

            //cmp.ContentThatNeedsToBeSync(mytbl, mytbl2);

            DataTable treitb = new DataTable();
            //treitb = mytbl.DefaultView.ToTable();
            //dataGrid1.AutoGenerateColumns = true;
            //dataGrid1.ItemsSource = mytbl.AsDataView();
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = @"dan-pc\acerextensa";
            builder.InitialCatalog = "AdventureWorks";
            builder.UserID = "sa";
            builder.Password = "sa";
            SqlConnection con = new SqlConnection(builder.ConnectionString);

            SqlDataAdapter ad = new SqlDataAdapter("SELECT * FROM HumanResources.Department",con);

            SqlCommandBuilder com = new SqlCommandBuilder(ad);
            DataTable test = new DataTable();
            ad.Fill(test);
//            test.Columns[1].ReadOnly = false;
            DataTable test2 = new DataTable();
            //test2 = test.Clone();
            //test2.Rows[0][1] = "test2";
            //ad.Update(test);
            //test.AcceptChanges();
 
            //if (db1 != db22)
            //    MessageBox.Show("different");
            //else
            //    MessageBox.Show("The same");

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {

        }

    }
}
