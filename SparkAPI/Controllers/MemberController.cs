using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SparkAPI.Models;

namespace SparkAPI.Controllers
{
    public class MemberController : ApiController
    {
        // GET: api/Member
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Member/5
        public Member Get(int id)
        {
            Member member = new Member();
            member.member_id = 1;
            member.first_name = "John";
            member.guardian_name = "none";
            member.email = "example@email.com";
            member.dob = DateTime.Parse("2/17/2018");
            member.phone = "1-800-1111";
            member.street_address = "123 Random Street";
            member.city = "Randoville";
            member.state = "California";
            member.zip = 99000;
            member.quota = 9999;
            member.member_group = "Adult";
            member.ethnicity = "Unknown";
            member.restricted_to_tech = false;
            member.west_central_resident = false;
            member.last_name = "Smith";

            return member;
        }

        // POST: api/Member
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Member/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Member/5
        public void Delete(int id)
        {
        }
    }
}
