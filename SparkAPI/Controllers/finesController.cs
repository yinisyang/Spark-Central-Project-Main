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
    public class finesController : ApiController
    {
        public ArrayList Get()
        {
            finesPersistence finep = new finesPersistence();
            return finep.getFines();
        }
        public Fines Get(int member_id)
        {
            finesPersistence finep = new finesPersistence();
            return finep.getFines(member_id);
        }
        public HttpResponseMessage Post([FromBody]Fines value)
        {
            finesPersistence finep = new finesPersistence();
            int id = finep.saveFine(value);
            value.memberId = id;
            HttpResponseMessage response = WebRequest.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("checkout/{0}", item_id));
            return response;
        }
        public void Put(int id, [FromBody]string value)
        {

        }
        public void Delete(int id)
        {

        }
    }
}