using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Collections;
using SparkAPI.Models;

namespace SparkAPI
{
    public abstract class BasePersistence
    {
        protected SqlConnection con;
        public BasePersistence() {
            try
            {
                con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/SparkAPI").ConnectionStrings.ConnectionStrings["SparkCentralConnectionString"].ConnectionString);
                con.Open();
            }
            catch (SqlException ex)
            {

            }
        }

        // In the GetALl function the number of arguments and order is unknown at runtime so the datatype needs
        // to be specified for each argument. In all other methods all parameters are required so they are known
        // beforehand. Thus a type doesn't need to passed in.

        // Dictionary<argument_name, Tuple<argument_value, sql_database_type, size>>
        public abstract ArrayList GetAll(Dictionary<String, Tuple<String, System.Data.SqlDbType, int>> args);
        public abstract Modellable Get(Dictionary<String, String> args);
        public abstract bool Delete(Dictionary<String, String> args);
        public abstract int Save(Modellable item);
        public abstract bool Update(Dictionary<String, String> args, Modellable item);

        public void GetAllPrepare(string sqlString, Dictionary<String, Tuple<String, System.Data.SqlDbType, int>> args, SqlCommand cmd)
        {
            if (args.Keys.Count != 0)
            {
                sqlString += "WHERE ";
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
        }
    }
}