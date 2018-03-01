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
        public ArrayList getFines(int? member_id)
        {
            String sqlString = "SELECT * FROM FINES ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            if(member_id != null)
            {
                sqlString += "WHERE member_id = @member_id";
                SqlParameter memberIdParam = new SqlParameter("@member_id", System.Data.SqlDbType.Int, 4);
                memberIdParam.Value = member_id;
                cmd.Parameters.Add(memberIdParam);
            }
            sqlString += ";";

            cmd.CommandText = sqlString;
            cmd.Prepare();
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList finesArray = new ArrayList();

            while (reader.Read())
            {
                Fine f = new Fine();
                f.fineId = reader.GetInt32(reader.GetOrdinal("fine_id"));
                f.memberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                f.amount = reader.GetFloat(reader.GetOrdinal("amount"));
                f.description = reader.GetString(reader.GetOrdinal("description"));
                finesArray.Add(f);
            }
            conn.Close();
            return finesArray;
        }
        public Fine getFine(int fine_id)
        {
            String sqlString = "SELECT * FROM FINES WHERE fine_id = @fine_id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@fine_id", System.Data.SqlDbType.Int, 4);
            idParam.Value = fine_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Fine f = new Fine();
                f.fineId = reader.GetInt32(reader.GetOrdinal("fine_id"));
                f.memberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                f.amount = reader.GetFloat(reader.GetOrdinal("amount"));
                f.description = reader.GetString(reader.GetOrdinal("description"));

                conn.Close();
                return f;
            }
            conn.Close();
            return null;
        }
        public int saveFine(Fine fineToSave)
        {
            String sqlString = "INSERT INTO FINES (member_id, amount, description) OUTPUT INSERTED.member_id VALUES(@member_id, @amount, @description)";
            SqlParameter memberIdParam = new SqlParameter("@member_id", System.Data.SqlDbType.Int, 4);
            SqlParameter amountParam = new SqlParameter("@amount", System.Data.SqlDbType.Decimal, 8);
            SqlParameter descriptionParam = new SqlParameter("@description", System.Data.SqlDbType.VarChar, 300);

            memberIdParam.Value = fineToSave.memberId;
            amountParam.Value = fineToSave.amount;
            descriptionParam.Value = fineToSave.description;

            SqlCommand cmd = new SqlCommand(sqlString, conn);
            cmd.Parameters.Add(memberIdParam);
            cmd.Parameters.Add(amountParam);
            cmd.Parameters.Add(descriptionParam);

            cmd.Prepare();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            return id;
        }
        public bool deleteFine(int fine_id)
        {
            String sqlString = "SELECT * FROM FINES WHERE fine_id = @fine_id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@fine_id", System.Data.SqlDbType.Int, 4);
            idParam.Value = fine_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                //Iutilized new variables rather than reusing the previous ones due to errors caused if I don't
                String sqlString2 = "DELETE FROM FINES WHERE fine_id = @id2;";
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
        public bool updateFine(int fine_id, Fine fineToSave)
        {
            String sqlString = "SELECT * FROM FINES WHERE fine_id = @fine_id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@fine_id", System.Data.SqlDbType.Int, 4);
            idParam.Value = fine_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                //Iutilized new variables rather than reusing the previous ones due to errors caused if I don't
                String sqlString3 = "UPDATE FINES SET amount=@amount, description=@description, member_id=@member_id WHERE fine_id=@fine_id";
                SqlCommand upcmd = new SqlCommand(sqlString3, conn);

                SqlParameter idParam2 = new SqlParameter("@amount", System.Data.SqlDbType.Float, 8);
                SqlParameter idParam3 = new SqlParameter("@description", System.Data.SqlDbType.VarChar, 300);
                SqlParameter idParam4 = new SqlParameter("@member_id", System.Data.SqlDbType.Int, 4);
                SqlParameter idParam5 = new SqlParameter("@fine_id", System.Data.SqlDbType.Int, 4);

                idParam2.Value = fineToSave.amount;
                idParam3.Value = fineToSave.description;
                idParam4.Value = fineToSave.memberId;
                idParam5.Value = fineToSave.fineId;

                upcmd.Parameters.Add(idParam2);
                upcmd.Parameters.Add(idParam3);
                upcmd.Parameters.Add(idParam4);
                upcmd.Parameters.Add(idParam5);

                upcmd.Prepare();

                upcmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}