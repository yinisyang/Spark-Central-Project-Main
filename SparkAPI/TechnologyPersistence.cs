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
            return techArray;
        }

        public Technology getTechnology(int id) {
            String sqlString = "SELECT * FROM Technology WHERE item_id = " + id.ToString() + ";";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Technology toReturn = new Technology();
                toReturn.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                toReturn.Name = reader.GetString(reader.GetOrdinal("name"));

                return toReturn;
            }
            return null;
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
            return id;
        }
    }
}