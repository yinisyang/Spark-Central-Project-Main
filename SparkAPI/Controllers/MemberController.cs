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
        public ArrayList Get(string member_group = null, string ethnicity = null, bool? restricted_to_tech = null, bool? west_central_resident = null, string email = null)
        {
            MemberPersistence memberp = new MemberPersistence();
            return memberp.getMembers(ethnicity, restricted_to_tech, west_central_resident, email);
        }

        // GET api/<controller>/5
        public Member Get(int member_id)
        {
            MemberPersistence memberp = new MemberPersistence();
            return memberp.getMember(member_id);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Member value)
        {
            MemberPersistence memberp = new MemberPersistence();
            int id = memberp.saveMember(value);

            if (id != -1)
            {
                value.member_id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("member/{0}", id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int member_id, [FromBody]Member value)
        {
            MemberPersistence memberp = new MemberPersistence();
            bool recordExisted = memberp.updateMember(member_id, value);

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
        public HttpResponseMessage Delete(int member_id)
        {
            MemberPersistence tp = new MemberPersistence();
            bool recordExisted = tp.deleteMember(member_id);

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
