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
        public ArrayList Get(bool? is_adult = null, string ethnicity = null, bool? restricted_to_tech = null, bool? west_central_resident = null, string email = null)
        {
            MemberPersistence memberp = new MemberPersistence();
            memberp.addCallField("is_adult", is_adult, System.Data.SqlDbType.Bit, 1);
            memberp.addCallField("ethnicity", ethnicity, System.Data.SqlDbType.VarChar, 50);
            memberp.addCallField("restricted_to_tech", restricted_to_tech, System.Data.SqlDbType.Bit, 1);
            memberp.addCallField("west_central_resident", west_central_resident, System.Data.SqlDbType.Bit, 1);
            memberp.addCallField("email", email, System.Data.SqlDbType.VarChar, 50);

            return memberp.GetAll();
        }

        // GET api/<controller>/5
        public Member Get(int member_id)
        {
            MemberPersistence memberp = new MemberPersistence();
            memberp.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);

            return (Member)memberp.Get();
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Member value)
        {
            MemberPersistence memberp = new MemberPersistence();
            int id = memberp.Save(value, "member_id");

            if (id != -1)
            {
                value.member_id = id;
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
                response.Headers.Location = new Uri(Request.RequestUri, String.Format("member?member_id={0}", id));
                return response;
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int member_id, [FromBody]Member value)
        {
            MemberPersistence memberp = new MemberPersistence();
            memberp.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);

            bool recordExisted = memberp.Update(value);

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
            MemberPersistence memberp = new MemberPersistence();
            memberp.addCallField("member_id", member_id, System.Data.SqlDbType.Int, 4);

            bool recordExisted = memberp.Delete();

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
