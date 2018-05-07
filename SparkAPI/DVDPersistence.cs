using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;

namespace SparkAPI
{
    public class DVDPersistence : BasePersistence
    {
        public DVDPersistence() : base("DVDS") { }

        protected override Modellable RetrieveNextItem(SqlDataReader reader)
        {
            DVD item = new DVD();

            item.item_id = reader.GetInt32(reader.GetOrdinal("item_id"));
            item.rating = reader.GetString(reader.GetOrdinal("rating"));
            item.release_year = reader.GetInt32(reader.GetOrdinal("release_year"));
            item.title = reader.GetString(reader.GetOrdinal("title"));

            return item;
        }
    }
}