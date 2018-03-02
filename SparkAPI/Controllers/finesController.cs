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
        public ArrayList Get(int? member_id = null)
        {
            FinesPersistence finep = new FinesPersistence();
            return finep.getFines(member_id);
        }
        public Fine Get(int fine_id)
        {
            FinesPersistence finep = new FinesPersistence();
            return finep.getFine(fine_id);
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
        public HttpResponseMessage Put(int fine_id, [FromBody]Fine value)
        {
            FinesPersistence finep = new FinesPersistence();
            bool recordExisted = finep.updateFine(fine_id, value);

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
        public HttpResponseMessage Delete(int fine_id)
        {
            FinesPersistence finep = new FinesPersistence();
            bool recordExisted = finep.deleteFine(fine_id);

            HttpResponseMessage response;
            if(recordExisted)
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