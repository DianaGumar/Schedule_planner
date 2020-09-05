using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;
using MySql.Data.MySqlClient;

namespace PlannerLib.DataBase
{

    public class MysqlController<T> : AbstractMySqlController<T, int> where T : class, new()
    {


        public MysqlController()
        {
            connection = GetConnection("Server=localhost;Database=planner;Uid=root;Pwd=1111;");


            Entrails = GetEntrailsTypesNames();
            Name = typeof(T).Name;
        }

        public MysqlController(string connstr)
        {
            connection = GetConnection(connstr);

            Entrails = GetEntrailsTypesNames();
            Name = typeof(T).Name;

        }

        MySqlConnection connection;

        private string Name;
        List<string>[] Entrails;

        /// <summary>
        /// reflection for jeneric class
        /// </summary>
        /// <returns></returns>
        private List<string>[] GetEntrailsTypesNames()
        {
            List<string>[] strs = new List<string>[2] { new List<string>(), new List<string>() };

            Type type = typeof(T);

            var Entrails = type.GetProperties();

            foreach (PropertyInfo propertiesInfo in Entrails)
            {
                strs[0].Add(propertiesInfo.PropertyType.Name);
                strs[1].Add(propertiesInfo.Name);
            }

            return strs;
        }

        /// <summary>
        /// reflection for jeneric class
        /// </summary>
        /// <returns></returns>
        private List<object> GetEntrailsValues(T obj)
        {
            List<object> strs = new List<object>();
            Type type = typeof(T);

            var Entrails = type.GetProperties();

            foreach (PropertyInfo propertyInfo in Entrails)
            {
                strs.Add(propertyInfo.GetValue(obj));
            }

            return strs;
        }

        /// <summary>
        /// set fields 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private T ReflectionWork(object[] obj)
        {
            T t = new T();

            Type type = typeof(T);
            var propertys = type.GetProperties();

            int i = 0;
            foreach (PropertyInfo propertyInfo in propertys)
            {
                if (obj[i] == null || obj[i] is System.DBNull)
                { }
                else
                {
                    propertyInfo.SetValue(t, obj[i]);
                }
                
                i++;
            }

            return t;
        }


        /// <summary>
        /// reed all objects in table
        /// </summary>
        /// <param name="columnsNames"></param>
        /// <returns>list by type T</returns>
        public IList<T> Reed()
        {
            List<T> entity = new List<T>();
            string[] columnsNames = null;

            string sql = "select * from " + Name + "s";

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //take columns names
                    columnsNames = new string[reader.FieldCount];

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columnsNames[i] = reader.GetName(i);
                    }


                    //take data
                    while (reader.Read())
                    {
                        object[] inside = new object[reader.FieldCount];
                        reader.GetValues(inside);
                        //возвращает объект T, todo
                        //полученный из массива значений Object
                        T t = ReflectionWork(inside);
                        entity.Add(t);

                    }

