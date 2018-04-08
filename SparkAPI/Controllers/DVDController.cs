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
    public class DVDController : ApiController
    {
        // GET api/<controller>
        public ArrayList Get(string title = null, int? release_year = null, string rating = null)
        {
            DVDPersistence dvdp = new DVDPersistence();
            dvdp.addCallField("title", title, System.Data.SqlDbType.VarChar, 50);
            dvdp.addCallField("release_year", release_year, System.Data.SqlDbType.Int, 4);
            dvdp.addCallField("rating", rating, System.Data.SqlDbType.VarChar, 50);

            return dvdp.GetAll();
        }

        // GET api/<controller>/5
        public DVD Get(int item_id)
        {
            DVDPersistence dvdp = new DVDPersistence();
            dvdp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);

            return (DVD)dvdp.Get();
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]DVD value)
        {
            DVDPersistence dvdp = new DVDPersistence();
            int id = dvdp.Save(value, "item_id");

            if (id != -1)
            {
                value.item_id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("dvd/{0}", id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int item_id, [FromBody]DVD value)
        {
            DVDPersistence dvdp = new DVDPersistence();
            dvdp.addCallField("item_id", item_id, System.Data.SqlDbType.Int, 4);

            bool recordExisted = dvdp.Update(value);

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
            DVDPersistence tp = new DVDPersistence();
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