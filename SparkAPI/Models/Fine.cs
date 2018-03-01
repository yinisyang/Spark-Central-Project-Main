using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Fine
    {
        public int fineId { get; set; }
        public int memberId { get; set; }
        public double amount { get; set; }
        public String description { get; set; }
    }
}