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
            throw new NotImplementedException();
        }
    }
}