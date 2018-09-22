using MssqlClient.Classes.Views;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MssqlClient.Classes.Beans
{
    class SqlHelper : IDisposable
    {
        SqlConnection _cn;
        public void Dispose()
        {
            if (_cn != null)
            {
                _cn.Dispose();
                _cn = null;
            }
        }
        public SqlHelper(string connectionString)
        {
            _cn = new SqlConnection(connectionString);
        }
        public bool IsConnection()
        {
            try
            {
                if (_cn.State == ConnectionState.Closed)
                {
                    _cn.Open();
                }
                return true;
            }
            catch (Exception ex)
            {
                FrmMain.Log.Error("Cannot connect to database", ex);
                return false;
            }
        }
    }
}
