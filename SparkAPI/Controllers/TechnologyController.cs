using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class TechnologyController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Technology> Get()
        {
            return null; // new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public Technology Get(int id)
        {
            Technology tech = new Technology();
            tech.ItemId = 1;
            tech.Name = "Test Name";

            return tech;
        }

        // POST api/<controller>
        public void Post([FromBody]Technology value)
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