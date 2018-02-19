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

        public ArrayList getDVDS()
        {
            String sqlString = "SELECT * FROM DVDS;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
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
            return dvdArray;
        }

        public DVD getDVD(int id)
        {
            String sqlString = "SELECT * FROM DVDS WHERE item_id = " + id.ToString() + ";";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                DVD toReturn = new DVD();
                toReturn.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                toReturn.Title = reader.GetString(reader.GetOrdinal("title"));
                toReturn.Rating = reader.GetString(reader.GetOrdinal("rating"));
                toReturn.ReleaseYear = reader.GetInt32(reader.GetOrdinal("release_year"));

                return toReturn;
            }
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
            cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            return id;
        }
    }
}