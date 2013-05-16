using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Database_Content_Sincronisation
{

    public class MyDatabase
    {
        private string _database, _server, _User, _Password;
        private int _counter = 0;
        private DataTable[] _Tables;
        private SqlConnection _connection;
        private SqlConnectionStringBuilder _builder;
        private SqlCommand _cmd;
        private SqlDataReader _reader;

        public MyDatabase(string database, string server, string user, string password)
        {
            _database = database;
            _server = server;
            _User = user;
            _Password = password;

            _builder = new SqlConnectionStringBuilder();

            _builder.DataSource = _server;
            _builder.InitialCatalog = _database;
            _builder.UserID = _User;
            _builder.Password = _Password;

            _connection = new SqlConnection();
            _connection.ConnectionString = _builder.ConnectionString;

            _cmd = new SqlCommand();
            _cmd.CommandText = "USE " + _database + " select sch.name + '.' + tbls.name from sys.tables tbls inner join sys.schemas sch on tbls.schema_id = sch.schema_id order by tbls.name";
            _cmd.Connection = _connection;
            _connection.Open();
            _reader = _cmd.ExecuteReader();
            DataTable tbl = new DataTable();
            tbl.Load(_reader);
            _connection.Close();
            DataRowCollection rows = tbl.Rows;
            _Tables = new DataTable[tbl.Rows.Count];
        }


        #region Properties

        public string Database
        {
            get { return _database; }
            //set { _database = value; }
        }

        public string Server
        {
            get { return _server; }
            //set { _server = value; }
        }

        public string User
        {
            get { return _User; }
            //set { _User = value; }
        }

        public string Password
        {
            get { return _Password; }
            //set { _Password = value; }
        }

        public SqlConnection ConnectionOfDb
        {
            get { return _connection; }
        }

        public DataTable[] Tables
        {
            get { return _Tables; }
            set { _Tables = value; }
        }

        public int NumberOfTables
        {
            get { return _counter; }
        }

        #endregion


        public string[] GetTableNames()
        {
            string[] tablenames = new string[_Tables.Count()];
            int i = 0;
            foreach (DataTable tbl in _Tables)
            {
                tablenames[i] = tbl.TableName;
                i++;
            }
            return tablenames;
        }

        public DataTable GetTable(int index)
        {
                return _Tables[index];
        }

        public DataColumn GetColumn(DataTable table, int columnIndex)
        {
            return table.Columns[columnIndex];
        }

        public void Fill(string TableName)
        {
            foreach (DataTable t in _Tables)
            {
                if ( t!= null && t.TableName == TableName)
                {
                    return;
                }
            }

            _cmd = new SqlCommand();
            _cmd.CommandText = "SELECT * FROM " + TableName;
            _cmd.Connection = _connection;
            _connection.Open();
            _reader = _cmd.ExecuteReader();
            _Tables[_counter] = new DataTable();

            this._Tables[_counter].Load(_reader);
            _Tables[_counter].TableName = TableName;
            _reader.Close();
            _connection.Close();
            _cmd.Connection.Close();

            _cmd.CommandText = "SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(Constraint_Catalog + '.' + Constraint_Schema + '.' + constraint_name), 'IsPrimaryKey') = 1 AND table_name = '" + TableName.Substring(TableName.IndexOf('.') + 1) + " ORDER BY column_name'";
            _connection.Open();
            _reader = _cmd.ExecuteReader();
            DataTable tbl = new DataTable();
            tbl.Load(_reader);
            _connection.Close();
            DataRowCollection rows = tbl.Rows;
            DataColumn[] cols = new DataColumn[tbl.Rows.Count];
            int i = 0;
            string s = "";
            foreach (DataRow r in rows)
            {
                s = r[0].ToString();
                cols[i] = _Tables[_counter].Columns[s];
                i++;
            }
            _Tables[_counter].PrimaryKey = cols;
            _counter++;
        }

        public void Fill()
        {
                _cmd = new SqlCommand();
                DataTable tableNames = new DataTable();
                DataTable tablePrimaryKeys = new DataTable();
                string tblname = "";

                _cmd.CommandText = "USE " + _database + " select sch.name + '.' + tbls.name from sys.tables tbls inner join sys.schemas sch on tbls.schema_id = sch.schema_id order by tbls.name";
                _cmd.Connection = _connection;
                _connection.Open();
                _reader = _cmd.ExecuteReader();

                tableNames.Load(_reader);
                _connection.Close();
                DataRowCollection rows = tableNames.Rows;
                tableNames.Dispose();
                _counter = 0;
                _connection.Open();
                foreach (DataRow table in rows)
                {
                    /*Populate tables */
                    _cmd.CommandText = "SELECT * FROM " + table.ItemArray[0].ToString();
                    _reader = _cmd.ExecuteReader();
                    DataTable tableValues = new DataTable();
                    tableValues.Load(_reader);
                    _Tables[_counter] = tableValues;
                    tblname = table.ItemArray[0].ToString().Substring(table.ItemArray[0].ToString().IndexOf('.') + 1);
                    _Tables[_counter].TableName = tblname;
                    /* End population of tables */

                    /*Set primary key foreach table*/

                    _cmd.CommandText = "SELECT column_name FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE OBJECTPROPERTY(OBJECT_ID(Constraint_Catalog + '.' + Constraint_Schema + '.' + constraint_name), 'IsPrimaryKey') = 1 AND table_name = '" + tblname + "'";
                    _reader = _cmd.ExecuteReader();
                    tablePrimaryKeys.Load(_reader);
                    DataRowCollection PrimaryKeys = tablePrimaryKeys.Rows;
                    DataColumn[] cols = new DataColumn[tablePrimaryKeys.Rows.Count];
                    int j = 0;
                    string s = "";

                    foreach (DataRow r in PrimaryKeys)
                    {
                        s = r[0].ToString();
                        cols[j] = _Tables[_counter].Columns[s];
                        j++;
                    }
                    _Tables[_counter].PrimaryKey = cols;

                    /*End setting primary key foreach table*/

                    _counter++;
                }
                _connection.Close();
        }

        //public void UpdateTable(DataTable TableSource, DataTable TableDestination)
        //{
        //    string UpdateComand = "UPDATE " + TableSource.TableName +" SET "
        //    _cmd = new SqlCommand();

        //}
    }
}
