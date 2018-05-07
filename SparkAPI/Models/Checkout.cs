using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Checkout : Modellable
    {
        public int item_id { get; set; }
        public int member_id { get; set; }
        public String item_type { get; set; }
        public DateTime due_date { get; set; }
        public DateTime checkout_date { get; set; }
        public bool resolved { get; set; }

        public Tuple<SqlDbType, int> GetAssociatedDBTypeAndSize(string propertyName)
        {
            switch (propertyName)
            {
                case "item_id": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "member_id": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "item_type": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "due_date": return new Tuple<SqlDbType, int>(SqlDbType.Date, 3);
                case "checkout_date": return new Tuple<SqlDbType, int>(SqlDbType.Date, 3);
                case "resolved": return new Tuple<SqlDbType, int>(SqlDbType.Bit, 1);
                default: throw new ArgumentException("That property name is not registered inside of the current model type");
            }
        }

        public string[] FieldsNotSpecifiedInPOST()
        {
            return new string[] {};
        }
    }
}