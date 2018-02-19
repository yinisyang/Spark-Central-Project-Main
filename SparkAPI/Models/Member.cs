using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Member
    {
        public int member_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string guardian_name { get; set; }
        public string email { get; set; }
        public DateTime dob { get; set; }
        public string phone { get; set; }
        public string street_address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zip { get; set; }
        public int quota { get; set; }
        public string member_group { get; set; }
        public string ethnicity { get; set; }
        public bool restricted_to_tech { get; set; }
        public bool west_central_resident { get; set; }
    }
}