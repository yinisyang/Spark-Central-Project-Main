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
        public Book Get(int id)
        {
            BookPersistence bookp = new BookPersistence();
            return bookp.GetBook(id);
        }

        // POST: api/Book
        public HttpResponseMessage Post([FromBody]Book value)
        {
            BookPersistence bookp = new BookPersistence();
            int id = bookp.SaveBook(value);
            value.Id = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("book/{0}", id));
            return response;
        }

        // PUT: api/Book/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Book/5
        public HttpResponseMessage Delete(int id)
        {
            BookPersistence bookp = new BookPersistence();
            HttpResponseMessage ret;
            if(bookp.DeleteBook(id))
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
