using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Checkout
    {
        public int ItemId { get; set; }
        public int MemberId { get; set; }
        public String ItemType { get; set; }
        public DateTime dueDate { get; set; }
        public bool resolved { get; set; }
    }
}