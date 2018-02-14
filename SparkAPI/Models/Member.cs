using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Member
    {
        public int MemberId{ get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GuardianName { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int Quota { get; set; }
        public string MemberGroup { get; set; }
        public string Ethnicity { get; set; }
        public bool IsTechRestricted { get; set; }
        public bool IsWestCentralResident { get; set; }

    }
}