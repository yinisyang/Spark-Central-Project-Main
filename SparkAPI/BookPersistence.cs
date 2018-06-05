using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;
using System.Data.SqlClient;


namespace SparkAPI
{
    public class BookPersistence : BasePersistence
    {
        public BookPersistence() : base("Books") { }

        protected override Modellable RetrieveNextItem(SqlDataReader reader)
        {
            Book b = new Book();
            b.item_id = reader.GetInt32(reader.GetOrdinal("item_id"));
            b.assn = reader.GetInt32(reader.GetOrdinal("assn"));
            b.author = reader.GetString(reader.GetOrdinal("author"));
            b.isbn_10 = reader.GetString(reader.GetOrdinal("isbn_10"));
            b.category = reader.GetString(reader.GetOrdinal("category"));
            b.publisher = reader.GetString(reader.GetOrdinal("publisher"));
            b.publication_year = reader.GetInt32(reader.GetOrdinal("publication_year"));
            b.pages = reader.GetInt32(reader.GetOrdinal("pages"));


            if (reader["description"] != DBNull.Value)
            {
                b.description = reader.GetString(reader.GetOrdinal("description"));
            }
            else
            {
                b.description = null;
            }

            b.title = reader.GetString(reader.GetOrdinal("title"));
            b.isbn_13 = reader.GetString(reader.GetOrdinal("isbn_13"));
            
            return b;
        }
    }
}