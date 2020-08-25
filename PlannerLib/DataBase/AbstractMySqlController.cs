using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLib.DataBase
{
    public abstract class AbstractMySqlController<E, K> : IController<E, K>
    {

        public abstract E Reed(K id);

        public abstract int Create(E obj);

        public abstract int Delete(K id);

        public abstract int Update(E obj);

        public static MySqlConnection GetConnection(string connstr)
        {

            MySqlConnection connection = new MySqlConnection(connstr);

            return connection;
        }

        

    }

}

