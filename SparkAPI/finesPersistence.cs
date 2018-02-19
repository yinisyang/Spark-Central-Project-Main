using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;

namespace SparkAPI
{
    public class FinesPersistence
    {
        private SqlConnection conn;
        public FinesPersistence()
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
        public ArrayList getFines()
        {
            String sqlString = "SELECT * FROM FINES;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList finesArray = new ArrayList();

            while (reader.Read())
            {
                Fine f = new Fine();
                f.memberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                f.amount = reader.GetDouble(reader.GetOrdinal("amount"));
                finesArray.Add(f);
            }
            return finesArray;
        }
        public Fine getFine(int member_id)
        {
            String sqlString = "SELECT * FROM FINES WHERE member_id = " + member_id.ToString() + ";";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                Fine f = new Fine();
                f.memberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                f.amount = reader.GetDouble(reader.GetOrdinal("amount"));

                return f;
            }
            return null;
        }
        public int saveFine(Fine fineToSave)
        {
            String sqlString = "INSERT INTO FINES (amount) OUTPUT INSERTED.member_id VALUES(@amount)";
            SqlParameter amountParam = new SqlParameter("@amount", System.Data.SqlDbType.Float, 8);

            amountParam.Value = fineToSave.amount;

            SqlCommand cmd = new SqlCommand(sqlString, conn);
            cmd.Parameters.Add(amountParam);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            return id;
        }
    }
}