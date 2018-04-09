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
            finep.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);

            return finep.GetAll();
        }
        public Fine Get(int fine_id)
        {
            FinesPersistence finep = new FinesPersistence();
            finep.addCallField("fine_id", fine_id, System.Data.SqlDbType.Int, 4);

            return (Fine)finep.Get();
        }
        public HttpResponseMessage Post([FromBody]Fine value)
        {
            FinesPersistence finep = new FinesPersistence();
            int id = finep.Save(value, "fine_id");

            if (id != -1)
            {
                value.member_id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("fine?fine_id={0}", id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        public HttpResponseMessage Put(int fine_id, [FromBody]Fine value)
        {
            FinesPersistence finep = new FinesPersistence();
            finep.addCallField("fine_id", fine_id, System.Data.SqlDbType.Int, 4);

            bool recordExisted = finep.Update(value);

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
            finep.addCallField("fine_id", fine_id, System.Data.SqlDbType.Int, 4);

            bool recordExisted = finep.Delete();

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