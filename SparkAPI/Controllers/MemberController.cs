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
        public IEnumerable<Member> Get()
        {
            return null;
            //return new Member[] {
               // , "value2" };
        }

        // GET: api/Member/5
        public Member Get(int id)
        {
            Member mem = new Member();
            mem.MemberId = 1;
            mem.FirstName = "Bob";
            mem.LastName = "Johnson";
            mem.GuardianName = null;
            mem.Ethnicity = "caucasion";
            mem.DOB = DateTime.Parse("5/24/2001");
            mem.Quota = 5;
            mem.State = "WA";
            mem.City = "Spokane";
            mem.Email = "gg@deadinside.com";
            mem.StreetAddress = "333 Elm Street";
            mem.Zip = "33333";
            mem.IsTechRestricted = true;
            mem.IsWestCentralResident = false;
            return mem;
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
