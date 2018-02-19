using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using SparkAPI.Models;

namespace SparkAPI
{
    public class MemberPersistence
    {
        private SqlConnection conn;

        public MemberPersistence()
        {
            try
            {
                conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/SparkAPI").ConnectionStrings.ConnectionStrings["SparkCentralConnectionString"].ConnectionString);
                conn.Open();
            }
            catch (SqlException ex)
            {

            }
        }

        public ArrayList getMembers()
        {
            String sqlString = "SELECT * FROM Members;";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            ArrayList MemberArray = new ArrayList();

            while (reader.Read())
            {
                Member member = new Member();

                member.member_id = reader.GetInt32(reader.GetOrdinal("member_id"));
                member.first_name = reader.GetString(reader.GetOrdinal("first_name"));
                member.guardian_name = reader.GetString(reader.GetOrdinal("guardian_name"));
                member.email = reader.GetString(reader.GetOrdinal("email"));
                member.dob = reader.GetDateTime(reader.GetOrdinal("dob"));
                member.phone = reader.GetString(reader.GetOrdinal("phone"));
                member.street_address = reader.GetString(reader.GetOrdinal("street_address"));
                member.city = reader.GetString(reader.GetOrdinal("city"));
                member.state = reader.GetString(reader.GetOrdinal("state"));
                member.zip = reader.GetInt32(reader.GetOrdinal("zip"));
                member.quota = reader.GetInt32(reader.GetOrdinal("quota"));
                member.member_group = reader.GetString(reader.GetOrdinal("member_group"));
                member.ethnicity = reader.GetString(reader.GetOrdinal("ethnicity"));
                member.restricted_to_tech = reader.GetBoolean(reader.GetOrdinal("restricted_to_tech"));
                member.west_central_resident = reader.GetBoolean(reader.GetOrdinal("west_central_resident"));
                member.last_name = reader.GetString(reader.GetOrdinal("last_name"));

                MemberArray.Add(member);

            }
            return MemberArray;
        }

        public Member getMember(int id)
        {
            String sqlString = "SELECT * FROM Members WHERE member_id = " + id.ToString() + ";";
            SqlCommand cmd = new SqlCommand(sqlString, conn);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Member toReturn = new Member();

                toReturn.member_id = reader.GetInt32(reader.GetOrdinal("member_id"));
                toReturn.first_name = reader.GetString(reader.GetOrdinal("first_name"));
                toReturn.guardian_name = reader.GetString(reader.GetOrdinal("guardian_name"));
                toReturn.email = reader.GetString(reader.GetOrdinal("email"));
                toReturn.dob = reader.GetDateTime(reader.GetOrdinal("dob"));
                toReturn.phone = reader.GetString(reader.GetOrdinal("phone"));
                toReturn.street_address = reader.GetString(reader.GetOrdinal("street_address"));
                toReturn.city = reader.GetString(reader.GetOrdinal("city"));
                toReturn.state = reader.GetString(reader.GetOrdinal("state"));
                toReturn.zip = reader.GetInt32(reader.GetOrdinal("zip"));
                toReturn.quota = reader.GetInt32(reader.GetOrdinal("quota"));
                toReturn.member_group = reader.GetString(reader.GetOrdinal("member_group"));
                toReturn.ethnicity = reader.GetString(reader.GetOrdinal("ethnicity"));
                toReturn.restricted_to_tech = reader.GetBoolean(reader.GetOrdinal("restricted_to_tech"));
                toReturn.west_central_resident = reader.GetBoolean(reader.GetOrdinal("west_central_resident"));
                toReturn.last_name = reader.GetString(reader.GetOrdinal("last_name"));

                return toReturn;
            }
            return null;
        }

