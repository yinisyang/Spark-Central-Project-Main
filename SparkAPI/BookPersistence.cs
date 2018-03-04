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

        public ArrayList GetBooks(string isbn10, string isbn13, string category, int? year)
        {
            String sqlString = "SELECT * FROM Books ";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            if(isbn10 != null || isbn13 != null || category != null || year != null)
            {
                sqlString += "WHERE ";
                if(isbn10 != null)
                {
                    sqlString += "isbn_10 = @isbn10 AND ";
                    SqlParameter isbn10Param = new SqlParameter("@isbn10", System.Data.SqlDbType.VarChar, 50);
                    isbn10Param.Value = isbn10;
                    cmd.Parameters.Add(isbn10Param);
                }
                if(isbn13 != null)
                {
                    sqlString += "isbn_13 = @isbn13 AND ";
                    SqlParameter isbn13Param = new SqlParameter("@isbn13", System.Data.SqlDbType.VarChar, 50);
                    isbn13Param.Value = isbn13;
                    cmd.Parameters.Add(isbn13Param);
                }
                if(category != null)
                {
                    sqlString += "category = @category AND ";
                    SqlParameter categoryParam = new SqlParameter("@category", System.Data.SqlDbType.VarChar, 50);
                    categoryParam.Value = category;
                    cmd.Parameters.Add(categoryParam);
                }
                if(year != null)
                {
                    sqlString += "publication_year = @year";
                    SqlParameter yearParam = new SqlParameter("@year", System.Data.SqlDbType.Int, 4);
                    yearParam.Value = year;
                    cmd.Parameters.Add(yearParam);
                }

            }
            if (sqlString.Substring(sqlString.Length - 4).Equals("AND "))
                sqlString = sqlString.Substring(0, sqlString.Length - 4);

            sqlString += ";";
            
            cmd.CommandText = sqlString;
            cmd.Prepare();
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
            con.Close();
            return bookList;
        }

        public Book GetBook(int id)
        {
            String sqlString = "SELECT * FROM Books WHERE item_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, con);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

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

                con.Close();
                return ret;
            }
            con.Close();
            return null;
        }
        public bool DeleteBook(int id)
        {
     //     String sqlString = "SELECT * FROM Books WHERE item_id = " + id.ToString() + ";";
            String sqlString = "SELECT * FROM Books WHERE item_id = @id;";
            SqlCommand cmd = new SqlCommand(sqlString, con);

            //sql parameters for protection
            SqlParameter idParam = new SqlParameter("@id", System.Data.SqlDbType.Int, 4);
            idParam.Value = id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();
            ///////

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                //Iutilized new variables rather than reusing the previous ones due to errors caused if I don't
                String sqlString2 = "DELETE FROM Books WHERE item_id = @id2;";
                SqlCommand delcmd = new SqlCommand(sqlString2, con);

                SqlParameter idParam2 = new SqlParameter("@id2", System.Data.SqlDbType.Int, 4);
                idParam2.Value = idParam.Value;
                delcmd.Parameters.Add(idParam2);

                delcmd.Prepare();

                delcmd.ExecuteNonQuery();
                return true;
            }
            con.Close();
            return false;
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
            con.Close();
            return id;
        }

        public bool updateBook(int item_id, Book toUpdate)
        {
            String sqlString = "SELECT * FROM Books WHERE item_id = @item_id;";
            SqlCommand cmd = new SqlCommand(sqlString, con);

            SqlParameter idParam = new SqlParameter("@item_id", System.Data.SqlDbType.Int, 4);
            idParam.Value = item_id;

            cmd.Parameters.Add(idParam);
            cmd.Prepare();

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                reader.Close();

                String sqlString3 = "UPDATE Books SET author=@author, isbn_10=@isbn_10, category=@category, publisher=@publisher, publication_year=@publication_year, pages=@pages, description=@description, title=@title, isbn_13=@isbn_13 WHERE item_id=@item_id";
                SqlCommand upcmd = new SqlCommand(sqlString3, con);

                SqlParameter authorParam = new SqlParameter("@author", System.Data.SqlDbType.VarChar, 50);
                SqlParameter isbn10Param = new SqlParameter("@isbn_10", System.Data.SqlDbType.VarChar, 20);
                SqlParameter categoryParam = new SqlParameter("@category", System.Data.SqlDbType.VarChar, 50);
                SqlParameter publisherParam = new SqlParameter("@publisher", System.Data.SqlDbType.VarChar, 50);
                SqlParameter yearParam = new SqlParameter("@publication_year", System.Data.SqlDbType.Int, 4);
                SqlParameter pagesParam = new SqlParameter("@pages", System.Data.SqlDbType.Int, 4);
                SqlParameter descParam = new SqlParameter("@description", System.Data.SqlDbType.VarChar, 500);
                SqlParameter titleParam = new SqlParameter("@title", System.Data.SqlDbType.VarChar, 50);
                SqlParameter isbn13Param = new SqlParameter("@isbn_13", System.Data.SqlDbType.VarChar, 50);
                SqlParameter IDParam = new SqlParameter("@item_id", System.Data.SqlDbType.Int, 4);

                authorParam.Value = toUpdate.Author;
                isbn10Param.Value = toUpdate.Isbn10;
                categoryParam.Value = toUpdate.Category;
                publisherParam.Value = toUpdate.Publisher;
                yearParam.Value = toUpdate.Year;
                pagesParam.Value = toUpdate.Pages;
                descParam.Value = toUpdate.Description;
                titleParam.Value = toUpdate.Title;
                isbn13Param.Value = toUpdate.Isbn13;
                IDParam.Value = item_id;

                upcmd.Parameters.Add(authorParam);
                upcmd.Parameters.Add(isbn10Param);
                upcmd.Parameters.Add(categoryParam);
                upcmd.Parameters.Add(publisherParam);
                upcmd.Parameters.Add(yearParam);
                upcmd.Parameters.Add(pagesParam);
                upcmd.Parameters.Add(descParam);
                upcmd.Parameters.Add(titleParam);
                upcmd.Parameters.Add(isbn13Param);
                upcmd.Parameters.Add(IDParam);

                upcmd.Prepare();

                upcmd.ExecuteNonQuery();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}