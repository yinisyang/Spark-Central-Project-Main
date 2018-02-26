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
            conn.Close();
            return finesArray;
        }
        public Fine getFine(int member_id)
        {
            String sqlString = "SELECT * FROM FINES WHERE member_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = member_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Fine f = new Fine();
                f.memberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                f.amount = reader.GetDouble(reader.GetOrdinal("amount"));

                conn.Close();
                return f;
            }
            conn.Close();
            return null;
        }
        public int saveFine(Fine fineToSave)
        {
            String sqlString = "INSERT INTO FINES (member_id, amount) OUTPUT INSERTED.member_id VALUES(@membeR_id, @amount)";
            SqlParameter amountParam = new SqlParameter("@amount", System.Data.SqlDbType.Float, 8);
            SqlParameter memberIdParam = new SqlParameter("@member_id", System.Data.SqlDbType.VarChar, 50);

            amountParam.Value = fineToSave.amount;
            memberIdParam.Value = fineToSave.memberId;

            SqlCommand cmd = new SqlCommand(sqlString, conn);
            cmd.Parameters.Add(amountParam);
            cmd.Parameters.Add(memberIdParam);

            cmd.Prepare();
            //cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            return id;
        }
        public bool deleteFine(int member_id)
        {
            String sqlString = "SELECT * FROM FINES WHERE member_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = member_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                //Iutilized new variables rather than reusing the previous ones due to errors caused if I don't
                String sqlString2 = "DELETE FROM FINES WHERE member_id = " + "@id2" + ";";
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