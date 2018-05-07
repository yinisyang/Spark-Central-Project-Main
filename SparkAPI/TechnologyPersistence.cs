using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;

namespace SparkAPI
{
    public class TechnologyPersistence : BasePersistence
    {
        public TechnologyPersistence() : base("Technology") { }

        protected override Modellable RetrieveNextItem(SqlDataReader reader)
        {
            Technology t = new Technology();
            t.item_id = reader.GetInt32(reader.GetOrdinal("item_id"));
            t.name = reader.GetString(reader.GetOrdinal("name"));

            return t;
        }
    }
}