using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;

namespace SparkAPI
{
    public class DVDPersistence
    {
        private SqlConnection conn;

        public DVDPersistence()
        {
            try
            {
                conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/SparkAPI").ConnectionStrings.ConnectionStrings["SparkCentralConnectionString"].ConnectionString);
                conn.Open();
            }
            catch (SqlException ex)
            {

            }
        }

        public ArrayList getDVDS(string title, int? releaseYear, string rating)
        {
            String sqlString = "SELECT * FROM DVDS ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            if (title != null || releaseYear != null || rating != null) {
                sqlString += "WHERE ";
                if (title != null)
                {
                    sqlString += "title = @title AND ";
                    SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.VarChar, 50);
                    titleParam.Value = title;
                    cmd.Parameters.Add(titleParam);
                }
                if (releaseYear != null)
                {
                    sqlString += "release_year = @release_year AND ";
                    SqlParameter releaseYearParam = new SqlParameter("@release_year", System.Data.SqlDbType.Int, 4);
                    releaseYearParam.Value = releaseYear;
                    cmd.Parameters.Add(releaseYearParam);
                }
                if (rating != null)
                {
                    sqlString += "rating = @rating";
                    SqlParameter ratingParam = new SqlParameter("@rating", System.Data.SqlDbType.VarChar, 50);
                    ratingParam.Value = rating;
                    cmd.Parameters.Add(ratingParam);
                }
            }
            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";
            
            cmd.CommandText = sqlString;
            cmd.Prepare();
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList dvdArray = new ArrayList();

            while (reader.Read())
            {
                DVD d = new DVD();
                d.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                d.Title = reader.GetString(reader.GetOrdinal("title"));
                d.Rating = reader.GetString(reader.GetOrdinal("rating"));
                d.ReleaseYear = reader.GetInt32(reader.GetOrdinal("release_year"));
                dvdArray.Add(d);
            }
            conn.Close();
            return dvdArray;
        }

        public DVD getDVD(int id)
        {
            String sqlString = "SELECT * FROM DVDS WHERE item_id = @id;";
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
                DVD toReturn = new DVD();
                toReturn.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                toReturn.Title = reader.GetString(reader.GetOrdinal("title"));
                toReturn.Rating = reader.GetString(reader.GetOrdinal("rating"));
                toReturn.ReleaseYear = reader.GetInt32(reader.GetOrdinal("release_year"));

                conn.Close();
                return toReturn;
            }
            conn.Close();
            return null;
        }

        public int saveDVD(DVD dvdToSave)
        {
            String sqlString = "INSERT INTO DVDS (title, rating, release_year) OUTPUT INSERTED.item_id VALUES (@title, @rating, @release_year)";
            SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.VarChar, 50);
            SqlParameter ratingParam = new SqlParameter("@rating", System.Data.SqlDbType.VarChar, 10);
            SqlParameter releaseYearParam = new SqlParameter("@release_year", System.Data.SqlDbType.Int, 4);

            titleParam.Value = dvdToSave.Title;
            ratingParam.Value = dvdToSave.Rating;
            releaseYearParam.Value = dvdToSave.ReleaseYear;

            SqlCommand cmd = new SqlCommand(sqlString, conn);
            cmd.Parameters.Add(titleParam);
            cmd.Parameters.Add(ratingParam);
            cmd.Parameters.Add(releaseYearParam);

            cmd.Prepare();
            //cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            return id;
        }

        public bool deleteDVD(int id)
        {
            String sqlString = "SELECT * FROM DVDS WHERE item_id = @id;";
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
                String sqlString2 = "DELETE FROM DVDS WHERE item_id = " + "@id2" + ";";
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
    }
}