using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Book : Modellable
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Isbn10 { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
        public int Year { get; set; }
        public int Pages { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Isbn13 { get; set; }

    }
}