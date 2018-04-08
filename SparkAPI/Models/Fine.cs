using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Fine : Modellable
    {
        public int fine_id { get; set; }
        public int member_id { get; set; }
        public double amount { get; set; }
        public String description { get; set; }

        public Tuple<SqlDbType, int> GetAssociatedDBTypeAndSize(string propertyName)
        {
            switch (propertyName)
            {
                case "fine_id": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "member_id": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "amount": return new Tuple<SqlDbType, int>(SqlDbType.Float, 8);
                case "description": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                default: throw new ArgumentException("That property name is not registered inside of the current model type");
            }
        }

        public string[] GetKeyNames()
        {
            return new string[] { "fine_id" };
        }
    }
}