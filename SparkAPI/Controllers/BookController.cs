using System;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Book/5
        public string Get(int id)
        {
            return "BOOOOK";
        }

        // POST: api/Book
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Book/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Book/5
        public void Delete(int id)
        {
        }
    }
}
