using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database_Content_Sincronisation
{
    class MyRow
    {
        private object _ID, _value;
        private string _ColumnName;

        public MyRow(object ID, string ColumnName, object value)
        {
            _ID = ID;
            _ColumnName = ColumnName;
            _value = value;
        }


        public object ID
        {
            get { return _ID; }
        }

        public string ColumnName
        {
            get { return _ColumnName; }
        }

        public object Value
        {
            get { return _value; }
        }
    }
}
