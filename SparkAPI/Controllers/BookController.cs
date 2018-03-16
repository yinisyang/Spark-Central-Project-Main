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

            Dictionary<String, Tuple<String, System.Data.SqlDbType, int>> args = new Dictionary<String, Tuple<String, System.Data.SqlDbType, int>>();
            // Create a tuple for each param that isn't null and add it to the argument list
            if(isbn10 != null) { args.Add("isbn_10", new Tuple<string, System.Data.SqlDbType, int>(isbn10, System.Data.SqlDbType.VarChar, 50));  }
            if (isbn13 != null) { args.Add("isbn_13", new Tuple<string, System.Data.SqlDbType, int>(isbn13, System.Data.SqlDbType.VarChar, 50)); }
            if (category != null) { args.Add("category", new Tuple<string, System.Data.SqlDbType, int>(category, System.Data.SqlDbType.VarChar, 50)); }
            if (year != null) { args.Add("year", new Tuple<string, System.Data.SqlDbType, int>(year.ToString(), System.Data.SqlDbType.Int, 4)); }

            return bookp.GetAll(args);
        }

        // GET: api/Book/5
        public Book Get(int item_id)
        {
            BookPersistence bookp = new BookPersistence();

            Dictionary<String, String> args = new Dictionary<String, String>();
            args.Add("item_id", item_id.ToString());

            return (Book)bookp.Get(args);
        }

        // POST: api/Book
        public HttpResponseMessage Post([FromBody]Book value)
        {
            BookPersistence bookp = new BookPersistence();
            int id = bookp.Save(value);

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

            Dictionary<String, String> args = new Dictionary<String, String>();
            args.Add("item_id", item_id.ToString());

            bool recordExisted = bookp.Update(args, value);

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

            Dictionary<String, String> args = new Dictionary<String, String>();
            args.Add("item_id", item_id.ToString());

            HttpResponseMessage ret;
            if(bookp.Delete(args))
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
