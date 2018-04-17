using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;

namespace SparkAPI
{
    public class FinesPersistence : BasePersistence
    {
        public FinesPersistence() : base("Fines") { }

        protected override Modellable RetrieveNextItem(SqlDataReader reader)
        {
            Fine f = new Fine();
            f.amount = reader.GetDouble(reader.GetOrdinal("amount"));
            f.description = reader.GetString(reader.GetOrdinal("description"));
            f.fine_id = reader.GetInt32(reader.GetOrdinal("fine_id"));
            f.member_id = reader.GetInt32(reader.GetOrdinal("member_id"));

            return f;
        }
    }
}