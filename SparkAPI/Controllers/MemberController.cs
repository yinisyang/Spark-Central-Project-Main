using System;
using System.Collections;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class MemberController : ApiController
    {
        // GET api/<controller>
        public ArrayList Get()
        {
            MemberPersistence memberp = new MemberPersistence();
            return memberp.getMembers();
        }

        // GET api/<controller>/5
        public Member Get(int id)
        {
            MemberPersistence memberp = new MemberPersistence();
            return memberp.getMember(id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Member value)
        {
            MemberPersistence memberp = new MemberPersistence();
            int id = memberp.saveMember(value);
            value.member_id = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, String.Format("member/{0}", id));
            return response;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            MemberPersistence tp = new MemberPersistence();
            bool recordExisted = tp.deleteMember(id);

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
