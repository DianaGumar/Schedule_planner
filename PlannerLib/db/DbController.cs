using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Text;

namespace PlannerLibrary.db
{
    class DbController<T> : AbstractController, IOperations<T> where T : class, new()
    {

        internal DbController()
        {
            //todo вынести в параметры строку подключения

            string host = "localhost"; // Имя хоста
            string database = "planner"; // Имя базы данных
            string user = "root"; // Имя пользователя
            string password = "1111"; // Пароль пользователя

            connStr = "Server = " + host + "; Database = " + database +
                "; Uid = " + user + "; Pwd = " + password + ";";


            //GetConnection();
        }

        public int Add(T obj)
        {
            return 0;
        }

        public int Add(Model.User obj)
        {

            int countRowsUffected = 0;
            //for user only
            // todo заменить в будущем на пулл открытых подключений без using
            using ( MySqlConnection conn = GetConnection())
            {


                conn.Open();

                //id int primary key auto_increment not null,
                //name nvarchar(50) not null,
                //password nvarchar(50) not null,
                //email nvarchar(50),
                //phone nvarchar(13),
                //role int not null


                string sql;


                sql = string.Format("insert Into users (name, password, email, phone, role)" +
                " value('" + obj.name + "','" + obj.password + "','" + obj.email + "','" + obj.phone + "'," + obj.role + ")");


                MySqlCommand command = new MySqlCommand(sql, conn);
                countRowsUffected = command.ExecuteNonQuery(); 


                conn.Close();
            }

            return countRowsUffected;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Reed(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> Reed(out string[] columnsName)
        {

            throw new NotImplementedException();
        }

        public ICollection<T> Reed()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }

   

}
