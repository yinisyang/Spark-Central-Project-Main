using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SparkAPI.Models;
using System.Collections;
using System.Data.SqlClient;


namespace SparkAPI
{
    public class BookPersistence
    {
        private SqlConnection con;
        public BookPersistence()
        {
            try
            {
                con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/SparkAPI").ConnectionStrings.ConnectionStrings["SparkCentralConnectionString"].ConnectionString);
                con.Open();
            }
            catch (SqlException ex)
            {

            }
        }

        public ArrayList GetBooks()
        {
            String sqlString = "SELECT * FROM Books;";
            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList bookList = new ArrayList();

            while(reader.Read())
            {
                Book b = new Book();
                b.Id = reader.GetInt32(reader.GetOrdinal("item_id"));
                b.Author = reader.GetString(reader.GetOrdinal("author"));
                b.Isbn10 = reader.GetString(reader.GetOrdinal("isbn_10"));
                b.Category = reader.GetString(reader.GetOrdinal("category"));
                b.Publisher = reader.GetString(reader.GetOrdinal("publisher"));
                b.Year = reader.GetInt32(reader.GetOrdinal("publication_year"));
                b.Pages = reader.GetInt32(reader.GetOrdinal("pages"));


                if (reader["description"] != DBNull.Value)
                {
                    b.Description = reader.GetString(reader.GetOrdinal("description"));
                }
                else
                {
                    b.Description = null;
                }

                b.Title = reader.GetString(reader.GetOrdinal("title"));
                b.Isbn13 = reader.GetString(reader.GetOrdinal("isbn_13"));

                bookList.Add(b);
            }
            return bookList;
        }

        public Book GetBook(int id)
        {
            String sqlString = "SELECT * FROM Books WHERE item_id = " + id.ToString() + ";";

            SqlCommand cmd = new SqlCommand(sqlString, con);
            SqlDataReader reader = cmd.ExecuteReader();

            if(reader.Read())
            {
                Book ret = new Book();
                ret.Id = reader.GetInt32(reader.GetOrdinal("item_id"));
                ret.Author = reader.GetString(reader.GetOrdinal("author"));
                ret.Isbn10 = reader.GetString(reader.GetOrdinal("isbn_10"));
                ret.Category = reader.GetString(reader.GetOrdinal("category"));
                ret.Publisher = reader.GetString(reader.GetOrdinal("publisher"));
                ret.Year = reader.GetInt32(reader.GetOrdinal("publication_year"));
                ret.Pages = reader.GetInt32(reader.GetOrdinal("pages"));

                if (reader["description"] != DBNull.Value)
                {
                    ret.Description = reader.GetString(reader.GetOrdinal("description"));
                }
                else
                {
                    ret.Description = null;
                }
                ret.Title = reader.GetString(reader.GetOrdinal("title"));
                ret.Isbn13 = reader.GetString(reader.GetOrdinal("isbn_13"));

                return ret;
            }
            return null;
        }

        public int SaveBook(Book b)
        {
            String sqlString = "INSERT INTO Books (author, isbn_10, category, publisher, publication_year, pages, description, title, isbn_13) OUTPUT INSERTED.item_id VALUES (@author, @isbn_10, @category, @publisher, @publication_year, @pages, @description, @title, @isbn_13)";
            SqlParameter authorParam = new SqlParameter("@author", System.Data.SqlDbType.VarChar, 50);
            SqlParameter isbn10Param = new SqlParameter("@isbn_10", System.Data.SqlDbType.VarChar, 20);
            SqlParameter categoryParam = new SqlParameter("@category", System.Data.SqlDbType.VarChar, 50);
            SqlParameter publisherParam = new SqlParameter("@publisher", System.Data.SqlDbType.VarChar, 50);
            SqlParameter publicationYearParam = new SqlParameter("@publication_year", System.Data.SqlDbType.Int, 4);
            SqlParameter pagesParam = new SqlParameter("@pages", System.Data.SqlDbType.Int, 4);
            SqlParameter descriptionParam = new SqlParameter("@description", System.Data.SqlDbType.VarChar, 500);
            SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.VarChar, 50);
            SqlParameter isbn13Param = new SqlParameter("@isbn_13", System.Data.SqlDbType.VarChar, 50);

            authorParam.Value = b.Author;
            isbn10Param.Value = b.Isbn10;
            categoryParam.Value = b.Category;
            publisherParam.Value = b.Publisher;
            publicationYearParam.Value = b.Year;
            pagesParam.Value = b.Pages;
            descriptionParam.Value = (object)b.Description ?? DBNull.Value;
            titleParam.Value = b.Title;
            isbn13Param.Value = (object)b.Isbn13 ?? DBNull.Value;

            SqlCommand cmd = new SqlCommand(sqlString, con);
            cmd.Parameters.Add(authorParam);
            cmd.Parameters.Add(isbn10Param);
            cmd.Parameters.Add(categoryParam);
            cmd.Parameters.Add(publisherParam);
            cmd.Parameters.Add(publicationYearParam);
            cmd.Parameters.Add(pagesParam);
            cmd.Parameters.Add(descriptionParam);
            cmd.Parameters.Add(titleParam);
            cmd.Parameters.Add(isbn13Param);

            cmd.Prepare();
            //cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            return id;
        }
    }
}