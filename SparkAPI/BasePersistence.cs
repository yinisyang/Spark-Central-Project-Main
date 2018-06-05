using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using SparkAPI.Models;
using System.Data;

namespace SparkAPI
{
    public abstract class BasePersistence
    {
        protected SqlConnection con;
        protected String tableName = null;
        Dictionary<String, Tuple<String, SqlDbType, int>> args;

        public BasePersistence(String tableName) {
            if (string.IsNullOrEmpty(tableName)) throw new NullReferenceException("A table associated with this persistence class must be specified");

            try
            {
                this.tableName = tableName;
                args = new Dictionary<string, Tuple<string, SqlDbType, int>>();
                this.con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/SparkAPI").ConnectionStrings.ConnectionStrings["SparkCentralConnectionString"].ConnectionString);
                con.Open();
            }
            catch (SqlException ex)
            {

            }
        }

        // In the GetAll function the number of arguments and order is unknown at runtime so the datatype needs
        // to be specified for each argument. In all other methods all parameters are required so they are known
        // beforehand. Thus a type doesn't need to passed in.

        // Dictionary<argument_name, Tuple<argument_value, sql_database_type, size>>
        public ArrayList GetAll() {
            String sqlString = "SELECT * FROM " + this.tableName;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if (args.Keys.Count != 0)
            {
                sqlString += " WHERE ";
            }

            foreach (String key in args.Keys)
            {
                sqlString += key + " = @" + key + " AND ";
                SqlParameter param = new SqlParameter("@" + key, args[key].Item2, args[key].Item3);
                param.Value = args[key].Item1;
                cmd.Parameters.Add(param);
            }

            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";

            cmd.CommandText = sqlString;
            cmd.Prepare();
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList list = new ArrayList();

            while (reader.Read())
            {
                list.Add(RetrieveNextItem(reader));
            }

            con.Close();
            clearFields();

            return list;
        }
        public Modellable Get()
        {
            String sqlString = "SELECT * FROM " + tableName + " WHERE ";
            SqlCommand cmd = new SqlCommand(sqlString, con);

            foreach (String key in args.Keys)
            {
                sqlString += key + " = @" + key + " AND ";
                SqlParameter param = new SqlParameter("@" + key, args[key].Item2, args[key].Item3);
                param.Value = args[key].Item1;
                cmd.Parameters.Add(param);
            }

            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";

            cmd.CommandText = sqlString;
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                clearFields();
                return RetrieveNextItem(reader);
            }
            clearFields();

            return null;
        }

