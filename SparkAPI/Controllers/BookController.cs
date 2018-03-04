using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class BookController : ApiController
    {
        // GET: api/Book
        public ArrayList Get(string isbn10 = null, string isbn13 = null, string category = null, int? year = null)
        {
            BookPersistence bookp = new BookPersistence();
            return bookp.GetBooks(isbn10, isbn13, category, year);
        }

        // GET: api/Book/5
        public Book Get(int item_id)
        {
            BookPersistence bookp = new BookPersistence();
            return bookp.GetBook(item_id);
        }

        // POST: api/Book
        public HttpResponseMessage Post([FromBody]Book value)
        {
            BookPersistence bookp = new BookPersistence();
            int id = bookp.SaveBook(value);

            if (id != -1)
            {
                value.Id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("book/{0}", id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT: api/Book/5
        public HttpResponseMessage Put(int item_id, [FromBody]Book value)
        {
            BookPersistence bookp = new BookPersistence();
            bool recordExisted = bookp.updateBook(item_id, value);

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
            HttpResponseMessage ret;
            if(bookp.DeleteBook(item_id))
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
