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
            return dvdp.getDVDS(title, release_year, rating);
        }

        // GET api/<controller>/5
        public DVD Get(int item_id)
        {
            DVDPersistence dvdp = new DVDPersistence();
            return dvdp.getDVD(item_id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]DVD value)
        {
            DVDPersistence dvdp = new DVDPersistence();
            int id = dvdp.saveDVD(value);

            if (id != -1)
            {
                value.ItemId = id;
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
            bool recordExisted = dvdp.updateDVD(item_id, value);

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
            bool recordExisted = tp.deleteDVD(item_id);

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