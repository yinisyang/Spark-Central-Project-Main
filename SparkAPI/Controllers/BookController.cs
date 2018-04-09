using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class BookController : ApiController
    {
        // GET: api/Book
        public ArrayList Get(string isbn10 = null, string isbn13 = null, string category = null, int? publication_year = null)
        {
            BookPersistence bookp = new BookPersistence();
            bookp.addCallField("isbn_10", isbn10, SqlDbType.VarChar, 50);
            bookp.addCallField("isbn_13", isbn13, SqlDbType.VarChar, 50);
            bookp.addCallField("category", category, SqlDbType.VarChar, 50);
            bookp.addCallField("publication_year", publication_year, SqlDbType.Int, 4);

            return bookp.GetAll();
        }

        // GET: api/Book/5
        public Book Get(int item_id)
        {
            BookPersistence bookp = new BookPersistence();
            bookp.addCallField("item_id", item_id, SqlDbType.Int, 4);

            return (Book)bookp.Get();
        }

        // POST: api/Book
        public HttpResponseMessage Post([FromBody]Book value)
        {
            BookPersistence bookp = new BookPersistence();
            int id = bookp.Save(value, "item_id");

            if (id != -1)
            {
                value.item_id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("book?item_id={0}", id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/Book/5
        public HttpResponseMessage Put(int item_id, [FromBody]Book value)
        {
            BookPersistence bookp = new BookPersistence();

            bookp.addCallField("item_id", item_id, SqlDbType.Int, 4);

            bool recordExisted = bookp.Update(value);

            HttpResponseMessage response;
            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }

        // DELETE: api/Book/5
        public HttpResponseMessage Delete(int item_id)
        {
            BookPersistence bookp = new BookPersistence();
            bookp.addCallField("item_id", item_id, SqlDbType.Int, 4);

            HttpResponseMessage ret;
            if(bookp.Delete())
            {
                ret = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                ret = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return ret;
        }
    }
}
