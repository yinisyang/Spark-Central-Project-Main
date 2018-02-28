using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;

namespace SparkAPI
{
    public class CheckoutPersistence
    {
        private SqlConnection conn;
        public CheckoutPersistence()
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
        public ArrayList getCheckouts(int? item_id, int? member_id, string item_type, bool? resolved)
        {
            String sqlString = "SELECT * FROM Item_Checkout ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;

            if(item_id != null || member_id != null || item_type != null)
            {
                sqlString += "WHERE ";
                if(item_id != null)
                {
                    sqlString += "item_id = @item_id AND ";
                    SqlParameter itemIDParam = new SqlParameter("@item_id", System.Data.SqlDbType.Int, 4);
                    itemIDParam.Value = item_id;
                    cmd.Parameters.Add(itemIDParam);
                }
                if(member_id != null)
                {
                    sqlString += "member_id = @member_id AND ";
                    SqlParameter memberIDParam = new SqlParameter("@member_id", System.Data.SqlDbType.Int, 4);
                    memberIDParam.Value = member_id;
                    cmd.Parameters.Add(memberIDParam);
                }
                if(item_type != null)
                {
                    sqlString += "item_type = @item_type AND ";
                    SqlParameter itemTypeParam = new SqlParameter("@item_type", System.Data.SqlDbType.VarChar, 50);
                    itemTypeParam.Value = item_type;
                    cmd.Parameters.Add(itemTypeParam);
                }
                if(resolved != null)
                {
                    sqlString += "resolved = @resolved";
                    SqlParameter resolvedParam = new SqlParameter("@resolved", System.Data.SqlDbType.Bit, 1);
                    resolvedParam.Value = resolved;
                    cmd.Parameters.Add(resolvedParam);
                }
            }
            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";

            cmd.CommandText = sqlString;
            cmd.Prepare();
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList checkoutArray = new ArrayList();

            while(reader.Read())
            {
                Checkout c = new Checkout();
                c.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                c.MemberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                c.ItemType = reader.GetString(reader.GetOrdinal("item_type"));
                c.dueDate = reader.GetDateTime(reader.GetOrdinal("due_date"));
                c.resolved = reader.GetBoolean(reader.GetOrdinal("resolved"));
                checkoutArray.Add(c);
            }

            conn.Close();
            return checkoutArray;
        }
        public ArrayList getCheckouts(int member_id)
        {
            String sqlString = "SELECT * FROM ITEM_CHECKOUT WHERE member_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = member_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList checkoutArray = new ArrayList();

            while(reader.Read())
            {
                Checkout c = new Checkout();
                c.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                c.MemberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                c.ItemType = reader.GetString(reader.GetOrdinal("item_type"));
                c.dueDate = reader.GetDateTime(reader.GetOrdinal("due_date"));
                c.resolved = reader.GetBoolean(reader.GetOrdinal("resolved"));

                checkoutArray.Add(c);
            }
            conn.Close();
            return checkoutArray;
        }
        public int saveCheckout(Checkout checkoutToSave)
        {
            String sqlString = "INSERT INTO ITEM_CHECKOUT (item_id, member_id, item_type, due_date, resolved) OUTPUT INSERTED.item_id VALUES(@item_id, @member_id, @item_type, @due_date, @resolved)";
            SqlParameter itemParam = new SqlParameter("@item_id", System.Data.SqlDbType.Int, 4);
            SqlParameter memberParam = new SqlParameter("@member_id", System.Data.SqlDbType.Int, 4);
            SqlParameter ItemTypeParam = new SqlParameter("@item_type", System.Data.SqlDbType.VarChar, 50);
            SqlParameter dueDateParam = new SqlParameter("@due_date", System.Data.SqlDbType.Date, 3);
            SqlParameter resolvedParam = new SqlParameter("@resolved", System.Data.SqlDbType.Bit, 1);

            itemParam.Value = checkoutToSave.ItemId;
            memberParam.Value = checkoutToSave.MemberId;
            ItemTypeParam.Value = checkoutToSave.ItemType;
            dueDateParam.Value = checkoutToSave.dueDate;
            resolvedParam.Value = checkoutToSave.resolved;

            SqlCommand cmd = new SqlCommand(sqlString, conn);
            cmd.Parameters.Add(itemParam);
            cmd.Parameters.Add(memberParam);
            cmd.Parameters.Add(ItemTypeParam);
            cmd.Parameters.Add(dueDateParam);
            cmd.Parameters.Add(resolvedParam);

            cmd.Prepare();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            return id;
        }
        public bool deleteCheckout(int item_id, int member_id, String item_type)
        {
            String sqlString = "SELECT * FROM ITEM_CHECKOUT WHERE item_id = @it_id AND member_id = @mem_id AND item_type = @it_type;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter it_idParam = new SqlParameter("@it_id", System.Data.SqlDbType.Int, 4);
            SqlParameter mem_idParam = new SqlParameter("@mem_id", System.Data.SqlDbType.Int, 4);
            SqlParameter it_typeParam = new SqlParameter("@it_type", System.Data.SqlDbType.VarChar, 50);

            it_idParam.Value = item_id;
            mem_idParam.Value = member_id;
            it_typeParam.Value = item_type;

            cmd.Parameters.Add(it_idParam);
            cmd.Parameters.Add(mem_idParam);
            cmd.Parameters.Add(it_typeParam);

            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                String sqlString2 = "SELECT * FROM ITEM_CHECKOUT WHERE item_id = @it_id2 AND member_id = @mem_id2 AND item_type = @it_type2;";
                SqlCommand delcmd = new SqlCommand(sqlString2, conn);

                //sql parameters for protection
                SqlParameter it_idParam2 = new SqlParameter("@it_id2", System.Data.SqlDbType.Int, 4);
                SqlParameter mem_idParam2 = new SqlParameter("@mem_id2", System.Data.SqlDbType.Int, 4);
                SqlParameter it_typeParam2 = new SqlParameter("@it_type2", System.Data.SqlDbType.VarChar, 50);

                it_idParam2.Value = it_idParam.Value;
                mem_idParam2.Value = mem_idParam.Value;
                it_typeParam2.Value = it_typeParam.Value;

                delcmd.Parameters.Add(it_idParam2);
                delcmd.Parameters.Add(mem_idParam2);
                delcmd.Parameters.Add(it_typeParam2);

                delcmd.Prepare();

                delcmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool updateCheckout(int item_id, int member_id, String item_type, Checkout checkoutToSave)
        {
            String sqlString = "SELECT * FROM ITEM_CHECKOUT WHERE item_id = @it_id AND member_id = @mem_id AND item_type = @it_type;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);

            //sql parameters for protection
            SqlParameter it_idParam = new SqlParameter("@it_id", System.Data.SqlDbType.Int, 4);
            SqlParameter mem_idParam = new SqlParameter("@mem_id", System.Data.SqlDbType.Int, 4);
            SqlParameter it_typeParam = new SqlParameter("@it_type", System.Data.SqlDbType.VarChar, 50);

            it_idParam.Value = item_id;
            mem_idParam.Value = member_id;
            it_typeParam.Value = item_type;

            cmd.Parameters.Add(it_idParam);
            cmd.Parameters.Add(mem_idParam);
            cmd.Parameters.Add(it_typeParam);

            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                String sqlString2 = "UPDATE ITEM_CHECKOUT SET due_date='" + checkoutToSave.dueDate.ToString("") + "' WHERE item_id = " + item_id.ToString() + " AND member_id = " + member_id.ToString() + " AND item_type = " + item_type;
                SqlCommand upcmd = new SqlCommand(sqlString2, conn);

                //sql parameters for protection
                SqlParameter it_idParam2 = new SqlParameter("@it_id2", System.Data.SqlDbType.Int, 4);
                SqlParameter mem_idParam2 = new SqlParameter("@mem_id2", System.Data.SqlDbType.Int, 4);
                SqlParameter it_typeParam2 = new SqlParameter("@it_type2", System.Data.SqlDbType.VarChar, 50);

                it_idParam2.Value = it_idParam.Value;
                mem_idParam2.Value = mem_idParam.Value;
                it_typeParam2.Value = it_typeParam.Value;

                upcmd.Parameters.Add(it_idParam2);
                upcmd.Parameters.Add(mem_idParam2);
                upcmd.Parameters.Add(it_typeParam2);

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
