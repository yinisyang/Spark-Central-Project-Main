using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SparkAPI.Models
{
    public class Member : Modellable
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
        public bool is_adult { get; set; }
        public string ethnicity { get; set; }
        public bool restricted_to_tech { get; set; }
        public bool west_central_resident { get; set; }

        public Tuple<SqlDbType, int> GetAssociatedDBTypeAndSize(string propertyName)
        {
            switch (propertyName)
            {
                case "member_id": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "first_name": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "last_name": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "guardian_name": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "email": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "dob": return new Tuple<SqlDbType, int>(SqlDbType.Date, 3);
                case "phone": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "street_address": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "city": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "state": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "zip": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "quota": return new Tuple<SqlDbType, int>(SqlDbType.Int, 4);
                case "is_adult": return new Tuple<SqlDbType, int>(SqlDbType.Bit, 1);
                case "ethnicity": return new Tuple<SqlDbType, int>(SqlDbType.VarChar, 50);
                case "restricted_to_tech": return new Tuple<SqlDbType, int>(SqlDbType.Bit, 1);
                case "west_central_resident": return new Tuple<SqlDbType, int>(SqlDbType.Bit, 1);
                default: throw new ArgumentException("That property name is not registered inside of the current model type");
            }
        }

        public string[] FieldsNotSpecifiedInPOST()
        {
            return new string[] { "member_id" };
        }
    }
}