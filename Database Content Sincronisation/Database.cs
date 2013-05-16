using System;

public class MyDatabase
{
    private string _sqlconnection, _database, _server, _User, _Password ;

    public MyDatabase(string database, string server, string user, string password)
    {
        _database = database;
        _server = server;
        _User = user;
        _Password = password;
    }

    public MyDatabase(string connectionstring)
    {
        _sqlconnection = connectionstring;
    }

    #region Properties

    string Database
    {
        get { return _database; }
        set { _database = value; }
    }

    string Server
    {
        get { return _server; }
        set { _server = value; }
    }

    string User
    {
        get { return _User; }
        set { _user = value; }
    }

    string Password
    {
        get { return _Password; }
        set { _Password = value; }
    }

    string ConnectionString
    {
        get { return _connectionstring; }
        set { _connectionstring = value; }
    }

    #endregion


}
