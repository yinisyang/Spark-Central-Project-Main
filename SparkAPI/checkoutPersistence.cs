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
        public ArrayList getCheckouts()
        {
            String sqlString = "SELECT * FROM Item_Checkout;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList checkoutArray = new ArrayList();

            while(reader.Read())
            {
                Checkout c = new Checkout();
                c.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                c.MemberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                c.ItemType = reader.GetString(reader.GetOrdinal("item_type"));
                c.dueDate = reader.GetDateTime(reader.GetOrdinal("due_date"));
                checkoutArray.Add(c);
            }

            conn.Close();
            return checkoutArray;
        }
        public ArrayList getCheckouts(int member_id)
        {
            String sqlString = "SELECT * FROM ITEM_CHECKOUT WHERE member_id = " + member_id.ToString() + ";";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList checkoutArray = new ArrayList();

            while(reader.Read())
            {
                Checkout c = new Checkout();
                c.ItemId = reader.GetInt32(reader.GetOrdinal("item_id"));
                c.MemberId = reader.GetInt32(reader.GetOrdinal("member_id"));
                c.ItemType = reader.GetString(reader.GetOrdinal("item_type"));
                c.dueDate = reader.GetDateTime(reader.GetOrdinal("due_date"));

                checkoutArray.Add(c);
            }
            conn.Close();
            return checkoutArray;
        }
        public int saveCheckout(Checkout checkoutToSave)
        {
            String sqlString = "INSERT INTO ITEM_CHECKOUT (item_id, member_id, item_type, due_date) OUTPUT INSERTED.item_id VALUES(@item_id, @member_id, @item_type, @due_date)";
            SqlParameter itemParam = new SqlParameter("@item_id", System.Data.SqlDbType.Int, 4);
            SqlParameter memberParam = new SqlParameter("@member_id", System.Data.SqlDbType.Int, 4);
            SqlParameter ItemTypeParam = new SqlParameter("@item_type", System.Data.SqlDbType.VarChar, 50);
            SqlParameter dueDateParam = new SqlParameter("@due_date", System.Data.SqlDbType.Date, 3);

            itemParam.Value = checkoutToSave.ItemId;
            memberParam.Value = checkoutToSave.MemberId;
            ItemTypeParam.Value = checkoutToSave.ItemType;
            dueDateParam.Value = checkoutToSave.dueDate;

            SqlCommand cmd = new SqlCommand(sqlString, conn);
            cmd.Parameters.Add(itemParam);
            cmd.Parameters.Add(memberParam);
            cmd.Parameters.Add(ItemTypeParam);
            cmd.Parameters.Add(dueDateParam);

            cmd.Prepare();
            //cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            conn.Close();
            return id;
        }
    }
}
