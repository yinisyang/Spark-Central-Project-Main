using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class DVDController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<DVD> Get()
        {
            return null; // new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public DVD Get(int id)
        {
            DVD dvd = new DVD();
            dvd.ItemId = id;
            dvd.Rating = "PG-13";
            dvd.ReleaseYear = 2013;
            dvd.Title = "A Midsummer's Dream";

            return dvd;
        }

        // POST api/<controller>
        public void Post([FromBody]DVD value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}