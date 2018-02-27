using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;

namespace SparkAPI
{
    public class TechnologyPersistence
    {
        private SqlConnection conn;

        public TechnologyPersistence() {
            try
            {
                conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/SparkAPI").ConnectionStrings.ConnectionStrings["SparkCentralConnectionString"].ConnectionString);
                conn.Open();
            }
            catch(SqlException ex)
            {

            }
        }

        public ArrayList getTechnology() {
            String sqlString = "SELECT * FROM Technology;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList techArray = new ArrayList();

            while(reader.Read())
            {
                Technology t = new Technology();
                t.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                t.Name = reader.GetString(reader.GetOrdinal("name"));
                techArray.Add(t);
            }
            conn.Close();
            return techArray;
        }

        public Technology getTechnology(int id) {
            String sqlString = "SELECT * FROM Technology WHERE item_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Technology toReturn = new Technology();
                toReturn.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                toReturn.Name = reader.GetString(reader.GetOrdinal("name"));

                conn.Close();
                return toReturn;
            }
            conn.Close();
            return null;
        }

        public bool deleteTechnology(int id)
        {
            String sqlString = "SELECT * FROM Technology WHERE item_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                //Iutilized new variables rather than reusing the previous ones due to errors caused if I don't
                String sqlString2 = "DELETE FROM Technology WHERE item_id = @id2;";
                SqlCommand delcmd = new SqlCommand(sqlString2, conn);

                SqlParameter idParam2 = new SqlParameter("@id2", System.Data.SqlDbType.Int, 4);
                idParam2.Value = idParam.Value;
                delcmd.Parameters.Add(idParam2);

                delcmd.Prepare();

                delcmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }

        public int saveTechnology(Technology techToSave)
        {
            String sqlString = "INSERT INTO Technology (name) OUTPUT INSERTED.item_id VALUES (@name);";

            SqlParameter nameParam = new SqlParameter("@name", System.Data.SqlDbType.VarChar, 50);
            nameParam.Value = techToSave.Name;

            SqlCommand cmd = new SqlCommand(sqlString, conn);
            cmd.Parameters.Add(nameParam);

            cmd.Prepare();
            //cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            return id;
        }
    }
}