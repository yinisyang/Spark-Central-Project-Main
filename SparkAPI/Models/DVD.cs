using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class DVD : Modellable
    {
        public int item_id { get; set; }
        public string title { get; set; }
        public int release_year { get; set; }
        public string rating { get; set; }

        public Tuple<SqlDbType, int> GetAssociatedDBTypeAndSize(string propertyName)
        {
            switch (propertyName)
            {
                case "item_id": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "title": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "release_year": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "rating": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                default: throw new ArgumentException("That property name is not registered inside of the current model type");
            }
        }

        public string[] FieldsNotSpecifiedInPOST()
        {
            return new string[] { "item_id" };
        }
    }
}