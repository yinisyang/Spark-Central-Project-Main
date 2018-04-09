using System;
using System.Collections.Generic;
using System.Collections;
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
        public ArrayList Get()
        {
            TechnologyPersistence tp = new TechnologyPersistence();

            return tp.GetAll(); 
        }

        // GET api/<controller>/5
        public Technology Get(int item_id)
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            tp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);

            return (Technology)tp.Get();
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Technology value)
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            int id = tp.Save(value, "item_id");

            if (id != -1)
            {
                value.item_id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("technology?item_id={0}", id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int item_id, [FromBody]Technology value)
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            tp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);

            bool recordExisted = tp.Update(value);

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

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int item_id)
        {
            TechnologyPersistence tp = new TechnologyPersistence();
            tp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);

            bool recordExisted = tp.Delete();

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
    }
}