                    //reader.NextResult();
                }
                reader.Close();

            }
            catch (Exception e)
            {
                throw new Exception("Connect to bd exeption: " + e.Message);
            }
            finally { connection.Close(); }

            return entity;
        }

        public override T Reed(int id)
        {
            T entity = new T();

            string sql = "select * from " + Name + "s where " + Entrails[1][0] + "=" + id;

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    object[] inside = new object[reader.FieldCount];
                    reader.GetValues(inside);
                    entity = ReflectionWork(inside);

                }
                reader.Close();

            }
            catch (Exception e)
            {
                throw new Exception("Connect to bd exeption: " + e.Message);
            }
            finally { connection.Close(); }

            return entity;
        }

        //T obj будет как out
        public T Reed(T obj, params string[] lables)
        {
            //select * from planner.users where planner.users.email = "lantan.mp4@gmail.com" and planner.users.password = "123"

            T entity = new T();

            List<object> values = GetEntrailsValues(obj);
            StringBuilder sb = new StringBuilder();

            foreach(string lable in lables)
            {
                for(int i = 0; i < Entrails[1].Count; i ++)
                {
                    if (Entrails[1][i].ToLower().Equals(lable.ToLower()))
                    {
                        if (i > 0 && sb.Length > 0) sb.Append(" and ");
                        sb.Append(lable.ToLower());
                        sb.Append("='");
                        sb.Append(values[i]);
                        sb.Append("'");
                        
                        break;
                    }
                }
            }

            string sql = "select * from " + Name + "s where " + sb.ToString();

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                MySqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    object[] inside = new object[reader.FieldCount];
                    reader.GetValues(inside);
                    entity = ReflectionWork(inside);

                }
                reader.Close();

            }
            catch (Exception e)
            {
                throw new Exception("Connect to bd exeption: " + e.Message);
            }
            finally { connection.Close(); }

            return entity;
        }


        public override int Create(T obj)
        {
            //create sql query
            List<object> values = GetEntrailsValues(obj);

            StringBuilder sb = new StringBuilder();
            sb.Append("insert into " + Name + "s (");
            int count = Entrails[0].Count;
            string prefix = "";
            for (int i = 1; i < count; i++)
            {

                if (Entrails[0][i].Equals("DateTime"))
                {
                    if (values[i].ToString() == DateTime.MinValue.ToString()) { values[i] = null; }
                    else
                    {
                        values[i] = ((DateTime)values[i]).ToString("yyyy-MM-dd hh-mm-ss");
                    }
                }


                if (values[i] != null)
                {
                    if (values[i].ToString() != "")
                    {
                        sb.Append(prefix);
                        prefix = ",";
                        sb.Append(Entrails[1][i]);
                    }    
                } 
            }
            sb.Append(") values( ");
            prefix = "";
            for (int i = 1; i < count; i++)
            {
                
                if(values[i] != null)
                {
                    if (values[i].ToString() != "")
                    {
                        sb.Append(prefix);
                        prefix = ",";
                        sb.Append("'" + values[i] + "'");
                    }
                }
            }
            sb.Append(" )");

            string sql = sb.ToString();

            int countRowsUffected = 0;

            //----------------------------------

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                countRowsUffected = command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception("Connect to bd exeption: " + e.Message);
            }
            finally { connection.Close(); }

            return countRowsUffected;
        }

        public override int Delete(int id)
        {
            string sql = "delete from " + Name + "s where " + Entrails[1][0] + "=" + id;
            int countRowsUffected = 0;

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                countRowsUffected = command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception("Connect to bd exeption: " + e.Message);
            }
            finally { connection.Close(); }

            return countRowsUffected;
        }

        public override int Update(T obj)
        {
            //create sql query
            List<object> values = GetEntrailsValues(obj);

            StringBuilder sb = new StringBuilder();
            sb.Append("update " + Name + "s set ");
            int count = Entrails[0].Count;
            for (int i = 1; i < count - 1; i++)
            {
                if (Entrails[0][i].Equals("DateTime"))
                {
                    values[i] = ((DateTime)values[i]).ToString("yyyy-MM-dd");
                }
                sb.Append(Entrails[1][i] + "= '" + values[i] + "', ");
            }
            if (Entrails[0][count - 1].Equals("DateTime"))
            {
                values[count - 1] = ((DateTime)values[count - 1]).ToString("yyyy-MM-dd");
            }
            sb.Append(Entrails[1][count - 1] + "= '" + values[count - 1] + "'");
            sb.Append(" where " + Entrails[1][0] + "= " + values[0]);

            string sql = sb.ToString();
            // update comission.entrants set EntrantName = 'Igor', ScoreDiploma = 8, Student = 0 
            //where EntrantID = 6;
            int countRowsUffected = 0;

            //----------------------------------

            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand(sql, connection);
                countRowsUffected = command.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                throw new Exception("Connect to bd exeption: " + e.Message);
            }
            finally { connection.Close(); }

            return countRowsUffected;
        }

    }

}
