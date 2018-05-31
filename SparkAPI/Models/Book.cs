using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace SparkAPI.Models
{
    public class Book : Modellable
    {
        public int item_id { get; set; }
        public int assn { get; set; }
        public string author { get; set; }
        public string isbn_10 { get; set; }
        public string category { get; set; }
        public string publisher { get; set; }
        public int publication_year { get; set; }
        public int pages { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public string isbn_13 { get; set; }
        public string image { get; set; }


        public Tuple<SqlDbType, int> GetAssociatedDBTypeAndSize(string propertyName)
        {
            switch (propertyName)
            {
                case "item_id": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "assn": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "author": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "isbn_10": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "category": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "publisher": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "publication_year": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "pages": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "description": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 300);
                case "title": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "isbn_13": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "image": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                default: throw new ArgumentException("That property name is not registered inside of the current model type");
            }
        }

        public string[] FieldsNotSpecifiedInPOST()
        {
            return new string[] {"item_id"};
        }
    }
}