        public bool Delete() {
            String sqlString = "SELECT * FROM " + tableName + " WHERE ";
            SqlCommand cmd = new SqlCommand(sqlString, con);

            foreach (String key in args.Keys)
            {
                sqlString += key + " = @" + key + " AND ";
                SqlParameter param = new SqlParameter("@" + key, args[key].Item2, args[key].Item3);
                param.Value = args[key].Item1;
                cmd.Parameters.Add(param);
            }

            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";
            cmd.CommandText = sqlString;
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                //I utilized new variables rather than reusing the previous ones due to errors caused if I don't
                String sqlString2 = "DELETE FROM " + tableName + " WHERE ";
                SqlCommand delcmd = new SqlCommand(sqlString2, con);

                foreach (String key in args.Keys)
                {
                    sqlString2 += key + " = @" + key + " AND ";
                    SqlParameter param = new SqlParameter("@" + key, args[key].Item2, args[key].Item3);
                    param.Value = args[key].Item1;
                    delcmd.Parameters.Add(param);
                }

                if (sqlString2.Substring(sqlString2.Length - 4).Equals("AND "))
                    sqlString2 = sqlString2.Substring(0, sqlString2.Length - 4);

                sqlString2 += ";";
                delcmd.CommandText = sqlString2;
                delcmd.Prepare();

                delcmd.Prepare();

                delcmd.ExecuteNonQuery();
                clearFields();
                return true;
            }
            con.Close();
            clearFields();
            return false;
        }

        public int Save(Modellable item, String keyToReturn) {
            string[] keyNames = item.FieldsNotSpecifiedInPOST();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            String sqlString = "INSERT INTO " + this.tableName + " (";

            int cur = 1;
            var properties = item.GetType().GetProperties();

            foreach (var prop in properties)
            {
                if (!keyNames.Contains(prop.Name)){
                    sqlString += prop.Name;
                    if (cur != properties.Count())
                    {
                        sqlString += ",";
                    }
                    
                }
                cur++;
            }
            sqlString += ") OUTPUT INSERTED." + keyToReturn + " VALUES (";

            cur = 1;
            foreach(var prop in properties)
            {
                if (!keyNames.Contains(prop.Name))
                {
                    sqlString += "@" + prop.Name;
                    if (cur != properties.Count())
                    {
                        sqlString += ",";
                    }
                    

                    Tuple<SqlDbType, int> DBTypeAndSize = item.GetAssociatedDBTypeAndSize(prop.Name);
                    SqlParameter param = new SqlParameter("@" + prop.Name, DBTypeAndSize.Item1, DBTypeAndSize.Item2);
                    param.Value = (object)prop.GetValue(item, null) ?? DBNull.Value;
                    cmd.Parameters.Add(param);
                }
                cur++;
            }
            sqlString += ");";

            cmd.CommandText = sqlString;

            try
            {
                cmd.Prepare();
                int id = (int)cmd.ExecuteScalar();
                con.Close();
                return id;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public bool Update(Modellable item)
        {
            String sqlString = "SELECT * FROM " + tableName + " WHERE ";
            SqlCommand cmd = new SqlCommand(sqlString, con);

            foreach (String key in args.Keys)
            {
                sqlString += key + " = @" + key + " AND ";
                SqlParameter param = new SqlParameter("@" + key, args[key].Item2, args[key].Item3);
                param.Value = args[key].Item1;
                cmd.Parameters.Add(param);
            }

            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";
            cmd.CommandText = sqlString;
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                reader.Close();
                String[] keyNames = item.FieldsNotSpecifiedInPOST();
                String updateSqlString = "UPDATE " + tableName + " SET ";
                SqlCommand upCmd = new SqlCommand();
                upCmd.Connection = con;

                int cur = 1;
                var properties = item.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    if (!keyNames.Contains(prop.Name))
                    {
                        updateSqlString += prop.Name + "=@" + prop.Name;
                        if (cur != properties.Count())
                        {
                            updateSqlString += ",";
                        }

                        Tuple<SqlDbType, int> DBTypeAndSize = item.GetAssociatedDBTypeAndSize(prop.Name);
                        SqlParameter param = new SqlParameter("@" + prop.Name, DBTypeAndSize.Item1, DBTypeAndSize.Item2);
                        param.Value = (object)prop.GetValue(item, null) ?? DBNull.Value;
                        upCmd.Parameters.Add(param);
                    }
                    cur++;
                }
                updateSqlString += " WHERE ";
                foreach (String key in args.Keys)
                {
                    updateSqlString += key + " = @" + key + " AND ";
                    SqlParameter param = new SqlParameter("@" + key, args[key].Item2, args[key].Item3);
                    param.Value = args[key].Item1;
                    upCmd.Parameters.Add(param);
                }

                if (updateSqlString.Substring(updateSqlString.Length - 4).Equals("AND "))
                    updateSqlString = updateSqlString.Substring(0, updateSqlString.Length - 4);

                updateSqlString += ";";
                upCmd.CommandText = updateSqlString;

                try
                {
                    upCmd.Prepare();

                    upCmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        // Fields should be added to a call before an API call is executed inside of the controller method
        // that is making the call
        public void addCallField(String fieldName, String fieldValue, SqlDbType dbType, int size)
        {
            if (fieldValue != null) { args.Add(fieldName, new Tuple<string, SqlDbType, int>(fieldValue, dbType, size)); }
        }

        public void addCallField(String fieldName, int? fieldValue, SqlDbType dbType, int size)
        {
            if(fieldValue != null) addCallField(fieldName, fieldValue.ToString(), dbType, size);
        }

        public void addCallField(String fieldName, bool? fieldValue, SqlDbType dbType, int size)
        {
            if(fieldValue != null) addCallField(fieldName, fieldValue.ToString(), dbType, size);
        }

        private void clearFields() { this.args.Clear(); }

        // abstract methods
        protected abstract Modellable RetrieveNextItem(SqlDataReader reader);
    }
}