using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;

namespace SparkAPI
{
    public class MemberPersistence : BasePersistence
    {
        public MemberPersistence() : base("Members") { }

        protected override Modellable RetrieveNextItem(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }
    }
}