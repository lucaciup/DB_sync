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
            //AdventureWorks.Fill("HumanResources.Department");
            //AdventureWorks.Fill();

            //int db1 = AdventureWorks.Tables[0].GetHashCode();

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
            Binding b = new Binding("DatabaseTableNames");
            b.Source = AdventureWorks;
            listBox_AllTables.SetBinding(ListBox.ItemsSourceProperty, b);
            listBox_AllTables.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", System.ComponentModel.ListSortDirection.Ascending));
            this.button_SelectItem.Click += new RoutedEventHandler(this.button_SelectItem_OnClick);
            this.button_SelectAllItems.Click += new RoutedEventHandler(button_SelectAllItems_Click);
            this.button_DeselectAll.Click += new RoutedEventHandler(button_DeselectAll_Click);
            //b.ElementName = AdventureWorks2.Tables[0].TableName;
            //var a = AdventureWorks.GetTableNames().AsEnumerable();
            //listBox_AllTables.ItemsSource = AdventureWorks.GetTableNames().AsEnumerable();
            //test2 = test.Clone();
            //test2.Rows[0][1] = "test2";
            //ad.Update(test);
            //test.AcceptChanges();
 
            //if (db1 != db22)
            //    MessageBox.Show("different");
            //else
            //    MessageBox.Show("The same");

            //var stackPanel = new StackPanel { Orientation = Orientation.Vertical };
            //stackPanel.Children.Add(new Label { Content = "Source database", HorizontalAlignment = HorizontalAlignment.Left });
            ////stackPanel.Children.Add(new Button { Content = "Button" });
            //foreach (DataTable t in AdventureWorks2.Tables)
            //{
            //    stackPanel.Children.Add(new CheckBox { Name = t.TableName, Content = t.TableName });
            //}
            //this.Content = stackPanel;

        }

        void button_DeselectAll_Click(object sender, RoutedEventArgs e)
        {
            if (!listBox_SelectedTables.Items.IsEmpty)
            {
                listBox_SelectedTables.Items.Clear();
            }
        }

        void button_SelectAllItems_Click(object sender, RoutedEventArgs e)
        {
            if (!listBox_AllTables.Items.IsEmpty)
            {
                listBox_SelectedTables.Items.Clear();
                foreach (object o in listBox_AllTables.Items)
                    listBox_SelectedTables.Items.Add(o);
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void button_DeselectItem_Click(object sender, RoutedEventArgs e)
        {
            if (!listBox_SelectedTables.Items.IsEmpty)
            {
                listBox_SelectedTables.Items.RemoveAt(listBox_SelectedTables.Items.Count - 1);
            }
        }

        private void button_SelectItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (!listBox_AllTables.Items.IsEmpty && !listBox_SelectedTables.Items.Contains(listBox_AllTables.SelectedItem))
            {
                listBox_SelectedTables.Items.Add(listBox_AllTables.SelectedItem);
            }
        }

    }
}