        public int saveMember(Member memberToSave)
        {
            String sqlString = "INSERT INTO Members (first_name, guardian_name, email, dob, phone, " +
                "street_address, city, state, zip, quota, member_group, ethnicity, restricted_to_tech, west_central_resident, last_name) " +
                "OUTPUT INSERTED.item_id VALUES (@first_name, @guardian_name, @email, @dob, @phone, " +
                "@street_address, @city, @state, @zip, @quota, @member_group, @ethnicity, @restricted_to_tech, @west_central_resident, @last_name)";

            SqlParameter first_nameParam = new SqlParameter("@first_name", System.Data.SqlDbType.VarChar, 50);
            SqlParameter guardian_nameParam = new SqlParameter("@guardian_name", System.Data.SqlDbType.VarChar, 50);
            SqlParameter emailParam = new SqlParameter("@email", System.Data.SqlDbType.VarChar, 50);
            SqlParameter dobParam = new SqlParameter("@dob", System.Data.SqlDbType.Date, 3);
            SqlParameter phoneParam = new SqlParameter("@phone", System.Data.SqlDbType.VarChar, 50);
            SqlParameter street_addressParam = new SqlParameter("@street_address", System.Data.SqlDbType.VarChar, 50);
            SqlParameter cityParam = new SqlParameter("@city", System.Data.SqlDbType.VarChar, 50);
            SqlParameter stateParam = new SqlParameter("@state", System.Data.SqlDbType.VarChar, 50);
            SqlParameter zipParam = new SqlParameter("@zip", System.Data.SqlDbType.Int, 4);
            SqlParameter quotaParam = new SqlParameter("@quota", System.Data.SqlDbType.Int, 4);
            SqlParameter member_groupParam = new SqlParameter("@member_group", System.Data.SqlDbType.VarChar, 50);
            SqlParameter ethnicityParam = new SqlParameter("@ethnicity", System.Data.SqlDbType.VarChar, 50);
            SqlParameter restricted_to_techParam = new SqlParameter("@restricted_to_tech", System.Data.SqlDbType.Bit, 1);
            SqlParameter west_central_residentParam = new SqlParameter("@west_central_resident", System.Data.SqlDbType.Bit, 1);
            SqlParameter last_nameParam = new SqlParameter("@last_name", System.Data.SqlDbType.VarChar, 50);

            first_nameParam.Value = memberToSave.first_name;
            guardian_nameParam.Value = memberToSave.guardian_name;
            emailParam.Value = memberToSave.email;
            dobParam.Value = memberToSave.dob;
            phoneParam.Value = memberToSave.phone;
            street_addressParam.Value = memberToSave.street_address;
            cityParam.Value = memberToSave.city;
            stateParam.Value = memberToSave.state;
            zipParam.Value = memberToSave.zip;
            quotaParam.Value = memberToSave.quota;
            member_groupParam.Value = memberToSave.member_group;
            ethnicityParam.Value = memberToSave.ethnicity;
            restricted_to_techParam.Value = memberToSave.restricted_to_tech;
            west_central_residentParam.Value = memberToSave.west_central_resident;
            last_nameParam.Value = memberToSave.last_name;

            SqlCommand cmd = new SqlCommand(sqlString, conn);

            cmd.Parameters.Add(first_nameParam);
            cmd.Parameters.Add(guardian_nameParam);
            cmd.Parameters.Add(emailParam);
            cmd.Parameters.Add(dobParam);
            cmd.Parameters.Add(phoneParam);
            cmd.Parameters.Add(street_addressParam);
            cmd.Parameters.Add(cityParam);
            cmd.Parameters.Add(stateParam);
            cmd.Parameters.Add(zipParam);
            cmd.Parameters.Add(quotaParam);
            cmd.Parameters.Add(member_groupParam);
            cmd.Parameters.Add(ethnicityParam);
            cmd.Parameters.Add(restricted_to_techParam);
            cmd.Parameters.Add(west_central_residentParam);
            cmd.Parameters.Add(last_nameParam);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            int id = (int)cmd.ExecuteScalar();
            return id;
        }
    }
}