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
        public ArrayList Get(string title = null, int? releaseYear = null, string rating = null)
        {
            DVDPersistence dvdp = new DVDPersistence();

            //if (title != null || releaseYear != null || rating != null)
                return dvdp.getDVDS(title, releaseYear, rating);
            //else
                //return dvdp.getDVDS();
        }

        // GET api/<controller>/5
        public DVD Get(int id)
        {
            DVDPersistence dvdp = new DVDPersistence();
            return dvdp.getDVD(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]DVD value)
        {
            DVDPersistence dvdp = new DVDPersistence();
            int id = dvdp.saveDVD(value);
            value.ItemId = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("dvd/{0}", id));
            return response;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            DVDPersistence tp = new DVDPersistence();
            bool recordExisted = tp.deleteDVD(id);

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