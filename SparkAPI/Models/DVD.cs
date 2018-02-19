using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class DVD
    {
        public int ItemId { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Rating { get; set; }
    }
}