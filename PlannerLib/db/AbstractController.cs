using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;

namespace PlannerLibrary.db
{
    //todo возможность работы с любой бд
    abstract class AbstractController
    {

        MySqlConnection connection;
        protected String connStr;


        private MySqlConnection Connect(string connectStr) { return new MySqlConnection(connectStr); }


        protected MySqlConnection GetConnection(string connectStr)
        {
            if (connection == null)
            {
                connection = Connect(connectStr);
            }

            return connection;
        }

        protected MySqlConnection GetConnection()
        {
            if (connStr != null || connStr != "")
            {
                return GetConnection(connStr);
            }

            throw new NullReferenceException();
          
        }

        

    }
}
