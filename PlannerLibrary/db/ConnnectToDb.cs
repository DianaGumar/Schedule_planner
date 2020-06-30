using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace PlannerLibrary.db
{
    abstract class ConnnectToDb
    {

        SqlConnection connection;

        SqlConnection GetConnection()
        {
            if(connection != null)
            {
                return connection;
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        void Connect(string connectStr) { connection = new SqlConnection(connectStr); }

        void UnConnect() 
        {
            connection.Close();
            connection.Dispose();
            connection = null;
        }


    }
}
