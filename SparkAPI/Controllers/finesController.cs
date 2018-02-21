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
    public class FinesController : ApiController
    {
        public ArrayList Get()
        {
            FinesPersistence finep = new FinesPersistence();
            return finep.getFines();
        }
        public Fine Get(int member_id)
        {
            FinesPersistence finep = new FinesPersistence();
            return finep.getFine(member_id);
        }
        public HttpResponseMessage Post([FromBody]Fine value)
        {
            FinesPersistence finep = new FinesPersistence();
            int id = finep.saveFine(value);
            value.memberId = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("checkout/{0}", id));
            return response;
        }
        public void Put(int id, [FromBody]string value)
        {

        }
        public HttpResponseMessage Delete(int id)
        {
            FinesPersistence finep = new FinesPersistence();
            bool recordExisted = finep.deleteFine(id);

            HttpResponseMessage response;
            if(recordExisted)
            {
                response = WebRequest.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = WebRequest.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}