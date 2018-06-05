using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;

namespace SparkAPI
{
    public class MemberPersistence : BasePersistence
    {
        public MemberPersistence() : base("Members") { }

        protected override Modellable RetrieveNextItem(SqlDataReader reader)
        {
            Member m = new Member();
            m.city = reader.GetString(reader.GetOrdinal("city"));
            m.dob = reader.GetDateTime(reader.GetOrdinal("dob"));
            m.signup_date = reader.GetDateTime(reader.GetOrdinal("signup_date"));
            m.email = reader.GetString(reader.GetOrdinal("email"));
            m.ethnicity = reader.GetString(reader.GetOrdinal("ethnicity"));
            m.first_name = reader.GetString(reader.GetOrdinal("first_name"));

            if (reader["guardian_name"] != DBNull.Value)
            {
                m.guardian_name = reader.GetString(reader.GetOrdinal("guardian_name"));
            }
            else
            {
                m.guardian_name = null;
            }

            m.is_adult = reader.GetBoolean(reader.GetOrdinal("is_adult"));
            m.last_name = reader.GetString(reader.GetOrdinal("last_name"));
            m.member_id = reader.GetInt32(reader.GetOrdinal("member_id"));
            m.phone = reader.GetString(reader.GetOrdinal("phone"));
            m.quota = reader.GetInt32(reader.GetOrdinal("quota"));
            m.restricted_to_tech = reader.GetBoolean(reader.GetOrdinal("restricted_to_tech"));
            m.state = reader.GetString(reader.GetOrdinal("state"));
            m.street_address = reader.GetString(reader.GetOrdinal("street_address"));
            m.west_central_resident = reader.GetBoolean(reader.GetOrdinal("west_central_resident"));
            m.zip = reader.GetInt32(reader.GetOrdinal("zip"));

            return m;
        }
    }
}