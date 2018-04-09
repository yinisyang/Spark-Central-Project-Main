using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;

namespace SparkAPI
{
    public class CheckoutPersistence : BasePersistence
    {
        public CheckoutPersistence() : base("Item_Checkout") { }

        protected override Modellable RetrieveNextItem(SqlDataReader reader)
        {
            Checkout c = new Checkout();
            c.item_id = reader.GetInt32(reader.GetOrdinal("item_id"));
            c.item_type = reader.GetString(reader.GetOrdinal("item_type"));
            c.member_id = reader.GetInt32(reader.GetOrdinal("membeR_id"));
            c.resolved = reader.GetBoolean(reader.GetOrdinal("resolved"));

            return c;
        }
    }
}
