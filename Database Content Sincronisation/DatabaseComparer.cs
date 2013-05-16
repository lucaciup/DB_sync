using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Database_Content_Sincronisation
{
    class DatabaseComparer
    {
        MyDatabase _database;

        //public bool MyCompare(MyDatabase firstDatabase, MyDatabase secondDatabase)
        //{

        //    if (firstDatabase.Tables.GetHashCode() == secondDatabase.Tables.GetHashCode())
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        for (int i = 0; i < firstDatabase.Tables.Count(); i++)
        //        {
        //            if (firstDatabase.Tables[i].GetHashCode() == secondDatabase.Tables[i].GetHashCode())
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                MyCompare(firstDatabase.Tables[i], secondDatabase.Tables[i]);
        //            }
        //        }
        //        return false;
        //    }
        //}

        public DataTable ContentThatNeedsToBeSync(DataTable Sourcetable, DataTable Destinationtable)
        {
            DataTable result = new DataTable();

           // if (Sourcetable.Rows.Count < Destinationtable.Rows.Count)
            {
            //    var name = from r in MyTable
            //where r.ID == 0
            //select r.Name;
                int[] asshole = new int[10];
                //asshole.Contains(
                DataRowCollection col = Destinationtable.Rows;
                //List<DataColumn> Destpk = Destinationtable.PrimaryKey.ToList<DataColumn>();
                var ssdds = Destinationtable.PrimaryKey[0][0];
                object dd = ssdds;
                DataColumnCollection Destpk = Destinationtable.PrimaryKey.Cast<DataColumnCollection>();
                bool IsPrimaryKeyTheSame = true;
                //Destinationtable.PrimaryKey.Contains(
                DataTable mytbl = new DataTable();
                //Destinationtable.Rows.Find(
                foreach (DataColumn c in Destpk)
                for (int i = 0; i < Destinationtable.PrimaryKey.Count(); i++)
                {
                    if (Destinationtable.PrimaryKey[i].GetHashCode() != Sourcetable.PrimaryKey[i].GetHashCode())
                    {
                        IsPrimaryKeyTheSame = false;
                    }
                }
                var aresult = Destinationtable.PrimaryKey;
            }
            Sourcetable.Merge(Destinationtable);
            return Sourcetable.GetChanges();

            //List<MyRow> r = new List<MyRow>();
            //for (int i = 0; i < table1.Rows.Count; i++)
            //{
            //    if (table1.Rows[i].GetHashCode() != table2.Rows[i].GetHashCode())
            //    {
            //        //table2.Rows[i] = Sincronize(table1.Rows[i], table2.Rows[i]);
            //        foreach (object value in Sincronize(table1.Rows[i], table2.Rows[i]))
            //        {
            //            r.Add(new MyRow(table1.Rows[i].ItemArray[0], table2.Columns[0].ToString(), value));
            //        }
            //    }
            //}
        }

        public List<object> Sincronize(DataRow row1, DataRow row2)
        {
            List<object> ListOfValues = new List<object>();
            for (int i = 0; i < row1.ItemArray.Count(); i++)
            {
                if (row1.ItemArray[i].GetHashCode() != row2.ItemArray[i].GetHashCode())
                {
                    ListOfValues.Add(row1.ItemArray[i]);
                }
            }
            return ListOfValues;

        }
    }
}
