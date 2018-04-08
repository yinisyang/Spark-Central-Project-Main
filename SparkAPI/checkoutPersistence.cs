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
            throw new NotImplementedException();
        }
    }
}
